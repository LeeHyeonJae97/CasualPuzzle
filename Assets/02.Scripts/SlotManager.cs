using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotManager : MonoBehaviour
{
    public static List<Slot> SelectedSlots = new List<Slot>();
    public static Slot.SlotType SelectedSlotType;
    public static int SelectedCount => SelectedSlots.Count;
    public static Vector2 LastSelectedPos => SelectedSlots[SelectedCount - 1].briefPos;
    public static LineRenderer Lr;

    [SerializeField] private LineRenderer lr;
    [SerializeField] private Vector2 slotSize;
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private Transform holder;   
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameManager timer;
    [SerializeField] private float popInterval;
    [SerializeField] private float supplementDuration;
    [SerializeField] private GameObject _cover;
    [SerializeField] private GameEventChannel _channel;
    private Vector2 firstPos;
    private float offsetX, offsetY;
    private WaitForSeconds popIntervalWait;
    private Slot[,] slots;

    public static void Select(Slot slot)
    {
        SelectedSlots.Add(slot);
        Lr.positionCount = SelectedCount;
        Lr.SetPosition(SelectedCount - 1, slot.transform.position);

        AudioChannel.Get("SFX").Play("Selected");
    }

    public static void Cancel()
    {
        SelectedSlots.RemoveAt(SelectedCount - 1);
        Lr.positionCount = SelectedCount;
    }

    private void Awake()
    {
        Lr = lr;
    }

    private void Start()
    {
        popIntervalWait = new WaitForSeconds(popInterval);
    }

    private void OnEnable()
    {
        _channel.OnGameStarted += OnGameStarted;
        _channel.OnRestarted += OnRestarted;
        _channel.OnBackToMain += OnBackToMain;
    }

    private void OnDisable()
    {
        _channel.OnGameStarted -= OnGameStarted;
        _channel.OnRestarted -= OnRestarted;
        _channel.OnBackToMain -= OnBackToMain;
    }

    private void OnGameStarted()
    {
        Organize();
    }

    private void OnRestarted()
    {
        Clear();
        Organize();
    }

    private void OnBackToMain()
    {
        Clear();
    }

    public void Organize()
    {
        slots = new Slot[row, column];

        int halfColumn = column / 2;
        int halfRow = row / 2;

        offsetX = slotSize.x + spacing.x;
        offsetY = slotSize.y + spacing.y;

        float firstX = column % 2 == 0 ? (halfColumn - 1) * offsetX + offsetX / 2 : halfColumn * offsetX;
        float firstY = row % 2 == 0 ? (halfRow - 1) * offsetY + offsetY / 2 : halfRow * offsetY;
        firstPos = center + new Vector2(-firstX, -firstY);

        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < column; x++)
            {
                int slotTypeIndex = Random.Range(0, Slot.SlotTypeCount);

                Slot slot = PoolingManager.Instance.Spawn("Slot", holder).GetComponent<Slot>();
                slot.Init((Slot.SlotType)slotTypeIndex, sprites[slotTypeIndex], firstPos + new Vector2(offsetX * x, offsetY * y), new Vector2(x, y), () => StartCoroutine(Pop()));
                slots[y, x] = slot;
            }
        }

        _cover.transform.SetAsLastSibling();
    }

    private IEnumerator Supplement()
    {
        for (int x = 0; x < slots.GetLength(1); x++)
        {
            int blank = 0;

            for (int y = 0; y < slots.GetLength(0); y++)
            {
                Slot slot = slots[y, x];

                if (slot == null)
                {
                    blank++;
                }
                else if (blank > 0)
                {
                    slot.transform.DOMoveY(slot.transform.position.y - offsetY * blank, supplementDuration).SetEase(Ease.OutSine);
                    slot.briefPos = new Vector2(x, y - blank);
                    slots[y - blank, x] = slot;
                }
            }

            for (int i = 0; i < blank; i++)
            {
                int slotTypeIndex = Random.Range(0, Slot.SlotTypeCount);

                Slot slot = PoolingManager.Instance.Spawn("Slot", holder).GetComponent<Slot>();
                slot.Init((Slot.SlotType)slotTypeIndex, sprites[slotTypeIndex], firstPos + new Vector2(offsetX * x, offsetY * (row + i)), new Vector2(x, row + i - blank), () => StartCoroutine(Pop()));

                slot.transform.DOMoveY(slot.transform.position.y - offsetX * blank, supplementDuration).SetEase(Ease.OutSine);
                slots[row + i - blank, x] = slot;
            }
        }

        yield return new WaitForSeconds(supplementDuration);
    }

    public IEnumerator Pop()
    {
        GetScore();
        GetExtraTime();

        _cover.SetActive(true);

        for (int i = 0; i < SelectedSlots.Count; i++)
        {
            int x = (int)SelectedSlots[i].briefPos.x;
            int y = (int)SelectedSlots[i].briefPos.y;
            slots[y, x] = null;

            PoolingManager.Instance.Despawn(SelectedSlots[i].gameObject);

            GameObject effect = PoolingManager.Instance.Spawn("Pop", SelectedSlots[i].transform.position);
            PoolingManager.Instance.Despawn(effect, 1);

            AudioChannel.Get("SFX").Play("Pop");

            yield return popIntervalWait;
        }
        SelectedSlots.Clear();

        yield return StartCoroutine(Supplement());

        _cover.SetActive(false);
    }

    public void Clear()
    {
        if (slots != null)
        {
            for (int i = 0; i < slots.GetLength(0); i++)
            {
                for (int j = 0; j < slots.GetLength(1); j++)
                    Destroy(slots[i, j].gameObject);
            }
        }

        if (SelectedCount > 0) SelectedSlots = new List<Slot>();
    }

    private void GetScore()
    {
        timer.Score += SelectedCount * SelectedCount;
    }

    private void GetExtraTime()
    {
        timer.GetExtraTime(SelectedCount * 0.35f);
    }

    private void OnDrawGizmos()
    {
        int halfColumn = column / 2;
        int halfRow = row / 2;

        offsetX = slotSize.x + spacing.x;
        offsetY = slotSize.y + spacing.y;

        float firstX = column % 2 == 0 ? (halfColumn - 1) * offsetX + offsetX / 2 : halfColumn * offsetX;
        float firstY = row % 2 == 0 ? (halfRow - 1) * offsetY + offsetY / 2 : halfRow * offsetY;
        firstPos = center + new Vector2(-firstX, -firstY);

        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < column; x++)
            {
                Gizmos.DrawWireCube(firstPos + new Vector2(offsetX * x, offsetY * y), slotSize);
            }
        }
    }
}

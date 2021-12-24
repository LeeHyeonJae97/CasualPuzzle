using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotManager : MonoBehaviour
{
    // 현재 선택된 슬롯들에 대한 정보
    public static List<Slot> SelectedSlots = new List<Slot>();
    public static Slot.SlotType SelectedSlotType;
    public static int SelectedCount => SelectedSlots.Count;
    public static Vector2 LastSelectedPos => SelectedSlots[SelectedCount - 1].briefPos;

    // 라인렌더러
    public static LineRenderer Lr;
    [SerializeField] private LineRenderer lr;

    // 현재 화면에 보여지는 모든 슬롯들이 담긴 배열
    private Slot[,] slots;

    // 슬롯들을 배치하기 위한 정보
    [SerializeField] private Vector2 slotSize;
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private Transform holder;
    private Vector2 firstPos;
    private float offsetX, offsetY;

    // 슬롯 이미지 후보 배열
    [SerializeField] private Sprite[] sprites;

    // 점수
    [SerializeField] private ScoreManager score;

    // 타이머
    [SerializeField] private Timer timer;

    // 팝 인터벌
    [SerializeField] private float popInterval;
    private WaitForSeconds popIntervalWait;

    // 보충 시간
    [SerializeField] private float supplementDuration;

    // 커버 캔버스
    [SerializeField] private Canvas coverCanvas;

    public static void Select(Slot slot)
    {
        SelectedSlots.Add(slot);
        Lr.positionCount = SelectedCount;
        Lr.SetPosition(SelectedCount - 1, slot.transform.position);

        AudioManager.Instance.PlaySfx("Selected");
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

    // 최초에 슬롯들을 랜덤하게 배치
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
        // 점수 및 추가 시간 획득
        GetScore();
        GetExtraTime();

        // 커버 활성화
        coverCanvas.enabled = true;

        // 선택된 슬롯들 제거
        for (int i = 0; i < SelectedSlots.Count; i++)
        {
            int x = (int)SelectedSlots[i].briefPos.x;
            int y = (int)SelectedSlots[i].briefPos.y;
            slots[y, x] = null;

            PoolingManager.Instance.Despawn(SelectedSlots[i].gameObject);

            GameObject effect = PoolingManager.Instance.Spawn("Pop", SelectedSlots[i].transform.position);
            PoolingManager.Instance.Despawn(effect, 1);
            AudioManager.Instance.PlaySfx("Pop");

            yield return popIntervalWait;
        }
        SelectedSlots.Clear();

        // 빈 공간 보충
        yield return StartCoroutine(Supplement());

        // 커버 비활성화
        coverCanvas.enabled = false;
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
        score.Gain(SelectedCount * SelectedCount);
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

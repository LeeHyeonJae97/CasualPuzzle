using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public enum SlotType { Red, Green, Blue, Yellow, Pink, Black }
    public const int SlotTypeCount = 6;

    public static bool IsDown;

    private SlotType slotType;
    [HideInInspector] public Vector2 briefPos;    

    [SerializeField] private Image image;
    [SerializeField] private Vector2 idleSize;
    [SerializeField] private Vector2 selectedSize;

    private UnityAction pop;

    public void Init(SlotType slotType, Sprite sprite, Vector2 pos, Vector2 briefPos, UnityAction pop)
    {
        this.slotType = slotType;        
        this.briefPos = briefPos;
        this.pop = pop;

        image.sprite = sprite;
        image.rectTransform.sizeDelta = idleSize;

        transform.position = pos;
        transform.localScale = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDown = true;
        SlotManager.Lr.gameObject.SetActive(true);

        SetSize(true);
        SlotManager.Select(this);        
        SlotManager.SelectedSlotType = slotType;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDown = false;
        SlotManager.Lr.gameObject.SetActive(false);

        if (!GameManager.IsPlaying) return;

        if (SlotManager.SelectedCount < 3)
        {
            for (int i = 0; i < SlotManager.SelectedCount; i++)
                SlotManager.SelectedSlots[i].SetSize(false);
            SlotManager.SelectedSlots.Clear();
        }
        else
        {
            pop?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsDown && SlotManager.SelectedSlotType == slotType)
        {
            if (!SlotManager.SelectedSlots.Contains(this) && CheckDistance())
            {
                SetSize(true);
                SlotManager.Select(this);
            }
            else if (SlotManager.SelectedSlots.Contains(this) && SlotManager.SelectedCount > 1 && this == SlotManager.SelectedSlots[SlotManager.SelectedSlots.Count - 2])
            {
                SlotManager.SelectedSlots[SlotManager.SelectedSlots.Count - 1].SetSize(false);                
                SlotManager.Cancel();
            }
        }
    }

    private void SetSize(bool selected)
    {
        image.rectTransform.sizeDelta = selected ? selectedSize : idleSize;
    }

    private bool CheckDistance()
    {
        Vector2 basePos = SlotManager.LastSelectedPos;
        return basePos.x - 1 <= briefPos.x && briefPos.x <= basePos.x + 1 && basePos.y - 1 <= briefPos.y && briefPos.y <= basePos.y + 1;
    }
}

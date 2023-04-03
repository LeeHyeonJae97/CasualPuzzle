using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExtendedScrollRect : ScrollRect
{
    public enum Type { Horizontal, Vertical, GridHorizontal, GridVertical }

    public IScrollRectItem[] Items
    {
        get
        {
            IScrollRectItem[] items = new IScrollRectItem[_items.Count];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = _items[i];
            }

            return items;
        }
    }
    public int Length => _items.Count;
    public int LengthVisible { get; private set; }
    public bool Full => Length >= LengthVisible;
    private LayoutGroup LayoutGroup => content.GetComponent<LayoutGroup>();

    [SerializeField] private Type _type;
    [SerializeField] private bool _circularLoop;
    [SerializeField] private bool _scrollbar;
    private List<IScrollRectItem> _items;
    private float _slotWidthOrHeight;
    private int _firstIndex;
    private int _firstIndexCircular;
    private int _loop;
    private int _rowOrColumnCount;
    private int _padding;
    private Vector2 _spacing;

    protected override void Awake()
    {
        base.Awake();

        onValueChanged.AddListener(OnValueChanged);
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();

        ResetInternal();
    }
#endif

    private void ResetInternal()
    {
        StopMovement();

        switch (_type)
        {
            case Type.Horizontal:
                InitializeHorizontal();
                break;
            case Type.Vertical:
                InitializeVertical();
                break;

            case Type.GridHorizontal:
                InitializeGridHorizontal();
                break;

            case Type.GridVertical:
                InitializeGridVertical();
                break;
        }

        for (int i = 0; i < LengthVisible; i++)
        {
            if (i < Length)
            {
                content.GetChild(i).GetComponent<ScrollRectSlot>().Init(_items[i + _firstIndex]);
                content.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                content.GetChild(i).gameObject.SetActive(false);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        void InitializeHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // reset padding value
            LayoutGroup.padding.left = _padding;

            // set content's size
            content.sizeDelta = new Vector2(Length * _slotWidthOrHeight + (Length - 1) * _spacing.x + _padding + LayoutGroup.padding.right, 0);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // reset padding value
            LayoutGroup.padding.top = _padding;

            // set content's size
            content.sizeDelta = new Vector2(0, Length * _slotWidthOrHeight + (Length - 1) * _spacing.y + _padding + LayoutGroup.padding.bottom);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeGridHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // reset padding value
            LayoutGroup.padding.left = _padding;

            // set content's size
            content.sizeDelta = new Vector2(Mathf.CeilToInt(Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + _padding + LayoutGroup.padding.right, 0);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeGridVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // reset padding value
            LayoutGroup.padding.top = _padding;

            // set content's size
            content.sizeDelta = new Vector2(0, Mathf.CeilToInt(Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + _padding + LayoutGroup.padding.bottom);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }
    }

    public void Initialize(List<IScrollRectItem> items)
    {
        if (content == null || LayoutGroup == null) return;

        _items = items;

        switch (_type)
        {
            case Type.Horizontal:
                InitializeHorizontal();
                break;
            case Type.Vertical:
                InitializeVertical();
                break;

            case Type.GridHorizontal:
                InitializeGridHorizontal();
                break;

            case Type.GridVertical:
                InitializeGridVertical();
                break;
        }

        var slots = GetComponentsInChildren<ScrollRectSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Length)
            {
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }

        void InitializeHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // get slot's width
            _slotWidthOrHeight = GetComponentInChildren<ScrollRectSlot>().GetComponent<RectTransform>().GetSize().x;

            // save padding value
            _padding = LayoutGroup.padding.left;

            // save spacing value
            _spacing.x = ((HorizontalLayoutGroup)LayoutGroup).spacing;

            // get max count of slots needed and instantiate slot
            LengthVisible = GetComponentsInChildren<ScrollRectSlot>().Length;

            // set content's size
            content.sizeDelta = new Vector2(Length * _slotWidthOrHeight + (Length - 1) * _spacing.x + LayoutGroup.padding.left + LayoutGroup.padding.right, 0);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // get slot's height
            _slotWidthOrHeight = GetComponentInChildren<ScrollRectSlot>().GetComponent<RectTransform>().GetSize().y;

            // save padding value
            _padding = LayoutGroup.padding.top;

            // save spacing value
            _spacing.y = ((VerticalLayoutGroup)LayoutGroup).spacing;

            // get max count of slots needed and instantiate slot
            LengthVisible = GetComponentsInChildren<ScrollRectSlot>().Length;

            // set content's size
            content.sizeDelta = new Vector2(0, Length * _slotWidthOrHeight + (Length - 1) * _spacing.y + LayoutGroup.padding.top + LayoutGroup.padding.bottom);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeGridHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            var size = GetComponentInChildren<ScrollRectSlot>().GetComponent<RectTransform>().GetSize();

            // get slot's width
            _slotWidthOrHeight = size.x;

            var layoutGroup = (GridLayoutGroup)LayoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // save padding value
            _padding = LayoutGroup.padding.left;

            // save spacing value
            _spacing = layoutGroup.spacing;

            // get slot count that need per column
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().GetSize().y / (size.y + _spacing.y));

            // get max count of slots needed and instantiate slot
            LengthVisible = _rowOrColumnCount * GetComponentsInChildren<ScrollRectSlot>().Length;

            // set content's size
            content.sizeDelta = new Vector2(Mathf.CeilToInt((float)Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + LayoutGroup.padding.left + LayoutGroup.padding.right, 0);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeGridVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            var size = GetComponentInChildren<ScrollRectSlot>().GetComponent<RectTransform>().GetSize();

            // get slot's height
            _slotWidthOrHeight = size.y;

            var layoutGroup = (GridLayoutGroup)LayoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // save spacing value
            _spacing = layoutGroup.spacing;

            // save padding value
            _padding = LayoutGroup.padding.top;

            // get slot count that need per row
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().GetSize().x / (size.x + _spacing.x));

            // get max count of slots needed and instantiate slot
            LengthVisible = _rowOrColumnCount * GetComponentsInChildren<ScrollRectSlot>().Length;

            // set content's size
            content.sizeDelta = new Vector2(0, Mathf.CeilToInt((float)Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + LayoutGroup.padding.top + LayoutGroup.padding.bottom);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }
    }

    public void Initialize(List<IScrollRectItem> items, ScrollRectSlot slotPrefab)
    {
        if (content == null || LayoutGroup == null) return;

        _items = items;

        switch (_type)
        {
            case Type.Horizontal:
                InitializeHorizontal();
                break;
            case Type.Vertical:
                InitializeVertical();
                break;

            case Type.GridHorizontal:
                InitializeGridHorizontal();
                break;

            case Type.GridVertical:
                InitializeGridVertical();
                break;
        }

        for (int i = 0; i < LengthVisible; i++)
        {
            var slot = Instantiate(slotPrefab, content);

            if (i < Length)
            {
                slot.Init(items[i]);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }

        void InitializeHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // get slot's width
            _slotWidthOrHeight = slotPrefab.GetComponent<RectTransform>().GetSize().x;

            // save padding value
            _padding = LayoutGroup.padding.left;

            // save spacing value
            _spacing.x = ((HorizontalLayoutGroup)LayoutGroup).spacing;

            // get max count of slots needed and instantiate slot
            LengthVisible = Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().x / (_slotWidthOrHeight + _spacing.x)) + 1;

            // set content's size
            content.sizeDelta = new Vector2(Length * _slotWidthOrHeight + (Length - 1) * _spacing.x + LayoutGroup.padding.left + LayoutGroup.padding.right, 0);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            // get slot's height
            _slotWidthOrHeight = slotPrefab.GetComponent<RectTransform>().GetSize().y;

            // save padding value
            _padding = LayoutGroup.padding.top;

            // save spacing value
            _spacing.y = ((VerticalLayoutGroup)LayoutGroup).spacing;

            // get max count of slots needed and instantiate slot
            LengthVisible = Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().y / (_slotWidthOrHeight + _spacing.y)) + 1;

            // set content's size
            content.sizeDelta = new Vector2(0, Length * _slotWidthOrHeight + (Length - 1) * _spacing.y + LayoutGroup.padding.top + LayoutGroup.padding.bottom);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeGridHorizontal()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            var size = slotPrefab.GetComponent<RectTransform>().GetSize();

            // get slot's width
            _slotWidthOrHeight = size.x;

            var layoutGroup = (GridLayoutGroup)LayoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // save padding value
            _padding = LayoutGroup.padding.left;

            // save spacing value
            _spacing = layoutGroup.spacing;

            // get slot count that need per column
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().GetSize().y / (size.y + _spacing.y));

            // get max count of slots needed and instantiate slot
            LengthVisible = _rowOrColumnCount * (Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().x / (_slotWidthOrHeight + _spacing.x)) + 1);

            // set content's size
            content.sizeDelta = new Vector2(Mathf.CeilToInt((float)Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + LayoutGroup.padding.left + LayoutGroup.padding.right, 0);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }

        void InitializeGridVertical()
        {
            _firstIndex = 0;
            _firstIndexCircular = 0;

            var size = slotPrefab.GetComponent<RectTransform>().GetSize();

            // get slot's height
            _slotWidthOrHeight = size.y;

            var layoutGroup = (GridLayoutGroup)LayoutGroup;

            // set cell size
            layoutGroup.cellSize = size;

            // save spacing value
            _spacing = layoutGroup.spacing;

            // save padding value
            _padding = LayoutGroup.padding.top;

            // get slot count that need per row
            _rowOrColumnCount = layoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount ? layoutGroup.constraintCount : Mathf.FloorToInt(GetComponent<RectTransform>().GetSize().x / (size.x + _spacing.x));

            // get max count of slots needed and instantiate slot
            LengthVisible = _rowOrColumnCount * (Mathf.CeilToInt(GetComponent<RectTransform>().GetSize().y / (_slotWidthOrHeight + _spacing.y)) + 1);

            // set content's size
            content.sizeDelta = new Vector2(0, Mathf.CeilToInt((float)Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + LayoutGroup.padding.top + LayoutGroup.padding.bottom);

            // reset content's anchored position
            content.anchoredPosition = Vector2.zero;
        }
    }

    public void SetItems(List<IScrollRectItem> items)
    {
        _items = items;

        Refresh();
    }

    public void AddItem(IScrollRectItem item)
    {
        _items.Add(item);

        Refresh();
    }

    public void AddItems(List<IScrollRectItem> items)
    {
        _items.AddRange(items);

        Refresh();
    }

    public void InsertItem(int index, IScrollRectItem item)
    {
        _items.Insert(index, item);

        Refresh();
    }

    public void InsertItems(int index, List<IScrollRectItem> items)
    {
        _items.InsertRange(index, items);

        Refresh();
    }

    public void RemoveItem(IScrollRectItem item)
    {
        _items.Remove(item);

        Refresh();
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);

        Refresh();
    }

    public void RemoveRange(int index, int count)
    {
        _items.RemoveRange(index, count);

        Refresh();
    }

    private void Refresh()
    {
        if (_circularLoop)
        {
            ResetInternal();
            return;
        }

        if (_firstIndex + LengthVisible >= Length)
        {
            _firstIndex = Mathf.Max(Length - LengthVisible, 0);
        }

        switch (_type)
        {
            case Type.Horizontal:
                InitializeHorizontal();
                break;
            case Type.Vertical:
                InitializeVertical();
                break;

            case Type.GridHorizontal:
                InitializeGridHorizontal();
                break;

            case Type.GridVertical:
                InitializeGridVertical();
                break;
        }

        for (int i = 0; i < LengthVisible; i++)
        {
            if (i < Length)
            {
                content.GetChild(i).GetComponent<ScrollRectSlot>().Init(_items[i + _firstIndex]);
                content.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                content.GetChild(i).gameObject.SetActive(false);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        void InitializeHorizontal()
        {
            LayoutGroup.padding.left = _padding + (int)(_slotWidthOrHeight + _spacing.x) * _firstIndex;

            // set content's size
            content.sizeDelta = new Vector2(Length * _slotWidthOrHeight + (Length - 1) * _spacing.x + _padding + LayoutGroup.padding.right, 0);
        }

        void InitializeVertical()
        {
            LayoutGroup.padding.top = _padding + (int)(_slotWidthOrHeight + _spacing.y) * _firstIndex;

            // set content's size
            content.sizeDelta = new Vector2(0, Length * _slotWidthOrHeight + (Length - 1) * _spacing.y + _padding + LayoutGroup.padding.bottom);
        }

        void InitializeGridHorizontal()
        {
            LayoutGroup.padding.left = _padding + (int)(_slotWidthOrHeight + _spacing.x) * (_firstIndex / _rowOrColumnCount);

            // set content's size
            content.sizeDelta = new Vector2(Mathf.CeilToInt(Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) - _spacing.x + _padding + LayoutGroup.padding.right, 0);
        }

        void InitializeGridVertical()
        {
            LayoutGroup.padding.top = _padding + (int)(_slotWidthOrHeight + _spacing.y) * (_firstIndex / _rowOrColumnCount);

            // set content's size
            content.sizeDelta = new Vector2(0, Mathf.CeilToInt(Length / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y) - _spacing.y + _padding + LayoutGroup.padding.bottom);
        }
    }

    private void OnValueChanged(Vector2 arg)
    {
        // if don't need to reuse slot, just finish
        if (!Full) return;

        switch (_type)
        {
            case Type.Horizontal:
                OnValueChangedHorizontal();
                break;

            case Type.Vertical:
                OnValueChangedVertical();
                break;

            case Type.GridHorizontal:
                OnValueChangedGridHorizontal();
                break;

            case Type.GridVertical:
                OnValueChangedGridVertical();
                break;
        }

        void OnValueChangedHorizontal()
        {
            // move first slot to last
            if (base.content.anchoredPosition.x < (_firstIndex + 1) * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                int gap = (int)(base.content.anchoredPosition.x / (_slotWidthOrHeight + _spacing.x) + _firstIndex);

                for (int i = 0; i < gap * -1; i++)
                {
                    if (_circularLoop)
                    {
                        // increase padding
                        LayoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                        _firstIndex++;

                        _loop = _firstIndex / Length;

                        // calculate circular first index
                        _firstIndexCircular = ++_firstIndexCircular % Length;

                        // calculate circular last index
                        int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndexCircular]);

                        // move to last
                        slot.SetAsLastSibling();
                    }
                    else
                    {
                        // if there's nothing to show more, just finish
                        if (_firstIndex + LengthVisible == Length) return;

                        // increase padding
                        LayoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                        // get index
                        int lastIndex = ++_firstIndex + LengthVisible - 1;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndex]);

                        // move to last
                        slot.SetAsLastSibling();
                    }
                }
            }

            // move last slot to first
            else if (base.content.anchoredPosition.x > _firstIndex * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                int gap = (int)(base.content.anchoredPosition.x / (_slotWidthOrHeight + _spacing.x) + (_firstIndex + 1));

                for (int i = 0; i < gap; i++)
                {
                    if (_circularLoop)
                    {
                        // reduce padding
                        LayoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                        _firstIndex--;

                        _loop = _firstIndex / Length;

                        // calculate circular index
                        _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[_firstIndexCircular]);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                    else
                    {
                        if (_firstIndex == 0) return;

                        // reduce padding
                        LayoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[--_firstIndex]);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                }
            }
        }

        void OnValueChangedVertical()
        {
            // move first slot to last
            if (base.content.anchoredPosition.y > (_firstIndex + 1) * (_slotWidthOrHeight + _spacing.y))
            {
                int gap = (int)(base.content.anchoredPosition.y / (_slotWidthOrHeight + _spacing.y) - _firstIndex);

                for (int i = 0; i < gap; i++)
                {
                    if (_circularLoop)
                    {
                        // increase padding
                        LayoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                        _firstIndex++;

                        _loop = _firstIndex / Length;

                        // calculate circular first index
                        _firstIndexCircular = ++_firstIndexCircular % Length;

                        // calculate circular last index
                        int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndexCircular]);

                        // move to last
                        slot.SetAsLastSibling();
                    }
                    else
                    {
                        // if there's nothing to show more, just finish
                        if (_firstIndex + LengthVisible == Length) return;

                        // increase padding
                        LayoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                        // get index
                        int lastIndex = ++_firstIndex + LengthVisible - 1;

                        // get slot
                        var slot = content.GetChild(0);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndex]);

                        // move to last
                        slot.SetAsLastSibling();
                    }
                }
            }

            // move last slot to first
            else if (base.content.anchoredPosition.y < _firstIndex * (_slotWidthOrHeight + _spacing.y))
            {
                int gap = (int)(base.content.anchoredPosition.y / (_slotWidthOrHeight + _spacing.y) - (_firstIndex + 1));

                for (int i = 0; i < gap * -1; i++)
                {
                    if (_circularLoop)
                    {
                        // reduce padding
                        LayoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                        _firstIndex--;

                        _loop = _firstIndex / Length;

                        // calculate circular index
                        _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[_firstIndexCircular]);

                        // move to first
                        slot.SetAsFirstSibling();

                    }
                    else
                    {
                        if (_firstIndex == 0) return;

                        // reduce padding
                        LayoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                        // get slot
                        var slot = content.GetChild(content.childCount - 1);

                        // refresh slot
                        slot.GetComponent<ScrollRectSlot>().Init(_items[--_firstIndex]);

                        // move to first
                        slot.SetAsFirstSibling();
                    }
                }
            }
        }

        void OnValueChangedGridHorizontal()
        {
            // move all of the slots in first column to last column
            if (base.content.anchoredPosition.x < ((_firstIndex / _rowOrColumnCount) + 1) * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                int gap = (int)(base.content.anchoredPosition.x / (_slotWidthOrHeight + _spacing.x) + (_firstIndex / _rowOrColumnCount));

                for (int i = 0; i < gap * -1; i++)
                {
                    if (_circularLoop)
                    {
                        // increase padding
                        LayoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            _firstIndex++;

                            // calculate circular first index
                            _firstIndexCircular = ++_firstIndexCircular % Length;

                            // calculate circular last index
                            int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                            // get slot
                            var slot = content.GetChild(0);

                            // refresh slot
                            slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndexCircular]);

                            // move to last
                            slot.SetAsLastSibling();
                        }

                        _loop = _firstIndex / _rowOrColumnCount / Length;
                    }
                    else
                    {
                        // if there's nothing to show more, just finish
                        if (_firstIndex + LengthVisible >= Length) return;

                        // increase padding
                        LayoutGroup.padding.left += (int)(_slotWidthOrHeight + _spacing.x);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            // get index
                            int lastIndex = ++_firstIndex + LengthVisible - 1;

                            // get slot
                            var slot = content.GetChild(0);

                            // refresh first slot with last data to show
                            if (lastIndex < Length)
                            {
                                slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndex]);
                            }
                            // if there's nothing to show more, just inactivate the slot
                            else
                            {
                                slot.gameObject.SetActive(false);
                            }

                            // move to last
                            slot.SetAsLastSibling();
                        }
                    }
                }
            }

            // move all of the slots in last column to first column
            else if (base.content.anchoredPosition.x > (_firstIndex / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.x) * -1)
            {
                int gap = (int)(base.content.anchoredPosition.x / (_slotWidthOrHeight + _spacing.x) + (_firstIndex / _rowOrColumnCount + 1));

                for (int i = 0; i < gap; i++)
                {
                    if (_circularLoop)
                    {
                        // reduce padding
                        LayoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            _firstIndex--;

                            // calculate circular index
                            _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                            // get slot
                            var slot = content.GetChild(content.childCount - 1);

                            // refresh last slot with first data to show
                            slot.GetComponent<ScrollRectSlot>().Init(_items[_firstIndexCircular]);

                            // move to first
                            slot.SetAsFirstSibling();
                        }

                        _loop = _firstIndex / _rowOrColumnCount / Length;
                    }
                    else
                    {
                        if (_firstIndex == 0) return;

                        // reduce padding
                        LayoutGroup.padding.left -= (int)(_slotWidthOrHeight + _spacing.x);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            // get slot
                            var slot = content.GetChild(content.childCount - 1);

                            // refresh last slot with first data to show
                            slot.GetComponent<ScrollRectSlot>().Init(_items[--_firstIndex]);

                            // if inactivated, activate the slot
                            if (!slot.gameObject.activeInHierarchy) slot.gameObject.SetActive(true);

                            // move to first
                            slot.SetAsFirstSibling();
                        }
                    }
                }
            }
        }

        void OnValueChangedGridVertical()
        {
            // move all of the slotss in first row to last row
            if (base.content.anchoredPosition.y > ((_firstIndex / _rowOrColumnCount) + 1) * (_slotWidthOrHeight + _spacing.y))
            {
                int gap = (int)(base.content.anchoredPosition.y / (_slotWidthOrHeight + _spacing.y) - (_firstIndex / _rowOrColumnCount));

                for (int i = 0; i < gap; i++)
                {
                    if (_circularLoop)
                    {
                        // increase padding
                        LayoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            _firstIndex++;

                            // calculate circular first index
                            _firstIndexCircular = ++_firstIndexCircular % Length;

                            // calculate circular last index
                            int lastIndexCircular = (_firstIndexCircular + LengthVisible - 1) % Length;

                            // get slot
                            var slot = content.GetChild(0);

                            slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndexCircular]);

                            // move to last
                            slot.SetAsLastSibling();
                        }

                        _loop = _firstIndex / _rowOrColumnCount / Length;
                    }
                    else
                    {
                        // if there's nothing to show more, just finish
                        if (_firstIndex + LengthVisible >= Length) return;

                        // increase padding
                        LayoutGroup.padding.top += (int)(_slotWidthOrHeight + _spacing.y);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            // get index
                            int lastIndex = ++_firstIndex + LengthVisible - 1;

                            // get slot
                            var slot = content.GetChild(0);

                            // refresh first slot with last data to show
                            if (lastIndex < Length)
                            {
                                slot.GetComponent<ScrollRectSlot>().Init(_items[lastIndex]);
                            }
                            // if there's nothing to show more, just inactivate the slot
                            else
                            {
                                slot.gameObject.SetActive(false);
                            }

                            // move to last
                            slot.SetAsLastSibling();
                        }
                    }
                }
            }

            // move all of the slots in last row to first row
            else if (base.content.anchoredPosition.y < (_firstIndex / _rowOrColumnCount) * (_slotWidthOrHeight + _spacing.y))
            {
                int gap = (int)(base.content.anchoredPosition.y / (_slotWidthOrHeight + _spacing.y) - ((_firstIndex / _rowOrColumnCount) + 1));

                for (int i = 0; i < gap * -1; i++)
                {
                    if (_circularLoop)
                    {
                        // reduce padding
                        LayoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            _firstIndex--;

                            // calculate circular index
                            _firstIndexCircular = --_firstIndexCircular < 0 ? Length - 1 + (_firstIndexCircular + 1) % (Length - 1) : _firstIndexCircular;

                            // get slot
                            var slot = content.GetChild(content.childCount - 1);

                            // refresh last slot with first data to show
                            slot.GetComponent<ScrollRectSlot>().Init(_items[_firstIndexCircular]);

                            // move to first
                            slot.SetAsFirstSibling();
                        }

                        _loop = _firstIndex / _rowOrColumnCount / Length;
                    }
                    else
                    {
                        if (_firstIndex == 0) return;

                        // reduce padding
                        LayoutGroup.padding.top -= (int)(_slotWidthOrHeight + _spacing.y);

                        for (int j = 0; j < _rowOrColumnCount; j++)
                        {
                            // get slot
                            var slot = content.GetChild(content.childCount - 1);

                            // refresh last slot with first data to show
                            slot.GetComponent<ScrollRectSlot>().Init(_items[--_firstIndex]);

                            // if inactivated, activate the slot
                            if (!slot.gameObject.activeInHierarchy) slot.gameObject.SetActive(true);

                            // move to first
                            slot.SetAsFirstSibling();
                        }
                    }
                }
            }
        }
    }
}

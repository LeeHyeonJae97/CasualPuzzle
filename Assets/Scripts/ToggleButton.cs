using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using NaughtyAttributes;

public class ToggleButton : Selectable, IPointerClickHandler
{
    private enum ToggleTransition { ColorTint, SpriteSwap }

    [SerializeField] private ToggleTransition _toggleTransition;

    [SerializeField] private Color _onColor = Color.white;
    [SerializeField] private Color _offColor = Color.white;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    public UnityEvent<bool> onStateChanged;
    [SerializeField] private bool _isOnOnStart;

    private bool _isOn;
    public bool IsOn
    {
        get { return _isOn; }

        set
        {
            _isOn = value;
            switch (_toggleTransition)
            {
                case ToggleTransition.ColorTint:
                    targetGraphic.color = _isOn ? _onColor : _offColor;
                    break;

                case ToggleTransition.SpriteSwap:
                    (targetGraphic as Image).sprite = _isOn ? _onSprite : _offSprite;
                    break;
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        IsOn = _isOnOnStart;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsOn = !IsOn;
        onStateChanged.Invoke(value);
    }
}

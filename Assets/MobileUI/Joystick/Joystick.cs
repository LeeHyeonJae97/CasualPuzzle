using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 Direction
    {
        get
        {
            var position = _stick.anchoredPosition;

            return position.normalized * (position.magnitude / Radius);
        }
    }
    private float Radius { get { return _base.rect.width / 2; } }

    [SerializeField] [Range(0, 1f)] private float _deadZone;
    [SerializeField] private RectTransform _base;
    [SerializeField] private RectTransform _stick;
    private Canvas _canvas;
    private CanvasScaler _canvasScaler;
    private bool _pressed;

    public UnityAction<Vector2> onDragged;
    public UnityAction onDown;
    public UnityAction onUp;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvasScaler = GetComponentInParent<CanvasScaler>();
    }

    private void Update()
    {
        if (_pressed)
        {
            onDragged?.Invoke(Direction);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;

        onDown?.Invoke();

        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;

        onUp?.Invoke();

        // reset stick's position
        _stick.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;

        if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            position = (eventData.position - (Vector2)_canvas.worldCamera.WorldToScreenPoint(_base.position)) / (Screen.width / _canvasScaler.referenceResolution.x);
        }
        else
        {
            position = (eventData.position - (Vector2)_base.position) / _canvas.transform.lossyScale.x;
        }

        // set stick's position
        _stick.anchoredPosition = Bound(position, Radius);
    }

    private Vector2 Bound(Vector2 position, float radius)
    {
        // bound stick's position not to be in deadZone and get out of base of joystick
        if (position.sqrMagnitude < (radius * _deadZone) * (radius * _deadZone))
        {
            return Vector2.zero;
        }
        else if (position.sqrMagnitude < radius * radius)
        {
            return position;
        }
        else
        {
            return position.normalized * radius;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class CustomToggle : MonoBehaviour
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    public enum Transition { None, ColorTint, SpriteSwap }

    [SerializeField] private bool initOnStart;
    [SerializeField] private bool invokeOnInit;

    [Tooltip("On right when on")]
    [SerializeField] private bool isOn;
    public bool IsOn
    {
        set
        {
            isOn = value;
            Toggle(isOn);
        }
    }

    [SerializeField] private Transition transition;

    [SerializeField] private Image bkgImage;
    [SerializeField] private Color onBkgColor = Color.white, offBkgColor = Color.white;
    [SerializeField] private Sprite onBkgSprite, offBkgSprite;

    [SerializeField] private bool switchPosition;
    [SerializeField] private RectTransform handle;
    [SerializeField] private Vector2 onPos, offPos;
    [SerializeField] private float slideDuration;

    [SerializeField] private ToggleEvent onClick = new ToggleEvent();

    [SerializeField] private SettingsSO settings;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            isOn = !isOn;
            Toggle(isOn);
        });
    }

    private void Start()
    {
        if (initOnStart)
        {
            switch (transition)
            {
                case Transition.ColorTint:
                    bkgImage.color = isOn ? onBkgColor : offBkgColor;
                    break;
                case Transition.SpriteSwap:
                    bkgImage.sprite = isOn ? onBkgSprite : offBkgSprite;
                    break;
            }

            // Switch position
            if (switchPosition) handle.anchoredPosition = isOn ? onPos : offPos;

            // Additional events
            if (invokeOnInit) onClick.Invoke(isOn);
        }
    }

    private void Toggle(bool isOn)
    {
        switch (transition)
        {
            case Transition.ColorTint:
                bkgImage.color = isOn ? onBkgColor : offBkgColor;
                break;
            case Transition.SpriteSwap:
                bkgImage.sprite = isOn ? onBkgSprite : offBkgSprite;
                break;
        }

        // Switch position
        if (switchPosition) handle.DOAnchorPos(isOn ? onPos : offPos, slideDuration);

        // Additional events
        onClick.Invoke(isOn);
    }

    public void SetUI(bool value)
    {
        switch (transition)
        {
            case Transition.ColorTint:
                bkgImage.color = value ? onBkgColor : offBkgColor;
                break;
            case Transition.SpriteSwap:
                bkgImage.sprite = value ? onBkgSprite : offBkgSprite;
                break;
        }

        // Switch position
        if (switchPosition) handle.DOAnchorPos(value ? onPos : offPos, slideDuration);
    }

    public void AddListener(UnityAction<bool> onClick)
    {
        this.onClick.AddListener(onClick);
    }

    public void Invoke()
    {
        button.onClick.Invoke();
    }
}

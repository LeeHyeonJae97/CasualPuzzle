using UnityEngine;
using UnityEngine.UI;

public static class RectTransformExtension
{
    public static Vector2 GetSize(this RectTransform rectTransform)
    {
        var canvasScaler = rectTransform.GetComponentInParent<CanvasScaler>();

        if (canvasScaler == null)
        {
            return rectTransform.rect.size;
        }

        switch (canvasScaler.uiScaleMode)
        {
            case CanvasScaler.ScaleMode.ConstantPixelSize:
                {
                    return rectTransform.rect.size;
                }
            case CanvasScaler.ScaleMode.ScaleWithScreenSize:
                {
                    if (canvasScaler.enabled)
                    {
                        return rectTransform.rect.size;
                    }
                    else
                    {
                        float scale = 0;

                        switch (canvasScaler.screenMatchMode)
                        {
                            case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
                                {
                                    float kLogBase = 2;
                                    float logWidth = Mathf.Log(Screen.width / canvasScaler.referenceResolution.x, kLogBase);
                                    float logHeight = Mathf.Log(Screen.height / canvasScaler.referenceResolution.y, kLogBase);
                                    float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, canvasScaler.matchWidthOrHeight);
                                    scale = Mathf.Pow(kLogBase, logWeightedAverage);
                                    break;
                                }
                            case CanvasScaler.ScreenMatchMode.Expand:
                                {
                                    scale = Mathf.Min(Screen.width / canvasScaler.referenceResolution.x, Screen.height / canvasScaler.referenceResolution.y);
                                    break;
                                }
                            case CanvasScaler.ScreenMatchMode.Shrink:
                                {
                                    scale = Mathf.Max(Screen.width / canvasScaler.referenceResolution.x, Screen.height / canvasScaler.referenceResolution.y);
                                    break;
                                }
                        }
                        return rectTransform.rect.size / scale;
                    }
                }
            case CanvasScaler.ScaleMode.ConstantPhysicalSize:
                {
                    if (canvasScaler.enabled)
                    {
                        return rectTransform.rect.size;
                    }
                    else
                    {
                        float currentDpi = Screen.dpi;
                        float dpi = (currentDpi == 0 ? canvasScaler.fallbackScreenDPI : currentDpi);
                        float targetDPI = 1;
                        switch (canvasScaler.physicalUnit)
                        {
                            case CanvasScaler.Unit.Centimeters: targetDPI = 2.54f; break;
                            case CanvasScaler.Unit.Millimeters: targetDPI = 25.4f; break;
                            case CanvasScaler.Unit.Inches: targetDPI = 1; break;
                            case CanvasScaler.Unit.Points: targetDPI = 72; break;
                            case CanvasScaler.Unit.Picas: targetDPI = 6; break;
                        }
                        return rectTransform.rect.size / (dpi / targetDPI);
                    }
                }
            default:
                return Vector2.zero;
        }
    }
}
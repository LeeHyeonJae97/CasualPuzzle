using UnityEngine;

public static class TransformExtension
{
    public static void SetParent(this Transform tr, Transform parent, Vector3 localPosition)
    {
        tr.SetParent(parent);
        tr.localPosition = localPosition;
    }

    public static void SetParent(this Transform tr, Transform parent, Vector3 localPosition, Quaternion localRotation)
    {
        tr.SetParent(parent);
        tr.localPosition = localPosition;
        tr.localRotation = localRotation;
    }

    public static void SetParent(this Transform tr, Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
    {
        tr.SetParent(parent);
        tr.localPosition = localPosition;
        tr.localRotation = localRotation;
        tr.localScale = localScale;
    }
}
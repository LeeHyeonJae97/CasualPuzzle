using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationBadgePosition { LeftBottom, LeftTop, RightTop, RightBottom }

public class NotificationBadge : MonoBehaviour
{
    public void Set(Transform target, NotificationBadgePosition position)
    {
        transform.SetParent(target);
        transform.SetAsLastSibling();

        SetPosition(position);
    }

    private void SetPosition(NotificationBadgePosition position)
    {
        var rectTr = transform as RectTransform;

        rectTr.pivot = new Vector2(0.5f, 0.5f);
        switch (position)
        {
            case NotificationBadgePosition.LeftBottom:
                rectTr.anchorMin = new Vector2(0f, 0f);
                rectTr.anchorMax = new Vector2(0f, 0f);
                break;

            case NotificationBadgePosition.LeftTop:
                rectTr.anchorMin = new Vector2(0f, 1f);
                rectTr.anchorMax = new Vector2(0f, 1f);
                break;

            case NotificationBadgePosition.RightTop:
                rectTr.anchorMin = new Vector2(1f, 1f);
                rectTr.anchorMax = new Vector2(1f, 1f);
                break;

            case NotificationBadgePosition.RightBottom:
                rectTr.anchorMin = new Vector2(1f, 0f);
                rectTr.anchorMax = new Vector2(1f, 0f);
                break;
        }
        rectTr.anchoredPosition = Vector2.zero;
    }
}

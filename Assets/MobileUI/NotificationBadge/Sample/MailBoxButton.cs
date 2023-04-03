using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxButton : MonoBehaviour
{
    [SerializeField] private NotificationBadge _prefab;

    private void Awake()
    {
        Instantiate(_prefab).Set(transform, NotificationBadgePosition.RightTop);
    }
}

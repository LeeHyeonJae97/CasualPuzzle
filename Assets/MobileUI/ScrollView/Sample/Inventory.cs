using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MobileUI
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemSlot _slotPrefab;
        [SerializeField] private ExtendedScrollRect _scrollRect;
        private List<List<IScrollRectItem>> _data;
        private int _tabIndex;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _tabIndex = 0;

                _scrollRect.SetItems(_data[_tabIndex]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _tabIndex = 1;

                _scrollRect.SetItems(_data[_tabIndex]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _data[_tabIndex].Insert(2, new ItemData(10));

                _scrollRect.SetItems(_data[_tabIndex]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _data[_tabIndex].RemoveAt(1);

                _scrollRect.SetItems(_data[_tabIndex]);
            }
        }

        protected void Initialize()
        {
            _data = new List<List<IScrollRectItem>>(2);

            _data.Add(new List<IScrollRectItem>(20));
            _data.Add(new List<IScrollRectItem>(5));

            for (int i = 0; i < _data.Capacity; i++)
            {
                for (int j = 0; j < _data[i].Capacity; j++)
                {
                    _data[i].Add(new ItemData(j));
                }
            }

            _scrollRect.Initialize(_data[0].Cast<IScrollRectItem>().ToList(), _slotPrefab);
        }
    }
}

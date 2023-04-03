using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileUI
{
    public class ExtendedHorizontalLayoutGroup : HorizontalLayoutGroup
    {
        public enum RuntimeMode { Enabled, Disabled, Destory }

        [SerializeField] private RuntimeMode _runtimeMode;

        protected override void Start()
        {
            base.Start();

            if (!Application.isPlaying) return;

            StartCoroutine(CoCheckRuntime());

            IEnumerator CoCheckRuntime()
            {
                yield return null;

                switch (_runtimeMode)
                {
                    case RuntimeMode.Enabled:
                        break;

                    case RuntimeMode.Disabled:
                        {
                            if (TryGetComponent<ContentSizeFitter>(out var filter)) filter.enabled = false;
                            enabled = false;
                            break;
                        }

                    case RuntimeMode.Destory:
                        {
                            if (TryGetComponent<ContentSizeFitter>(out var filter)) Destroy(filter);
                            Destroy(this);
                            break;
                        }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileUI
{
    public class UIBehaviour : MonoBehaviour
    {
        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return UIManager.Instance.StartCoroutine(routine);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class BoxView : MonoBehaviour
    {
        public Transform _placeToPut;
        public List<InteractiveObjectViev> _stuff;

        //public Action <bool> Open { get; set; }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractiveObjectViev contactView))
            {
                //Open?.Invoke(true);
            }
        }
        private void openBox()
        {
            StartCoroutine(DelayOpenBox(_stuff));
        }

        private IEnumerator DelayOpenBox(List<InteractiveObjectViev> stuffInBox)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < stuffInBox.Count; i++)
            {
                yield return new WaitForSeconds(1);
                stuffInBox[i].gameObject.SetActive(true);
            }
            StopCoroutine(DelayOpenBox(stuffInBox));
        }
    }
}
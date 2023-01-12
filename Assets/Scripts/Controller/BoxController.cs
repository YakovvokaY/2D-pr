using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class BoxController : MonoBehaviour
    {
        private Transform _placeToPutT;
        private List<InteractiveObjectViev> _stuffInBox = new List<InteractiveObjectViev>();

        private float _delay = 1;
        private bool _openFlag = false;

        public BoxController (List<InteractiveObjectViev> stuffInBox, Transform placeToPutT, BoxView box)
        {
            _placeToPutT = placeToPutT;
            _stuffInBox = stuffInBox;

            box.Open += openBox;
        }

        private void openBox(bool flag)
        {
            _openFlag = flag;
            if (_openFlag == true)
            {
                StartCoroutine(DelayOpenBox(_stuffInBox));
            }
        }

        private IEnumerator DelayOpenBox(List<InteractiveObjectViev> stuffInBox)
        {
            yield return new WaitForSeconds(_delay);
            for (int i = 0; i< stuffInBox.Count; i++)
            {
                yield return new WaitForSeconds(_delay);
                stuffInBox[i].gameObject.SetActive(true);
            }
            StopCoroutine(DelayOpenBox(_stuffInBox));
        }
    }
}
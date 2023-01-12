using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{

    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectViev _playerView;
        [SerializeField] private CanoneView _canoneView;
        private PlayerController _playerController;
        private CanonController _canonController;
        private EmitterController _emitterController;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _canonController = new CanonController(_canoneView._muzzleT, _playerView.transform);
            _emitterController = new EmitterController(_canoneView._bullets, _canoneView._emitterT);
        }
        void Update()
        {
            _playerController.Update();
            _canonController.Update();
            _emitterController.Update();
        }
    }
}
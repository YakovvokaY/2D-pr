using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{

    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CanoneView _canoneView;
        private PlayerController _playerController;
        private CanonController _canonController;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _canonController = new CanonController(_canoneView._muzzleT, _playerView.transform);
        }
        void Update()
        {
            _playerController.Update();
            _canonController.Update();
        }
    }
}
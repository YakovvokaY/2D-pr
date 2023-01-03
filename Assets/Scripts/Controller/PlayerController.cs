using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class PlayerController 
    {
        private AnimConfig _config;
        private SpriteAnimController _playerAnimator;
        private LevelObjectView _plaerView;

        private Transform _playerT;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 3f;
        private float _animationSpeed = 10f;
        private float _movingTreashold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpTreashold = 1f;
        private float _g = -9.8f;
        private float _groundLevel = 0.5f;
        private float _yVelocity;

        public PlayerController(LevelObjectView player)
        {
            _config = Resources.Load<AnimConfig>("SpriteAnimCFG");
            _playerAnimator = new SpriteAnimController(_config);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _plaerView = player;
            _playerT = player._transform;
        }

        private void MoveTowards()
        {
            _playerT.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }
        public bool IsGrounded()
        {
            return _playerT.position.y <= _groundLevel && _yVelocity <= 0;
        }
        public void Update()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;

            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreashold;

            if (_isMoving)
            {
                MoveTowards();
            }
            if(IsGrounded())
            {
                _playerAnimator.StartAnimation(_plaerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);
                if (_isJump&&_yVelocity<=0)
                {
                    _yVelocity = _jumpForce;
                }
                else if(_yVelocity<0)
                {
                    _yVelocity = 0;
                    _playerT.position = new Vector3(_playerT.position.x, _groundLevel, _playerT.position.z);
                }
            }
            else
            {
                if(Mathf.Abs(_yVelocity)>_jumpTreashold)
                {
                    _playerAnimator.StartAnimation(_plaerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);

                }
                _yVelocity += _g * Time.deltaTime;
                _playerT.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class PlayerController 
    {
        #region filds
        private AnimConfig _config;
        private SpriteAnimController _playerAnimator;
        private ContactPooler _contactPooler;
        private LevelObjectView _plaerView;

        private Transform _playerT;
        private Rigidbody2D _rb;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 2000f;
        private float _animationSpeed = 10f;
        private float _movingTreashold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpTreashold = 1f;
        private float _yVelocity;
        private float _xVelocity;

        private int _health = 100;
        #endregion 

        public PlayerController(InteractiveObjectViev player)
        {
            _config = Resources.Load<AnimConfig>("SpriteAnimCFG");
            _playerAnimator = new SpriteAnimController(_config);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _plaerView = player;
            _playerT = player._transform;
            _rb = player._rb;
            _contactPooler = new ContactPooler(_plaerView._collider);

            player.TakeDamage += TakeBullet;
        }

        public void TakeBullet(DamageView bullet)
        {
            _health -= bullet.DamagePoint;
        }

        private void MoveTowards()
        {
            _xVelocity = (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            _rb.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }
        public void Update()
        {
            if(_health<=0)
            {
                _health = 0;
                _plaerView._spriteRenderer.enabled = false;
                _plaerView.gameObject.SetActive(false);
            }

            _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _yVelocity = _rb.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreashold;
            _playerAnimator.StartAnimation(_plaerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

            if (_isMoving)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity,_rb.velocity.y);
            }
            if(_contactPooler.IsGrounded)
            {
                
                if (_isJump && _yVelocity<= _jumpTreashold)
                {
                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
                else if(_yVelocity<0)
                {

                }
            }
            else
            {
                if(Mathf.Abs(_yVelocity)>_jumpTreashold)
                {
                    _playerAnimator.StartAnimation(_plaerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }
        }
    }
}

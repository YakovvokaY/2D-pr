using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class BulletController
    {
        private Vector3 _velosity;
        private DamageView _viev;

        public BulletController(DamageView viev)
        {
            _viev = viev;
            Active(false);
        }

        public void Active(bool val)
        {
            _viev.gameObject.SetActive(val);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velosity = velocity;
            float _angle = Vector3.Angle(Vector3.left, _velosity);
            Vector3 _axis = Vector3.Cross(Vector3.left, _velosity);
            _viev.transform.rotation = Quaternion.AngleAxis(_angle,_axis);
        }
        public void Trow(Vector3 position, Vector3 velocity)
        {
            _viev.transform.position = position;
            SetVelocity(_velosity);
            _viev._rb.velocity = Vector2.zero;
            _viev._rb.angularVelocity = 0;
            Active(true);
            _viev._rb.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}
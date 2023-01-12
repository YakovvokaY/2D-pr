using System;
using UnityEngine;

namespace MVCMPlatformer
{
    public class InteractiveObjectViev : LevelObjectView
    {
        public Action<DamageView> TakeDamage { get; set; }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out DamageView contactView))
            {
                TakeDamage?.Invoke(contactView);
            }
        }
    }
}
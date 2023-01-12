using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class DamageView : LevelObjectView
    {
        [SerializeField] private int _damagePoint = 10;
        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }
    }
}
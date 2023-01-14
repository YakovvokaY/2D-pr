using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MVCMPlatformer
{
    public class SentryView : LevelObjectView
    {
        public List<Transform> patrolPoints;
        public Pathfinding.AIDestinationSetter AIDS;
        public float targetDistance = 8;
        public float treashPatrolDistance = 0.1f;
    }
}
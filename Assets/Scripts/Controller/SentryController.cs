using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class SentryController 
    {
        private List<Transform> _patrolPoints;
        private Transform _playerTransform;
        private SentryView _sentry;

        private int i=0;

        public SentryController(Transform playerTransform, SentryView sentry)
        {
            _playerTransform = playerTransform;
            _sentry = sentry;
            _patrolPoints = _sentry.patrolPoints;
        }
        public void Update()
        {
            if ((_playerTransform.position - _sentry._transform.position).magnitude > _sentry.targetDistance)
            {
                if ((_patrolPoints[i].position - _sentry._transform.position).magnitude <= _sentry.treashPatrolDistance)
                {
                    i = i + 1;
                    if (i >= _patrolPoints.Count)
                    {
                        i = 0;
                    }
                    _sentry.AIDS.target = _patrolPoints[i];
                }
                else
                {
                    _sentry.AIDS.target = _patrolPoints[i];
                }
            }
            else
            {
                _sentry.AIDS.target = _playerTransform;
            }
        }
    }
}
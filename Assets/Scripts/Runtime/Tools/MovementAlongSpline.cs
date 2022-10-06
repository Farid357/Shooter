using System;
using UnityEngine;

namespace Shooter.Tools
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class MovementAlongSpline : MonoBehaviour
    {
        [SerializeField, Min(0.02f)] private float _speed = 1.5f;

        private ISpline _spline;
        private Vector3 _nextPoint = Vector3.zero;

        private bool HasReachedPoint => transform.position == _nextPoint;

        public void Init(ISpline spline)
        {
            _spline = spline ?? throw new ArgumentNullException(nameof(spline));
            _nextPoint = _nextPoint == Vector3.zero && _spline.CanGetNextPoint() ? _spline.GetNextPoint() : _nextPoint;
        }

        private void Update()
        {
            if (HasReachedPoint)
            {
                if (_spline.CanGetNextPoint())
                    _nextPoint = _spline.GetNextPoint();
            }

            else
            {
                Move();
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPoint, _speed * Time.deltaTime);
        }
    }
}
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Tools
{
    public sealed class Spline : MonoBehaviour, ISpline
    {
        [SerializeField, Title("Spline Data", TitleAlignment = TitleAlignments.Centered)] private List<Transform> _points;
        private int _dataIndex;
        private Transform _lastCreatedPoint;
        
        public Vector3 GetNextPoint()
        {
            if (CanGetNextPoint() == false)
                throw new InvalidOperationException($"{nameof(CanGetNextPoint)} is false");
            
            _dataIndex++;
            return _points[_dataIndex].position;
        }

        public bool CanGetNextPoint() => _points.Count != _dataIndex + 1;
        
        public void Reset() => _dataIndex = 0;

        [Button(ButtonSizes.Medium, ButtonStyle.CompactBox, Name = "Create Point")]
        public void CreatePoint()
        {
            var primitive = new GameObject("Spline Point")
            {
                transform =
                {
                    position = Camera.main.transform.position,
                    parent = transform
                }
            };

            _lastCreatedPoint = primitive.transform;
            _points.Add(_lastCreatedPoint);
        }

        [Button(ButtonSizes.Medium, ButtonStyle.CompactBox, Name = "Delete Last Created Point"), GUIColor(1, 0, 0, 1)]
        public void DeleteLastCreatedPoint()
        {
            if (_lastCreatedPoint is not null)
                DestroyImmediate(_lastCreatedPoint.gameObject);
        }
    }
}
using Shooter.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraLook : SerializedMonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _sensivity = 1.5f;
        [SerializeField, Min(0.1f)] private float _smoothTime = 0.1f;
        [SerializeField] private float _minYClamp = -90;
        [SerializeField] private float _maxYClamp = 90;

        [SerializeField] private ICharacterTransform _character;

        private Vector3 _rotation;
        private Vector3 _smoothRotation;

        private void Update()
        {
            _rotation.x += Input.GetAxis("Mouse X") * _sensivity;
            _rotation.y += Input.GetAxis("Mouse Y") * _sensivity;
            _rotation.y = Mathf.Clamp(_rotation.y, _minYClamp, _maxYClamp);
            _smoothRotation = ToSmooth(_rotation);
            transform.rotation = Quaternion.Euler(-_smoothRotation.y, _smoothRotation.x, 0f);
            _character.Rotate(new Vector3(0f, _rotation.x, 0f));
        }

        private Vector3 ToSmooth(Vector3 rotation)
        {
            var smooth = new Vector3
            {
                x = ToSmooth(rotation.x, _smoothRotation.x),
                y = ToSmooth(rotation.y, _smoothRotation.y)
            };
            return smooth;
        }

        private float ToSmooth(float rotation, float target)
        {
            var a = 0f;
            return Mathf.SmoothDamp(rotation, target, ref a, _smoothTime);
        }
    }
}
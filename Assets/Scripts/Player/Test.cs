using UnityEngine;
using UnityEngine.InputSystem;

namespace CrazyHammer.Core
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private float _maxHandsDistance = 4f;
        [SerializeField] private float _movementSensitivity = 0.01f;
        [SerializeField] private float _mass = 1f;
        [SerializeField] public float lerpSpeed = 8.0f;

        private Vector3 _mouseStartPosition;
        private Vector3 _handsInitialPosition;
        private bool _pressed;
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            if (!Mouse.current.leftButton.isPressed)
            {
                if (_pressed)
                    _pressed = false;
                return;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _pressed = true;
                _mouseStartPosition = Mouse.current.position.ReadValue();
                _handsInitialPosition = transform.position;
            }

            var mousePositionOffset = _mouseStartPosition - (Vector3)Mouse.current.position.ReadValue();
            mousePositionOffset *= _movementSensitivity;

            var calculatedHandsPosition = _handsInitialPosition - mousePositionOffset;

            var offset = Vector3.ClampMagnitude(calculatedHandsPosition - _player.transform.position, _maxHandsDistance);
            var targetPosition = _player.transform.position + offset;
            
            float smoothDampTime = Mathf.Sqrt(_mass);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothDampTime, Mathf.Infinity, Time.deltaTime * lerpSpeed);
        }

    }
}

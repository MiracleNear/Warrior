using System;
using UnityEngine;

namespace InputSystem
{
    public class MobileInput : MonoBehaviour
    {
        public event Action<Vector2> MovePressed;
        public event Action MoveUnpressed;

        [SerializeField] private float _holdTimeToActivate;

        private float _pressingTime;
        private bool _isMovePressedInPreviousFrame;
        private Joystick _joystick;

        private void Awake()
        {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            if (_joystick.IsMoving)
            {
                if (!_isMovePressedInPreviousFrame)
                {
                    _pressingTime = Time.time;
                    _isMovePressedInPreviousFrame = true;
                }
                else
                {
                    if(Time.time - _pressingTime <= _holdTimeToActivate) return;

                    MovePressed?.Invoke(_joystick.Direction);
                }
                
            }
            else if(_isMovePressedInPreviousFrame)
            {
                MoveUnpressed?.Invoke();
                _isMovePressedInPreviousFrame = false;
            }
        }
    }
}
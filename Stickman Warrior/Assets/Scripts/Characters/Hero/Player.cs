using AnimationSystem;
using Characters.Enemies;
using InputSystem;
using UnityEngine;

namespace Characters.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : Character
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedSmothingTime;
        
        private MobileInput _mobileInput;
        private Vector3 _direction;
        private Vector3 _veloctityDamp;

        protected override void Init()
        {
            _mobileInput = FindObjectOfType<MobileInput>();
            
            SetTargetForLook<Enemy>();
        }

        private void OnEnable()
        {
            _mobileInput.MovePressed += OnMovePressed;
            _mobileInput.MoveUnpressed += OnMoveUnpressed;
        }

        private void OnDisable()
        {
            _mobileInput.MovePressed -= OnMovePressed;
            _mobileInput.MoveUnpressed -= OnMoveUnpressed;
        }

        private void OnMoveUnpressed()
        {
            _direction = Vector3.zero;
        }

        private void OnMovePressed(Vector2 input)
        {
            _direction = new Vector3(input.x, 0f, input.y).normalized;
        }
        
        
        private void FixedUpdate()
        {
            TurnTowardsTarget();
            Move();
        }

        private void Move()
        {
            Vector3 targetVelocity = (transform.forward * _direction.z + transform.right * _direction.x) * _speed;
            
            Vector3 smoothedVelocity = Vector3.SmoothDamp(Rigidbody.velocity, targetVelocity, ref _veloctityDamp, _speedSmothingTime);

            Rigidbody.velocity = smoothedVelocity;
        }
    }
}
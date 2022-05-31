using InputSystem;
using UnityEngine;

namespace AnimationSystem
{
    public class PlayerAnimationController : AnimationController
    {
        [SerializeField] private float _dampTimeForVelocityParameter;
        
        private MobileInput _mobileInput;

        protected override void Init()
        {
            _mobileInput = FindObjectOfType<MobileInput>();

            base.Init();
        }

        protected override void Enable()
        {
            _mobileInput.MovePressed += OnMovePressed;
            _mobileInput.MoveUnpressed += OnMoveUnpressed;
        }

        protected override void Disable()
        {
            _mobileInput.MovePressed -= OnMovePressed;
            _mobileInput.MoveUnpressed -= OnMoveUnpressed;
        }

        private void OnMoveUnpressed()
        { 
            Animator.SetBool(PlayerAnimationParameter.States.Run, false);
        }

        private void OnMovePressed(Vector2 input)
        {
            Vector3 velocity = new Vector3(input.x, 0f, input.y).normalized;

            Animator.SetBool(PlayerAnimationParameter.States.Run, true);
            
            Animator.SetFloat(PlayerAnimationParameter.States.VelocityX, velocity.x, _dampTimeForVelocityParameter, Time.deltaTime);
            Animator.SetFloat(PlayerAnimationParameter.States.VelocityZ, velocity.z, _dampTimeForVelocityParameter, Time.deltaTime);
        }
    }
}
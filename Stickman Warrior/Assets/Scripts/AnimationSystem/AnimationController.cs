using UnityEngine;

namespace AnimationSystem
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        protected Animator Animator;

        private void Awake()
        {
            Init();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void OnEnable()
        {
            Enable();
        }

        protected virtual void Init()
        {
            Animator = GetComponent<Animator>();
        }

        protected virtual void Enable()
        {
            
        }

        protected virtual void Disable()
        {
            
        }

        public void StartHitAnimation()
        {
            Animator.SetTrigger(PlayerAnimationParameter.States.Hit);
        }
    }
}
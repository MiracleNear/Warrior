using AnimationSystem;
using UnityEngine;

namespace Battle
{
    [RequireComponent(typeof(AnimationController))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private AttackZoneDetector _attackZoneDetector;

        private AnimationController _animationController;
        private bool _isAttack;

        private void Awake()
        {
            _animationController = GetComponent<AnimationController>();
        }

        private void StartAttack()
        {
            _isAttack = true;
        }
        
        private void Hit()
        {
            if (_attackZoneDetector.TryGetIntersection(out Collider collider))
            {
                print(collider.transform.parent.name);
            }
        }

        private void EndAttack()
        {
            _isAttack = false;
        }

        private void FixedUpdate()
        {
            if (TryAttack())
            {
                _animationController.StartHitAnimation();
            }
        }

        private bool TryAttack()
        {
            if (!_attackZoneDetector.CheckIntersectionWithTargetHitArea())
            {
                return false;
            }

            if (_isAttack)
            {
                return false;
            }

            return true;
        }
    }
}
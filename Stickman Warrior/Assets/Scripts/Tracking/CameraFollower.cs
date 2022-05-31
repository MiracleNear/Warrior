using UnityEngine;
using Characters.Hero;

namespace Tracking
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Transform _target;
        private Vector3 _pastFrameDirection;

        private void OnValidate()
        {
            _target = FindObjectOfType<Player>().transform;
    
            transform.position = _target.transform.position + _offset;
            transform.rotation = Quaternion.LookRotation(-_offset, Vector3.up);
        }

        private void Awake()
        {
            _target = FindObjectOfType<Player>().transform;
        }
        
        
        private void LateUpdate()
        {
            FollowTarget();
            LookAtTarget(-_offset);
            RememberCurrentFrameDirection();
        }

        private void FollowTarget()
        {
            transform.position = _target.position + CalculateTargetOffset();
        }

        private Vector3 CalculateTargetOffset()
        {
            return _offset = CalculateRotationFrom(_target.forward, _pastFrameDirection) * _offset;
        }

        private Quaternion CalculateRotationFrom(Vector3 from, Vector3 to)
        {
            float angle = Vector3.SignedAngle(to, from, Vector3.up);
            
            return Quaternion.AngleAxis(angle, Vector3.up);
        }

        private void LookAtTarget(Vector3 lookDirection)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

        private void RememberCurrentFrameDirection()
        {
            _pastFrameDirection = _target.forward;
        }
    }
}

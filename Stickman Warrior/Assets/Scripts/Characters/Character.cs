using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Character : MonoBehaviour
    {
        protected Rigidbody Rigidbody;

        private Transform _target;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Init();
        }

        protected abstract void Init();

        protected void SetTargetForLook<T>() where T : Character
        {
            _target = FindObjectOfType<T>().transform;
        }
        
        protected void TurnTowardsTarget()
        {
            Rigidbody.MoveRotation(CalculateRotationToLookTarget(_target.position));
        }
        
        private Quaternion CalculateRotationToLookTarget(Vector3 targetPosition)
        {
            Vector3 directionLook = (targetPosition - transform.position).normalized;

            directionLook.y = 0;
            
            return Quaternion.LookRotation(directionLook, Vector3.up);   
        }
    }
}

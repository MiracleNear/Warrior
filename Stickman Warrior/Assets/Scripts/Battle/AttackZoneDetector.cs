using UnityEngine;

namespace Battle
{
    public class AttackZoneDetector : MonoBehaviour
    {
        [SerializeField] private Vector3 _hitAreaSize;
        [SerializeField] private Vector3 _hitAreaPosition;
        [SerializeField] private LayerMask _availableZone;

        private Vector3 _halfAreaSize => _hitAreaPosition / 2f;
        private Vector3 _centerHitArea => transform.parent.TransformPoint(_hitAreaPosition);
        private Quaternion _orientationHitArea => Quaternion.LookRotation(transform.parent.forward);
        
        public bool CheckIntersectionWithTargetHitArea()
        {
            return Physics.CheckBox(_centerHitArea, _halfAreaSize, _orientationHitArea, _availableZone);
        }

        public bool TryGetIntersection(out Collider collider)
        {
            collider = null;

            Collider[] hits = Physics.OverlapBox(_centerHitArea, _halfAreaSize,
                _orientationHitArea, _availableZone);

            if (hits.Length > 0)
            {
                collider = hits[0];
                return true;
            }

            return false;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_centerHitArea, _hitAreaSize);
            Gizmos.color = Color.clear;
        }
    }
}

using UnityEngine;

namespace Runner.Movement {
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 offset;
        
        void Start() {
            offset = transform.position - target.position;
        }
    
        private void LateUpdate() {
            if (null == target) return;
            transform.position = target.position + offset;
        }
    }
}
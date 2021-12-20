using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Movement {
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float ropeSpring = 4f;
        [SerializeField] private float ropeDuration = 0.75f;
        [SerializeField] private float stopSpeed = 5f;
        [SerializeField] private Transform handTransform;
        [SerializeField] private Animator animator;

        public bool isMoving = true;
        private LineRenderer lineRenderer;
        private SpringJoint ropeJoint;
        private Vector3 ropeAnchor;
        private bool isHanging = false;

        void Start() {
            ropeJoint = GetComponent<SpringJoint>();
            lineRenderer = GetComponent<LineRenderer>();

            GameManager.Instance.OnGameStateChange += GameManagerOnGameStateChange;
        }

        void Update()
        {
            switch (GameManager.Instance.currentState) {
                case GameState.Scoring:
                    speed = Mathf.Lerp(speed, 0, stopSpeed * Time.deltaTime);
                    animator.SetFloat("Run Speed", speed / 10);
                    UpdatePosition();
                    HandleRope();
                    break;
                case GameState.Playing:
                    UpdatePosition();
                    HandleRope();
                    break;
                default:
                    return;
            }
        }

        void OnDestroy() {
            GameManager.Instance.OnGameStateChange -= GameManagerOnGameStateChange;
        }

        void GameManagerOnGameStateChange(GameState newState) {
            if (newState == GameState.Playing) {
                animator.SetTrigger("Trigger Run");
            }
            else if (newState == GameState.Won) {
                animator.SetTrigger("Trigger Idle");
            }
            else if (newState == GameState.Lost) {
                animator.SetTrigger("Die");
                ResetRope();
                Destroy(this.gameObject, 2f);
            }
        }

        void UpdatePosition() {
            if (!isMoving) return;
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        void HandleRope() {
            if (GameManager.Instance.ropeUses > 0 && !isHanging && Input.GetMouseButtonDown(0)) {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach (RaycastHit hitInfo in hits) {
                    if (!hitInfo.transform.CompareTag("CanGrapple")) continue;
                    // We use height center of the object hit to simplify player choice
                    if (Vector3.Distance(transform.position, hitInfo.transform.position) >= 25f) continue;
                    Vector3 hitCenter = hitInfo.transform.position;
                    ropeAnchor = new Vector3(transform.position.x, hitCenter.y, hitInfo.point.z);
                    ThrowRope();
                }
            }
            if (isHanging) {
                Invoke("ResetRope", ropeDuration);
                RenderRope();
                UpdateRotation();
                if (Input.GetMouseButtonUp(0)) {
                    CancelInvoke("ResetRope");
                    ResetRope();
                }
            }
        }
        
        void UpdateRotation() {
            var rotation = Quaternion.LookRotation (ropeAnchor - transform.position);
            rotation.y = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 1);
        }

        void RenderRope() {
            lineRenderer.SetPosition(0, handTransform.position);
            lineRenderer.SetPosition(1, ropeAnchor);
        }

        void ThrowRope() {
            animator.SetBool("Has Rope", true);
            ropeJoint.connectedAnchor = ropeAnchor;
            ropeJoint.spring = ropeSpring;
            isHanging = true;
            GameManager.Instance.ReduceRope(1);
        }

        void ResetRope() {
            if (!isHanging) return;

            transform.rotation = Quaternion.Euler(Vector3.zero);
            animator.SetBool("Has Rope", false);
            ropeJoint.spring = 0f;
            isHanging = false;
            lineRenderer.SetPosition(0, handTransform.position);
            lineRenderer.SetPosition(1, handTransform.position);
        }

        static Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Animation Events
        public void OnCollision() {
            animator.SetTrigger("Knock Down");
            ResetRope();
            isMoving = false;
        }

        public void GetUp() {
            animator.SetTrigger("Get Up");
        }

        public void OnGotUp() {
            animator.SetTrigger("Trigger Run");
            isMoving = true;
        }
    }
}

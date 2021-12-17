using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Movement {
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float ropeSpring = 4f;
        [SerializeField] private float stopSpeed = 5f;

        public bool isMoving = true;
        private LineRenderer lineRenderer;
        private SpringJoint ropeJoint;
        private Vector3 ropeAnchor;
        private bool isHanging = false;

        void Start() {
            ropeJoint = GetComponent<SpringJoint>();
            lineRenderer = GetComponent<LineRenderer>();
        }

        void Update()
        {
            switch (GameManager.Instance.currentState) {
                case GameState.Scoring:
                    speed = Mathf.Lerp(speed, 0, stopSpeed * Time.deltaTime);
                    UpdatePosition();
                    HandleRope();

                    // We force the rope to break in the yard stick portion of the game
                    if (isHanging) Invoke("ResetRope", 1f);
                    break;
                case GameState.Playing:
                    UpdatePosition();
                    HandleRope();
                    break;
                default:
                    return;
            }
        }

        private void UpdatePosition() {
            if (!isMoving) return;
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        void HandleRope() {
            if (GameManager.Instance.ropeUses > 0 && !isHanging && Input.GetMouseButtonDown(0)) {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach (RaycastHit hitInfo in hits) {
                    if (!hitInfo.transform.CompareTag("CanGrapple")) continue;
                    // We use height center of the object hit to simplify player choice
                    Vector3 hitCenter = hitInfo.transform.position;
                    ropeAnchor = new Vector3(transform.position.x, hitCenter.y, hitInfo.point.z);
                    ThrowRope();
                }
            }
            if (isHanging) {
                RenderRope();
                if (Input.GetMouseButtonUp(0)) {
                    ResetRope();
                }
            }
        }

        void RenderRope() {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, ropeAnchor);
        }

        void ThrowRope() {
            ropeJoint.connectedAnchor = ropeAnchor;
            ropeJoint.spring = ropeSpring;
            isHanging = true;
            GameManager.Instance.ReduceRope(1);
        }

        void ResetRope() {
            if (!isHanging) return;

            ropeJoint.spring = 0f;
            isHanging = false;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

        static Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

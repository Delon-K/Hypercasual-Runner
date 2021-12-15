using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Movement {
    public class StopOnCollision : MonoBehaviour
    {
        // In case the player collides with the border of a platform
        // We stop the automatic movement to the right to sooth the collision

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                other.GetComponent<Mover>().isMoving = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) {
                other.GetComponent<Mover>().isMoving = true;
            }
        }
    }
}

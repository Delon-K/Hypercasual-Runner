using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Movement {
    public class StopOnCollision : MonoBehaviour
    {
        // In case the player collides with the border of a platform
        // We stop the automatic movement to the right to sooth the collision
        // And we also add a little force backwards to separate player and platform        
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                StartCoroutine(UpdatePlayerMoving(other.gameObject, false, 0f));
                other.GetComponent<Rigidbody>().AddForce(Vector3.back);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                StartCoroutine(UpdatePlayerMoving(other.gameObject, true, 0.25f));
            }
        }

        IEnumerator UpdatePlayerMoving(GameObject player, bool newMoving, float delay) {
            yield return new WaitForSeconds(delay);
            player.GetComponent<Mover>().isMoving = newMoving;
        }
    }
}

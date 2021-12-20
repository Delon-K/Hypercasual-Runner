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
                // StartCoroutine(UpdatePlayerMoving(other.gameObject, false));
                other.GetComponent<Rigidbody>().AddForce(Vector3.back * 20f);
                other.GetComponent<Mover>().OnCollision();
            }
        }
    }
}

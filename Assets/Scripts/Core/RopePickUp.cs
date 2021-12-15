using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Core {
    public class RopePickUp : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                print("Player Picks Rope");
                GameManager.Instance.ropeUses++;
                Destroy(gameObject);
            }
        }
    }
}

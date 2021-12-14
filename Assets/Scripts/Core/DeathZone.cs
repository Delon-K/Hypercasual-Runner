using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Core {
    public class DeathZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player")) {
                print("Player Detected");
                GameManager.Instance.currentState = GameManager.GameState.Finish;
            }
        }
    }
}

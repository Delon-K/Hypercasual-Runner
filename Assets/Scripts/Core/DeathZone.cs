using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Core {
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                print("Player Detected");
                GameManager.Instance.ChangeState(GameState.Lost);
            }
        }
    }
}

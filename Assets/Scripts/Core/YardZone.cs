using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Core {
    public class YardZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                GameManager.Instance.ChangeState(GameState.Scoring);
            }
        }
    }
}

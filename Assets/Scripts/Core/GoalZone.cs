using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Core {
    public class GoalZone : MonoBehaviour
    {
        [SerializeField] private int extraScore = 10;
        private bool hasScored = false;

        private void OnTriggerStay(Collider other) {
            if (!hasScored && other.gameObject.CompareTag("Player") && other.GetComponent<Rigidbody>().velocity.magnitude == 0) {
                hasScored = true;
                GameManager.Instance.AddScore(extraScore);
                GameManager.Instance.ChangeState(GameState.Won);
            }
        }
    }
}

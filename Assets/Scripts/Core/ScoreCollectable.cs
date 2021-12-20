using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Core {
    public class ScoreCollectable : MonoBehaviour
    {
        [SerializeField] private int extraScore = 10;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                GameManager.Instance.AddScore(extraScore);
                Destroy(this.gameObject);
            }
        }
    }
}

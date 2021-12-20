using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Movement {
    public class GetUpAnimationEvent : MonoBehaviour
    {
        private GameObject player;

        void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public void GetUp() {
            player.GetComponent<Mover>().GetUp();
        }

        public void OnGotUp() {
            player.GetComponent<Mover>().OnGotUp();
        }
    }
}

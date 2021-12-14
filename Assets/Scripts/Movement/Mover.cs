using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Managers;

namespace Runner.Movement {
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        void Update()
        {
            if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;
            
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }
}

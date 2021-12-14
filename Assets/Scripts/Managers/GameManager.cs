using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runner.Managers {
    public class GameManager : MonoBehaviour
    {
        public enum GameState { Start, Playing, Finish };
        public GameState currentState;
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    go.AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            _instance = this;
        }

        void Start() {
            currentState = GameState.Start;
        }

        void Update() {
            if (currentState == GameState.Start && Input.GetKeyDown(KeyCode.S)) currentState = GameState.Playing;
            if (currentState == GameState.Finish && Input.GetKeyDown(KeyCode.R)) ReloadScene();
        }

        void ReloadScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

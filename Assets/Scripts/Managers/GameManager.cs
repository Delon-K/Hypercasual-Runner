using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { 
    Start,
    Playing,
    Scoring,
    Lost,
    Won 
}

namespace Runner.Managers {
    public class GameManager : MonoBehaviour
    {
        public GameState currentState;
        public int ropeUses = 0;
        public float score = 1000;
        public float scoreMultiplier = 2f;

        public delegate void GameStateChange(GameState newState);
        public event GameStateChange OnGameStateChange;
        private static GameManager _instance;
        public static GameManager Instance
        {
            get {
                if (_instance == null) {
                    GameObject go = new GameObject("GameManager");
                    go.AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        void Awake() {
            _instance = this;
        }

        void Start() {
            ChangeState(GameState.Start);
        }

        void Update() {
            if (currentState == GameState.Start && Input.GetMouseButtonDown(0)) ChangeState(GameState.Playing);

            if (currentState == GameState.Playing) {
                ReduceScore(Time.deltaTime * scoreMultiplier);
            }
        }

        public void ReloadScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public float AddScore(float amount) {
            score += amount;
            UIManager.Instance.UpdateScoreText(score);
            return score;
        }

        public float ReduceScore(float amount) {
            score -= amount;
            UIManager.Instance.UpdateScoreText(score);
            return score;
        }

        public int AddRope(int amount) {
            ropeUses += amount;
            UIManager.Instance.UpdateRopeText(ropeUses);
            return ropeUses;
        }

        public int ReduceRope(int amount) {
            ropeUses -= amount;
            UIManager.Instance.UpdateRopeText(ropeUses);
            return ropeUses;
        }

        public void ChangeState(GameState newState) {
            currentState = newState;

            switch (currentState) {
                case GameState.Start:
                    break;
                case GameState.Playing:
                    break;
                case GameState.Scoring:
                    break;
                case GameState.Won:
                    break;
                case GameState.Lost:
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(currentState), newState, null);
            }
        
            if (null != OnGameStateChange) {
                OnGameStateChange(currentState);
            }
        }
    }
}

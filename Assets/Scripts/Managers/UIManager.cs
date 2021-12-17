using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.UI;
using System;

namespace Runner.Managers {
    public class UIManager : MonoBehaviour
    {
        private LevelUI levelUI;
        private static UIManager _instance;
        public static UIManager Instance
        {
            get {
                if (_instance == null) {
                    GameObject go = new GameObject("UIManager");
                    go.AddComponent<UIManager>();
                }
                return _instance;
            }
        }

        void Awake() {
            _instance = this;
        }

        void Start() {
            levelUI = GetComponent<LevelUI>();
            GameManager.Instance.OnGameStateChange += GameManagerOnGameStateChange;
        }

        void OnDestroy() {
            GameManager.Instance.OnGameStateChange -= GameManagerOnGameStateChange;
        }

        private void GameManagerOnGameStateChange(GameState newState) {
            switch (newState) {
                case GameState.Start:
                    break;
                case GameState.Playing:
                    levelUI.LevelStarted();
                    break;
                case GameState.Scoring:
                    break;
                case GameState.Won:
                    levelUI.LevelFinished(true, GameManager.Instance.score);
                    break;
                case GameState.Lost:
                    levelUI.LevelFinished(false, GameManager.Instance.score);
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        public void UpdateScoreText(float score) {
            levelUI.UpdateScoreText(score);
        }

        public void UpdateRopeText(int ropeUses) {
            levelUI.UpdateRopeText(ropeUses);
        }
    }
}

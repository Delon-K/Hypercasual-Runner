using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.UI;

namespace Runner.Managers {
    public class UIManager : MonoBehaviour
    {
        private LevelUI levelUI;
        private MainMenuUI mainMenuUI;
        
        private static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }

        void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } 
            else _instance = this;
        }

        void Start() {
            GameManager.Instance.OnGameStateChange += GameManagerOnGameStateChange;
            levelUI = GetComponent<LevelUI>();
            mainMenuUI = GetComponent<MainMenuUI>();
            mainMenuUI?.MainMenuLoaded(GameManager.Instance.levelsData);
        }

        void OnDestroy() {
            GameManager.Instance.OnGameStateChange -= GameManagerOnGameStateChange;
        }

        private void GameManagerOnGameStateChange(GameState newState) {
            switch (newState) {
                case GameState.MainMenu:
                    mainMenuUI.MainMenuLoaded(GameManager.Instance.levelsData);
                    break;
                case GameState.Start:
                    levelUI.LevelLoaded(GameManager.Instance.GetLevelScores());
                    break;
                case GameState.Playing:
                    levelUI.LevelStarted();
                    break;
                case GameState.Scoring:
                    break;
                case GameState.Won:
                    bool hasNextLevel = (null != GameManager.Instance.FindNextStageLevel());
                    levelUI.LevelFinished(true, GameManager.Instance.score, hasNextLevel);
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

        public void LoadSceneFromMenu(bool isFirst) {
            LevelData levelClicked = mainMenuUI.GetLevelShownByPosition(isFirst);
            GameManager.Instance.LoadSceneByName(levelClicked.namePath);
        }

        public void LoadNextStage() {
            GameManager.Instance.LoadNextScene();
        }

        public void LoadMainMenu() {
            GameManager.Instance.LoadSceneByName("MainMenu");
        }

        public void ReloadScene() {
            GameManager.Instance.ReloadScene();
        }
    }
}

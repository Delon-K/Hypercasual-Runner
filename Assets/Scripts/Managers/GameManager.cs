using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { 
    MainMenu,
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
        public JsonArray<LevelData> levelsData;

        private int twoStarScore;
        private int threeStarScore;
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } 
            else _instance = this;
            DontDestroyOnLoad(this.gameObject);

            levelsData = JsonReader.ReadLevelsFromResources();
        }

        void Start() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name == "MainMenu") ChangeState(GameState.MainMenu);
            else {
                score = 1000;
                ropeUses = 0;
                LevelData levelData = System.Array.Find(levelsData.Items, levelData => levelData.namePath == scene.name);
                twoStarScore = levelData.twoStar;
                threeStarScore = levelData.threeStar;
                ChangeState(GameState.Start);
            }
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

        public void LoadSceneByName(string name) {
            SceneManager.LoadScene(name);
        }

        public void LoadNextScene() {
            LoadSceneByName(FindNextStageLevel().namePath);
        }

        public LevelData FindNextStageLevel() {
            LevelData currentLevelData = System.Array.Find(levelsData.Items, levelData => levelData.namePath == SceneManager.GetActiveScene().name);
            if (null == currentLevelData) return null;

            LevelData nextLevelData = System.Array.Find(levelsData.Items, (levelData => levelData.levelNumber == currentLevelData.levelNumber + 1));
            if (null == nextLevelData) return null;
            else return nextLevelData;
        }

        public System.Tuple<int, int> GetLevelScores() {
            return System.Tuple.Create(twoStarScore, threeStarScore);
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
            if (null != OnGameStateChange) {
                OnGameStateChange(currentState);
            }
        }
    }
}

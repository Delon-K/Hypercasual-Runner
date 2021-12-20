using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Runner.Managers;

namespace Runner.UI {
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject winLosePanel;
        [SerializeField] private Text ropeText;
        [SerializeField] private Image twoStarFail;
        [SerializeField] private Image twoStarWin;
        [SerializeField] private Image threeStarFail;
        [SerializeField] private Image threeStarWin;
        [SerializeField] private Button nextLevelButton;

        private int twoStarScore;
        private int threeStarScore;

        void Start() {
            if (null != scoreText) scoreText.text = null;

            GameManager.Instance.OnGameStateChange += GameManagerOnGameStateChange;
            GameManager.Instance.OnRopeUsesChange += GameManagerOnRopeUsesChange;

            LevelLoaded(GameManager.Instance.GetLevelScores());
        }

        void OnDestroy() {
            GameManager.Instance.OnGameStateChange -= GameManagerOnGameStateChange;
            GameManager.Instance.OnRopeUsesChange -= GameManagerOnRopeUsesChange;
        }

        void GameManagerOnGameStateChange(GameState newState) {
            if (newState == GameState.Playing) LevelStarted();
            else if (newState == GameState.Won) {
                bool hasNextLevel = (null != GameManager.Instance.FindNextStageLevel());
                LevelFinished(true, GameManager.Instance.score, hasNextLevel);
            }
            else if (newState == GameState.Lost) LevelFinished(false, GameManager.Instance.score);
        }

        void GameManagerOnRopeUsesChange(int ropeUses) {
            if (null == ropeText) {
                Debug.LogError("Rope Text not set up");
                return;
            }

            ropeText.text = "Ropes: " + ropeUses.ToString();
        }

        void Update() {
            UpdateScoreText(GameManager.Instance.score);
        }

        public void LevelLoaded(System.Tuple<int, int> scores) {
            twoStarScore = scores.Item1;
            threeStarScore = scores.Item2;
            Debug.Log(twoStarScore);
            Debug.Log(threeStarScore);
            startPanel.SetActive(true);
            scoreText.gameObject.SetActive(false);
            ropeText.gameObject.SetActive(false);
        }

        public void LevelStarted() {
            startPanel.SetActive(false);
            scoreText.gameObject.SetActive(true);
            ropeText.gameObject.SetActive(true);
        }

        public void UpdateScoreText(float score) {
            if (null == scoreText) {
                Debug.LogError("Score Text not set up");
                return;
            }

            scoreText.text = "Score:\n" + Mathf.Round(score).ToString();
        }

        public void LevelFinished(bool hasWon, float finalScore, bool hasNextLevel = false) {
            winLosePanel.SetActive(true);
            scoreText.gameObject.SetActive(false);
            ropeText.gameObject.SetActive(false);

            winLosePanel.transform.GetChild(0).gameObject.SetActive(hasWon ? true : false);
            winLosePanel.transform.GetChild(1).gameObject.SetActive(hasWon ? false : true);

            if (finalScore >= twoStarScore) {
                twoStarFail.gameObject.SetActive(false);
                twoStarWin.gameObject.SetActive(true);
                Debug.Log(finalScore >= twoStarScore);
            }
            if (finalScore >= threeStarScore) {
                threeStarFail.gameObject.SetActive(false);
                threeStarWin.gameObject.SetActive(true);
            }
            if (hasNextLevel && hasWon) {
                nextLevelButton.gameObject.SetActive(true);
            }
            winLosePanel.GetComponentInChildren<Text>().text = Mathf.Round(finalScore).ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.UI {
    [RequireComponent(typeof(Runner.Managers.UIManager))]
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject winLosePanel;
        [SerializeField] private Text ropeText;

        void Start() {
            if (null != scoreText) scoreText.text = null;
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

        public void UpdateRopeText(int ropeUses) {
            if (null == ropeText) {
                Debug.LogError("Rope Text not set up");
                return;
            }

            ropeText.text = "Ropes: " + ropeUses.ToString();
        }

        public void LevelFinished(bool hasWon, float finalScore) {
            winLosePanel.SetActive(true);
            scoreText.gameObject.SetActive(false);
            ropeText.gameObject.SetActive(false);

            winLosePanel.transform.GetChild(0).gameObject.SetActive(hasWon ? true : false);
            winLosePanel.transform.GetChild(1).gameObject.SetActive(hasWon ? false : true);
            winLosePanel.GetComponentInChildren<Text>().text = Mathf.Round(finalScore).ToString();
        }
    }
}

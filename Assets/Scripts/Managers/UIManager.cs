using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Managers {
    public class UIManager : MonoBehaviour
    {
        public Text scoreText;
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
            if (null != scoreText) scoreText.text = null;
        }

        void Update() {
            UpdateScore(GameManager.Instance.score);
        }

        public void UpdateScore(float score) {
            if (null == scoreText) {
                Debug.LogError("Score Text not set up");
                return;
            }

            scoreText.text = "Score:\n" + Mathf.Round(score).ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Runner.Managers;

namespace Runner.UI {
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private int pageSize = 2;
        [SerializeField] private Slider levelSelectSlider;
        [SerializeField] private Button firstLevelButton;
        [SerializeField] private Button secondLevelButton;

        private int totalPageNumber;
        private int totalItems;
        private int currentPage;
        private JsonArray<LevelData> levelDataList;
        private LevelData firstLevelData;
        private LevelData secondLevelData;

        private void Start() {
            MainMenuLoaded(GameManager.Instance.levelsData);
        }

        public void MainMenuLoaded(JsonArray<LevelData> levels) {
            levelDataList = levels;
            firstLevelData = levelDataList.Items[0];
            secondLevelData = levelDataList.Items[1];

            currentPage = 1;
            totalItems = levelDataList.Items.Length;
            totalPageNumber = Mathf.CeilToInt(totalItems / pageSize);
            levelSelectSlider.maxValue = totalPageNumber;
        }

        public void ChangeSliderValue() {
            currentPage = (int)levelSelectSlider.value;

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Mathf.Min(startIndex + pageSize - 1, totalItems - 1);

            for (int i = startIndex; i < endIndex; i++) {
                firstLevelData = levelDataList.Items[startIndex];
                firstLevelButton.GetComponentInChildren<Text>().text = firstLevelData.levelNumber.ToString();
                secondLevelData = levelDataList.Items[endIndex];
                secondLevelButton.GetComponentInChildren<Text>().text = secondLevelData.levelNumber.ToString();
            }
        }

        public void LoadSceneFromMenu(bool isFirst) {
            LevelData levelClicked = isFirst ? firstLevelData : secondLevelData;
            GameManager.Instance.LoadSceneByName(levelClicked.namePath);
        }

        public void ExitButton() {
            GameManager.Instance.CloseGame();
        }
    }
}

using UnityEngine;

[System.Serializable]
public class JsonReader
{
    const string path = "levels";

    public static JsonArray<LevelData> ReadLevelsFromResources() {
        string jsonString = UnityEngine.Resources.Load<TextAsset>(path).ToString();

        JsonArray<LevelData> levelData = JsonUtility.FromJson<JsonArray<LevelData>>("{\"Items\":" + jsonString + "}");

        return levelData;
    }

}
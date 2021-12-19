[System.Serializable]
public class LevelData
{
    public string namePath;
    public int levelNumber;
    public int twoStar;
    public int threeStar;
}

[System.Serializable]
public class JsonArray<T>
{
    public T[] Items;
}
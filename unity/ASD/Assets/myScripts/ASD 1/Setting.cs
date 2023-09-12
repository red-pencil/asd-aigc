using System.Collections.Generic;

[System.Serializable]
public class StorysSetting {
    public List<StorySetting> storySetting = new List<StorySetting>();
}

[System.Serializable]
public class StorySetting {
    public int emotionIndex;

    public string emotion;
    public int levelIndex;
    public string level;
    public string keyword;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateIO : MonoBehaviour
{
    public StoryTemplateArray storyArray;
    public StoryTemplateItem storyItem;
    public StoryTemplatePage storyPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/TemplateLib.json").ToString();
        storyArray= JsonUtility.FromJson<StoryTemplateArray>(jsonContent);

        Debug.Log("<<< Template Read! >>>");
    }

    public void SaveJson()
    {
        string jsonContent = JsonUtility.ToJson(storyArray);
        System.IO.File.WriteAllText("./Assets/MyData/TemplateLib.json", jsonContent);

        Debug.Log("<<< Template Saved! >>>");
    }
}


[System.Serializable]
public class StoryTemplateArray {
    public List<StoryTemplateItem> library = new List<StoryTemplateItem>();

}
[System.Serializable]
public class StoryTemplateItem {
    public int itemIndex;
    public int emotionIndex;
    public string title;
    public string intro;
    public List<StoryTemplatePage> content = new List<StoryTemplatePage>();
}

[System.Serializable]
public class StoryTemplatePage {
    public int emotionIndex;
    public int pageIndex;
    public string title;
    public string body;
    public string folder;
    //public int[] xy = [0,0];
}

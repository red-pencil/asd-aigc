using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateIO : MonoBehaviour
{
    public StoryTemplates storyArray;
    public StoryTemplate story;
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
        storyArray = JsonUtility.FromJson<StoryTemplates>(jsonContent);

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
public class StoryTemplates {
    public List<StoryTemplate> templateLib = new List<StoryTemplate>();
}

[System.Serializable]
public class StoryTemplate {
    public int emotionIndex;
    public int templateIndex;
    public string title;
    public string body;
    public string folder;
    //public int[] xy = [0,0];
}

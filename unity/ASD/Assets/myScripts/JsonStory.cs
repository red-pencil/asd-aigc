using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JsonStory : MonoBehaviour
{
    private string storyContent;
    public AllPagesInfo allPagesInfo;
    void Awake()
    {
        // var userFolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        // apiKeyContent = File.ReadAllText(userFolderPath + "/.openai/auth.json").ToString();
        storyContent = System.IO.File.ReadAllText("./Assets/MyData/StoryData.json").ToString();

        allPagesInfo = JsonUtility.FromJson<AllPagesInfo>(storyContent);
        Debug.Log(allPagesInfo.pageInfo[0].prompt);
 
    }

    void Start()
    {

    }

}

[System.Serializable]
public class AllPagesInfo {
    public List<PageInfo> pageInfo = new List<PageInfo>();
}

[System.Serializable]
public class PageInfo {
    public int index;
    public string title;
    public string body;
    public string prompt;

}
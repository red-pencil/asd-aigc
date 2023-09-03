using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptIO : MonoBehaviour
{
    public string promptFull;

    public Template prompt;
    
    void Awake()
    {
        OpenTemplateJson();
    }
    // Start is called before the first frame update
    void Start()
    {
        // FindKeyword(promptFull, "ppt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTemplateJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/Template.json").ToString();
        prompt = JsonUtility.FromJson<Template>(jsonContent);

        Debug.Log("<<< Template Read! >>>");
        Debug.Log(jsonContent);
        
        StitchPromptFull();
        
    }

    public string StitchPromptFull()
    {
        promptFull = prompt.level_1 + prompt.basicInfoStart + prompt.age + prompt.age_gender + prompt.gender + prompt.gender_language + prompt.language + prompt.language_like + prompt.like + prompt.like_disklike + prompt.dislike + prompt.disklike_keyword + prompt.keyword + prompt.keyword_emotion1 + prompt.emotion1 + prompt.emotion1_emotion2 + prompt.emotion2 + prompt.emotion2_reaction + prompt.reaction + prompt.basicInfoEnd + prompt.format;
        // + prompt.level_2 + prompt.level_3;
        
        Debug.Log("Full Prompt:\n" + promptFull);

        return promptFull;
    }

    public void OpenAllJson()
    {
        prompt = JsonUtility.FromJson<Template>(System.IO.File.ReadAllText("./Assets/MyData/Template.json").ToString());

        Debug.Log("<<< Template Read! >>>");
        

        StorySetting setting = JsonUtility.FromJson<StorySetting>(System.IO.File.ReadAllText("./Assets/MyData/Setting.json").ToString());

        prompt.emotion1 = setting.emotion;
        prompt.emotion2 = setting.emotion;

        Debug.Log("<<< Setting Read! >>>");
        

        ChildrenProfile profile = JsonUtility.FromJson<ChildrenProfile>(System.IO.File.ReadAllText("./Assets/MyData/Profile.json").ToString());

        prompt.gender = profile.childProfile[0].gender;

        Debug.Log("<<< Profile Read! >>>");

        StitchPromptFull();
        
    }

    public void FindKeyword(string sourceContent, string targetWord)
    {
        List<int> indexArray = new List<int>();
        int indexCurrent = 0;
        
        if (sourceContent.Contains(targetWord))
        {
           
            for (int i = 0; i < sourceContent.Length; )
            {
                
                indexCurrent = sourceContent.IndexOf(targetWord, i);
                
                if (indexCurrent == -1)
                {
                    break;
                } 
                else
                {
                    Debug.Log("Found at " + indexCurrent);
                    indexArray.Add(indexCurrent);
                    i = indexCurrent + 1;
                }
                
            }
           
        }

    }


}

[System.Serializable]
public class Prompt
{


}


[System.Serializable]
public class Template
{
    public string level_1, basicInfoStart, age, age_gender, gender, gender_language, language, language_like, like, like_disklike, dislike, disklike_keyword, keyword, keyword_emotion1, emotion1, emotion1_emotion2, emotion2, emotion2_reaction, reaction, basicInfoEnd, format, level_2, level_3;
}

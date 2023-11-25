using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public class PromptIO : MonoBehaviour
{
    public string promptFull;
    public string promptEasy;

    public PromptTemplate prompt;
    public bool usingStoryTemplate;
    public int indexTemplate = 0;
    public StoryTemplateArray storyTemplateArray;
    public string promptStoryTemplate;
    
    public GameObject AIObject;
    [SerializeField] private bool autoSendTemplate;
    void Awake()
    {
        OpenPromptTemplateJson();
        if (autoSendTemplate)
        {
            AIObject.GetComponent<MyChatGPT>().promptTemplate = promptFull;
            // AIObject.GetComponent<MyChatGPT>().SendReplyAuto(promptFull);
            AIObject.GetComponent<MyChatGPT>().SendReplyAuto();
        }
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

    public void OpenPromptTemplateJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/PromptTemplate.json").ToString();
        prompt = JsonUtility.FromJson<PromptTemplate>(jsonContent);

        Debug.Log("<<< Prompt Template Read! >>>");
        Debug.Log(jsonContent);
        
        if (usingStoryTemplate)
        {
            StitchPersonalInfo();
        }
        else
        {
            StitchPromptFull();
        }
        
        
    }

    public void OpenStoryTemplateJson(int openIndex = 0)
    {
        string temp = System.IO.File.ReadAllText("./Assets/MyData/TemplateLib.json").ToString();
        storyTemplateArray = JsonUtility.FromJson<StoryTemplateArray>(temp);
        promptStoryTemplate = "";
        
        for(int i = 0; i < storyTemplateArray.library[openIndex].content.Count; i++ )
        {
            StoryTemplatePage storyTemplatePage =  new StoryTemplatePage();
            storyTemplatePage = storyTemplateArray.library[openIndex].content[i];
            promptStoryTemplate = promptStoryTemplate + "Slide " + (storyTemplatePage.pageIndex + 1).ToString() + ": " + storyTemplatePage.title + ". \n" + "Content: " + storyTemplatePage.body + "\n\n";

        }
    }

    public string StitchPersonalInfo()
    {
        string promptPersonalInfo;
        promptPersonalInfo = prompt.basicInfoStart + prompt.age + prompt.age_gender + prompt.gender + prompt.gender_language + prompt.language + prompt.language_like + prompt.like + prompt.like_disklike + prompt.dislike + prompt.disklike_keyword + prompt.keyword + prompt.keyword_emotion1 + prompt.emotion1 + prompt.emotion1_emotion2 + prompt.emotion2 + prompt.emotion2_reaction + prompt.reaction + prompt.basicInfoEnd;
        promptPersonalInfo = promptPersonalInfo + "\n\n";
        string promptPreamble = "Now, I want you to do the following: I will give you the basic info of the children and I will give you the story you have created. I want you to personalize the selected story with the information I provided. Remember not to make any significant changes to the storyline. \n Below is the info of the kid:\n" ;

        OpenStoryTemplateJson(indexTemplate);
    
        promptFull = promptPreamble + promptPersonalInfo + promptStoryTemplate;
        Debug.Log("Full Prompt:\n" + promptFull);

        return promptFull;
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
        prompt = JsonUtility.FromJson<PromptTemplate>(System.IO.File.ReadAllText("./Assets/MyData/PromptTemplate.json").ToString());

        Debug.Log("<<< Prompt Template Read! >>>");
        

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
public class PromptTemplate
{
    public string level_1, basicInfoStart, age, age_gender, gender, gender_language, language, language_like, like, like_disklike, dislike, disklike_keyword, keyword, keyword_emotion1, emotion1, emotion1_emotion2, emotion2, emotion2_reaction, reaction, basicInfoEnd, format, level_2, level_3;
}

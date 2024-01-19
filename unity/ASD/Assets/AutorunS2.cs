using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutorunS2 : MonoBehaviour
{
    public GameObject LoadStoryObject, PageSwtichObject, AiDrawObject, LevelSwitchObject;
    public MetaChoice currentChoice = new MetaChoice();

    // Start is called before the first frame update
    void Start()
    {
        OpenJson();

        LoadStoryObject.GetComponent<StoryIO>().templateName = currentChoice.storyChoice;
        AiDrawObject.GetComponent<TemplateDraw>().templateName = currentChoice.storyChoice;
        AiDrawObject.GetComponent<FaceSwap>().LoadCurTex(currentChoice.avatarChoice);
        LevelSwitchObject.GetComponent<LevelSwtich>().SwitchLevel(currentChoice.levelChoice);


        LoadStoryObject.GetComponent<StoryIO>().OpenJson();
        PageSwtichObject.GetComponent<storyPageSwitch>().LoadStory();
        AiDrawObject.GetComponent<TemplateDraw>().ReadFromJson();
        AiDrawObject.GetComponent<TemplateDraw>().DrawFromTemplate();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/currentChoice.json").ToString();
        currentChoice = JsonUtility.FromJson<MetaChoice>(jsonContent);

        Debug.Log("<<< Choice Read! >>>");
    }
}

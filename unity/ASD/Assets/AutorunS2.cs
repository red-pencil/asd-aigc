using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutorunS2 : MonoBehaviour
{
    public GameObject LoadStoryObject, PageSwtichObject, AiDrawObject; 

    // Start is called before the first frame update
    void Start()
    {
        LoadStoryObject.GetComponent<StoryIO>().OpenJson();
        PageSwtichObject.GetComponent<storyPageSwitch>().LoadStory();
        AiDrawObject.GetComponent<TemplateDraw>().ReadFromJson();
        AiDrawObject.GetComponent<TemplateDraw>().DrawFromTemplate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

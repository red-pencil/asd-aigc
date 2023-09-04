using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadStory : MonoBehaviour
{
    public GameObject loadStory;
    public List<string> promptArray = new List<string>();
    public List<InputField> inputFieldArray = new List<InputField>();
    public AllPagesInfo allStorysInfo;
    // Start is called before the first frame update
    void Start()
    {
        ReadStoryFromJson();
        FillPromptInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStoryFromJson()
    {
        allStorysInfo = loadStory.GetComponent<JsonStory>().allPagesInfo;
        foreach (PageInfo pageInfo in allStorysInfo.pageInfo)
        {
            if (pageInfo.prompt != "")
            {
                promptArray.Add(pageInfo.prompt);
            }
        }

    }
    
    public void FillPromptInput()
    {
        for(int i = 0; i < Mathf.Min(promptArray.Count, inputFieldArray.Count); i++)
        {
            inputFieldArray[i].text = promptArray[i];

        }
    }
}

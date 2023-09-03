using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public class ParseReply : MonoBehaviour
{
    public GameObject replyObject;
    public string reply;
    public string divider;
    public List<int> indexArray;
    public List<string> storyPages;
    public List<string[]> story;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DividePage(string sourceContent, string title, string prompt, string body)
    {
        story = new List<string[]>();
        int keyword1Index, keyword2Index, keyword3Index;
        keyword1Index = sourceContent.IndexOf(title);
        keyword2Index = sourceContent.IndexOf(prompt);
        keyword3Index = sourceContent.IndexOf(body);

        // story.Add(new string[4] {sourceContent.Substring(0, keyword1Index), 
        //     sourceContent.Substring(keyword1Index, keyword2Index-keyword1Index), 
        //     sourceContent.Substring(keyword2Index, keyword3Index-keyword2Index),
        //     sourceContent.Substring(keyword3Index, sourceContent.Length)});
    
    }

    public void DivideReply()
    {
        reply = replyObject.GetComponent<MyChatGPT>().messageReply;
        FindKeyword(reply, divider);
        storyPages = new List<string>();
        
        for (int i = 0; i < indexArray.Count - 1; i++)
        {
            storyPages.Add( reply.Substring( indexArray[i], (indexArray[i+1]-indexArray[i]) ) );
            Debug.Log(storyPages[storyPages.Count-1]);
        }
        DividePage(storyPages[1], "故事标题", "描述图片的prompt", "气泡框文字");
    }

    public void FindKeyword(string sourceContent, string targetWord)
    {
        indexArray = new List<int>();
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

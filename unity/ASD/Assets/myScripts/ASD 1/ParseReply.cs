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
    // public string[][] story2 = new string[3][];
    public List<string[]> story = new List<string[]>();
    public string titleDivider, promptDivider, bodyDivider;
    
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

        Debug.Log(keyword1Index.ToString() + " " + 
        keyword2Index.ToString() + " " +  
        keyword3Index.ToString());

        if (keyword1Index == -1 || keyword2Index == -1 || keyword3Index == -1)
        {
            Debug.Log("Missing Keyword(s)");

        }
        else
        {
            Debug.Log("title: " + sourceContent.Substring(keyword1Index+title.Length, keyword2Index-keyword1Index-title.Length) );
            Debug.Log("prompt: " + sourceContent.Substring(keyword2Index+prompt.Length, keyword3Index-keyword2Index-prompt.Length) );
            Debug.Log("body: " + sourceContent.Substring(keyword3Index+body.Length, sourceContent.Length-keyword3Index-body.Length) );


        }
      
        story.Add(new string[4] {sourceContent.Substring(0, keyword1Index), 
            sourceContent.Substring(keyword1Index+title.Length, keyword2Index-keyword1Index-title.Length), 
            sourceContent.Substring(keyword2Index+prompt.Length, keyword3Index-keyword2Index-prompt.Length),
            sourceContent.Substring(keyword3Index+body.Length, sourceContent.Length-keyword3Index-body.Length)});
    
    }

    public void DivideReply()
    {
        // read from object
        // reply = replyObject.GetComponent<MyChatGPT>().messageReply;
        // read from json
        // replyObject.GetComponent<MyChatGPTIO>().OpenJson;
        reply = replyObject.GetComponent<ChatGPTRecordIO>().messages.records[0].content;
        Debug.Log("Full Script:\n" + reply);
        
        FindKeyword(reply, divider);
        storyPages = new List<string>();
        
        for (int i = 0; i < indexArray.Count - 1; i++)
        {
            storyPages.Add( reply.Substring( indexArray[i], (indexArray[i+1]-indexArray[i]) ) );
            Debug.Log(storyPages[storyPages.Count-1]);
        }
            storyPages.Add( reply.Substring( indexArray[^1], ((reply.Length-1)-indexArray[^1]) ) );
            Debug.Log(storyPages[storyPages.Count-1]);
        // DividePage(storyPages[1], titleDivider, promptDivider, bodyDivider);
        // do it in StoryIO
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
                    // Debug.Log("Found at " + indexCurrent);
                    indexArray.Add(indexCurrent);
                    i = indexCurrent + 1;
                }
                
            }
           
        }

    }
}

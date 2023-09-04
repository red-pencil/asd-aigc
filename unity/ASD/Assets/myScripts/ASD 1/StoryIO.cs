using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIO : MonoBehaviour
{
    public StoryScript slides;
    public PageScript slide;
    public GameObject ParseObject;
    public string titleDivider, promptDivider, bodyDivider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (string, string, string, string) DividePage(string sourceContent, string title, string prompt, string body)
    {
        
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
            return ("", "", "", "");
        }
        else
        {
            Debug.Log("title: " + sourceContent.Substring(keyword1Index+title.Length, keyword2Index-keyword1Index-title.Length) );
            Debug.Log("prompt: " + sourceContent.Substring(keyword2Index+prompt.Length, keyword3Index-keyword2Index-prompt.Length) );
            Debug.Log("body: " + sourceContent.Substring(keyword3Index+body.Length, sourceContent.Length-keyword3Index-body.Length) );

            return (sourceContent.Substring(0, keyword1Index), sourceContent.Substring(keyword1Index+title.Length, keyword2Index-keyword1Index-title.Length), sourceContent.Substring(keyword2Index+prompt.Length, keyword3Index-keyword2Index-prompt.Length), sourceContent.Substring(keyword3Index+body.Length, sourceContent.Length-keyword3Index-body.Length));
        }
      
        
    
    }

    public void ReadFromParse() 
    {
        slides = new StoryScript();
        List<string> storyOriginal = ParseObject.GetComponent<ParseReply>().storyPages;
     
        for (int i = 0; i < storyOriginal.Count; i++)
        {
            slide = new PageScript();
            slide.index = i;
            string leftover;
            (leftover, slide.title, slide.prompt, slide.body) = DividePage(storyOriginal[i], titleDivider, promptDivider, bodyDivider);
            // slide.title = storyOriginal[i][1];
            // slide.prompt = storyOriginal[i][2];
            // slide.body = storyOriginal[i][3];

            slides.pages.Add(slide);
        }

        
        SaveJson();
        
    }

    public void OpenJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/StoryScript.json").ToString();
        slides = JsonUtility.FromJson<StoryScript>(jsonContent);

        Debug.Log("<<< Story Read! >>>");
    }

    public void SaveJson()
    {
        string jsonContent = JsonUtility.ToJson(slides);
        System.IO.File.WriteAllText("./Assets/MyData/StoryScript.json", jsonContent);

        Debug.Log("<<< Story Saved! >>>");
    }

}

[System.Serializable]
public class StoryScript {
    public List<PageScript> pages = new List<PageScript>();
}

[System.Serializable]
public class PageScript {
    public int index;
    public string title;
    public string body;
    public string prompt;
}
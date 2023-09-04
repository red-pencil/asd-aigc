using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class storyPageSwitch : MonoBehaviour
{
    public List<GameObject> imageArray = new List<GameObject>();
    public int activePageIndex = 0;
    public AllPagesInfo allStorysInfo;
    public GameObject AIScriptOject;
    public GameObject pageTitle, pageBody;
    [SerializeField] private GameObject storyOject;
    [SerializeField] private StoryScript storyScript;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStory()
    {
        foreach(GameObject image in imageArray)
        {
            image.SetActive(false);
        }
        
        imageArray[activePageIndex].SetActive(true);

        // allStorysInfo = AIScriptOject.GetComponent<ReadStory>().allStorysInfo;
        storyScript = storyOject.GetComponent<StoryIO>().slides;

        // pageTitle.GetComponent<TMP_Text>().text = "Demo";
        // Debug.Log(allStorysInfo.pageInfo.Count);
        DisplayTitle(activePageIndex);
    }

    public void pagePrev()
    {
        if (activePageIndex > 0)
        {
            imageArray[activePageIndex].SetActive(false);
            activePageIndex = activePageIndex - 1;
            imageArray[activePageIndex].SetActive(true);

            DisplayTitle(activePageIndex);
        }
        else 
        {
            Debug.Log("Home");
        }
    }

    public void pageNext()
    {
        if (activePageIndex < imageArray.Count - 1)
        {
            imageArray[activePageIndex].SetActive(false);
            activePageIndex = activePageIndex + 1;
            imageArray[activePageIndex].SetActive(true);

            DisplayTitle(activePageIndex);
        }
        else 
        {
            Debug.Log("End");
        }
    }

    private void DisplayTitle(int i)
    {
        // pageTitle.GetComponent<TMP_Text>().text = allStorysInfo.pageInfo[i].title;
        // pageBody.GetComponent<TMP_Text>().text = allStorysInfo.pageInfo[i].body;
        if (i == 0 )
        {
            pageTitle.GetComponent<TMP_Text>().text = "AIGC";
            pageBody.GetComponent<TMP_Text>().text = "Generation AI";
        } else 
        {
            pageTitle.GetComponent<TMP_Text>().text = storyScript.pages[i-1].index.ToString() + storyScript.pages[i].title;
            pageBody.GetComponent<TMP_Text>().text = storyScript.pages[i-1].body;
        }
        
    }
}

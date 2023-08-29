using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class storyPageSwitch : MonoBehaviour
{
    public List<GameObject> imageArray= new List<GameObject>();
    public int activePageIndex = 0;
    public AllPagesInfo allStorysInfo;
    public GameObject AIScriptOject;
    public GameObject pageTitle, pageBody;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        foreach(GameObject image in imageArray)
        {
            image.SetActive(false);
        }
        
        imageArray[activePageIndex].SetActive(true);

        allStorysInfo = AIScriptOject.GetComponent<ReadStory>().allStorysInfo;
        // pageTitle.GetComponent<TMP_Text>().text = "Demo";
        // Debug.Log(allStorysInfo.pageInfo.Count);
        DisplayTitle(activePageIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        pageTitle.GetComponent<TMP_Text>().text = allStorysInfo.pageInfo[i].title;
        pageBody.GetComponent<TMP_Text>().text = allStorysInfo.pageInfo[i].body;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyPageSwitch : MonoBehaviour
{
    public List<GameObject> imageArray= new List<GameObject>();
    public int activePageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject image in imageArray)
        {
            image.SetActive(false);
        }
        
        imageArray[activePageIndex].SetActive(true);
        
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
        }
        else 
        {
            Debug.Log("Home");
        }
    }

    public void pageNext()
    {
        if (activePageIndex < 2)
        {
            imageArray[activePageIndex].SetActive(false);
            activePageIndex = activePageIndex + 1;
            imageArray[activePageIndex].SetActive(true);
        }
        else 
        {
            Debug.Log("End");
        }
    }
}

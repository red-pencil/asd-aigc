using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPageSwitch : MonoBehaviour
{
    public GameObject[] pageArray;
    public int activePageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PagePrev()
    {
        if (activePageIndex > 0)
        {
            pageArray[activePageIndex].SetActive(false);
            activePageIndex = activePageIndex - 1;
            pageArray[activePageIndex].SetActive(true);
        }
        else 
        {
            Debug.Log("Home");
        }
    }

    public void PageNext()
    {
        if (activePageIndex < pageArray.Length - 1)
        {
            pageArray[activePageIndex].SetActive(false);
            activePageIndex = activePageIndex + 1;
            pageArray[activePageIndex].SetActive(true);
        }
        else 
        {
            Debug.Log("End");
        }
    }
}

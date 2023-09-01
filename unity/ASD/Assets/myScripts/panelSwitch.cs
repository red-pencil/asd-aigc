using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelSwitch : MonoBehaviour
{

    public GameObject currentPage, nextPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void togglePanel()
    {
        currentPage.SetActive(!currentPage.activeSelf);
        nextPage.SetActive(!nextPage.activeSelf);
    }

    public void swtichPanel()
    {
        currentPage.SetActive(false);
        nextPage.SetActive(true);
    }
}

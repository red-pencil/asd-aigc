using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageSwitch : MonoBehaviour
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

    public void swtichTwoPage()
    {
        currentPage.SetActive(!currentPage.activeSelf);
        nextPage.SetActive(!nextPage.activeSelf);
    }
}

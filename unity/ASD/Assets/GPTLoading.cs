using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTLoading : MonoBehaviour
{
    public GameObject AIObject;
    public GameObject LoadingPage;
    private bool initial = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!initial)
        {
            if (AIObject.GetComponent<ChatGPTRecordIO>().message.role == "Reply")
            {
                LoadingPage.SetActive(false);
                initial = true;
            }
            else
            {
                LoadingPage.SetActive(true);
            }
        }
        
        
    }

    public void toggleIntial()
    {
        initial = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwtich : MonoBehaviour
{
    [Header("button1_2, button1_3, button3_2, button3_3")]
    public GameObject[] level1;
     
    [Header("button1_3, button3_3, button5_3")]
    public GameObject[] level2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLevel(int levelOrder)
    {
        switch(levelOrder) 
        {
        case 1:
            foreach (GameObject button in level1) button.SetActive(false);
            break;
        case 2:
            foreach (GameObject button in level2) button.SetActive(false);
            break;
        default:
            foreach (GameObject button in level1) button.SetActive(true);
            foreach (GameObject button in level2) button.SetActive(true);
            break;
        }
    }
}

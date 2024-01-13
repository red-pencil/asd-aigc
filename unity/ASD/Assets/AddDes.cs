using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddDes : MonoBehaviour
{
    public int targetOrder;
    public string targetText;
    public GameObject sourceStory;
    // Start is called before the first frame update
    void Start()
    {
        targetText = sourceStory.GetComponent<StoryIO>().slides.pages[targetOrder].title;
        
        this.gameObject.GetComponentInChildren<TMP_Text>().text = targetText;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

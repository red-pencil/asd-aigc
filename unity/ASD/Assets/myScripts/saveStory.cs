using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class saveStory : MonoBehaviour
{

    public GameObject emotionDropdown, levelDropdown;
    public TMP_Dropdown emotionChoice, levelChoice;

    // Start is called before the first frame update
    void Start()
    {
        emotionChoice = emotionDropdown.GetComponent<TMP_Dropdown>();
        levelChoice = levelDropdown.GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveStory()
    {
        Debug.Log("Emotion: " + emotionChoice.options[emotionChoice.value].text + ", Level: " + levelChoice.options[levelChoice.value].text);       //Debug.Log(emotionDropdown.GetComponent<Dropdown>().value);
                    // levelDropdown.GetComponent<Dropdown>().value.ToString()  );
    

    }
}

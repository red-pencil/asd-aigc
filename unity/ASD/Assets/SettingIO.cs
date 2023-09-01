using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingIO : MonoBehaviour
{
    public GameObject emotionDropdown, levelDropdown, keywordInputField;
    public string[] levelDetail = {
        "Weak", "Average", "Advanced"
    };

    public StorySetting storySetting = new StorySetting();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadFromInput()
    {
        TMP_Dropdown emotionChoice = emotionDropdown.GetComponent<TMP_Dropdown>();
        TMP_Dropdown levelChoice = emotionDropdown.GetComponent<TMP_Dropdown>();
        
        storySetting.emotion = emotionChoice.options[emotionChoice.value].text;
        storySetting.level = levelDetail[levelChoice.value];
        storySetting.keyword = keywordInputField.GetComponent<TMP_InputField>().text;

        Debug.Log("<<< Read From Input! >>>");
    }

    public void SaveSettingJson()
    {
        string jsonContent = JsonUtility.ToJson(storySetting);
        System.IO.File.WriteAllText("./Assets/MyData/Setting.json", jsonContent);

        Debug.Log("<<< Setting Saved! >>>");
    }


}


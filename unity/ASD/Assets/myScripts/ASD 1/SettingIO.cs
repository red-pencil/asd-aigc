using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingIO : MonoBehaviour
{
    [Header("Input:")]
    [SerializeField] private GameObject emotionDropdown, levelDropdown, keywordInputField;
    public string[] levelDetail = {
        "Weak", "Average", "Advanced"
    };

    [Header("Profile Json:")]
    public StorysSetting storySettingArray;

    [SerializeField] private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        OpenSettingJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadFromInput()
    {
        TMP_Dropdown emotionChoice = emotionDropdown.GetComponent<TMP_Dropdown>();
        TMP_Dropdown levelChoice = levelDropdown.GetComponent<TMP_Dropdown>();
        
        StorySetting storySettingItem = new StorySetting();

        storySettingItem.emotionIndex = emotionChoice.value;
        storySettingItem.emotion = emotionChoice.options[emotionChoice.value].text;
        storySettingItem.levelIndex = levelChoice.value;
        Debug.Log(levelChoice.value);
        storySettingItem.level = levelDetail[levelChoice.value];
        storySettingItem.keyword = keywordInputField.GetComponent<TMP_InputField>().text;

        storySettingArray.storySetting.Insert(0, storySettingItem);

        Debug.Log("<<< Read From Input! >>>");
    }
    
    public void WriteToInput()
    {
        if (i >= storySettingArray.storySetting.Count-1) i = 0;

        TMP_Dropdown emotionChoice = emotionDropdown.GetComponent<TMP_Dropdown>();
        TMP_Dropdown levelChoice = levelDropdown.GetComponent<TMP_Dropdown>();

        emotionChoice.value = storySettingArray.storySetting[i].emotionIndex;
        levelChoice.value = storySettingArray.storySetting[i].levelIndex;
        keywordInputField.GetComponent<TMP_InputField>().text = storySettingArray.storySetting[i].keyword;

        Debug.Log("<<< Write to Field! >>>");

        i++;
    }

    public void SaveSettingJson()
    {
        string jsonContent = JsonUtility.ToJson(storySettingArray);
        System.IO.File.WriteAllText("./Assets/MyData/Setting.json", jsonContent);

        Debug.Log("<<< Setting Saved! >>>");
    }

    public void OpenSettingJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/Setting.json").ToString();
        storySettingArray = JsonUtility.FromJson<StorysSetting>(jsonContent);

        Debug.Log("<<< Setting Read! >>>");
    }


}


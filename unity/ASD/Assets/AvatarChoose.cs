using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class AvatarChoose : MonoBehaviour
{
    public RawImage avatarRawImage;
    // public Texture2D avatarTex; 
    public string[] fileNames;
    public List<string> validFileNames = new List<string>();
    public List<string> shortFileNames = new List<string>();
    public int targetFileOrder = 0;

    public string[] folderNames;
    public List<string> shortFolderNames = new List<string>();

    public GameObject storyDropDown;
    private TMP_Dropdown storyList;
    public int targeFolderOrder = 0;
    public GameObject levelDropDown;

    public MetaChoice currentChoice;


    // Start is called before the first frame update
    void Start()
    {
        GetFileNames();

        SwtichFiles(0);

        GetFolderNames();
        storyList = storyDropDown.GetComponent<TMP_Dropdown>();
        storyList.AddOptions(shortFolderNames);
    }

    public void SwtichFiles(int step)
    {
        targetFileOrder = ( (targetFileOrder + step < validFileNames.Count) && (targetFileOrder + step >= 0) )? targetFileOrder + step : targetFileOrder;

        Texture2D avatarTex = new Texture2D(2, 2);
        byte[] imgBytes = System.IO.File.ReadAllBytes(validFileNames[targetFileOrder]);
        // Debug.Log(validFileNames[1]);
        avatarTex.LoadImage(imgBytes);
        avatarRawImage.texture = avatarTex;

    }

    public void GetFileNames()
    {
        fileNames = Directory.GetFiles("./Assets/MyAvatar/");
        // Debug.Log(fileNames.Length);
        int toRemove = "./Assets/MyAvatar/".Length;

        foreach (string fileName in fileNames)
        {
            if ( (fileName.Contains("png")) && (!fileName.Contains("meta")) )
                {
                    validFileNames.Add(fileName);
                    string cutOne = fileName.Remove(0, toRemove);
                    shortFileNames.Add(cutOne.Remove(cutOne.Length-4, 4));
                }
        }

        // Debug.Log(validFileNames.Count);
        // foreach (string fileName in validFileNames)
        // {
        //     Debug.Log(fileName);
        // }
    }

    public void GetFolderNames()
    {
        folderNames = Directory.GetDirectories("./Assets/MyTemplates/");

        int toRemove = "./Assets/MyTemplates/".Length;
        foreach (string folderName in folderNames)
        {
            shortFolderNames.Add(folderName.Remove(0, toRemove));
        }

        // foreach (string folderName in shortFolderNames) Debug.Log(folderName);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveChoiceJson()
    {
        currentChoice.storyChoice = shortFolderNames[storyList.value];
        currentChoice.avatarChoice = shortFileNames[targetFileOrder];
        currentChoice.levelChoice = levelDropDown.GetComponent<TMP_Dropdown>().value + 1;
        SaveJson();
    }

    public void OpenJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/currentChoice.json").ToString();
        currentChoice = JsonUtility.FromJson<MetaChoice>(jsonContent);

        Debug.Log("<<< Choice Read! >>>");
    }

    public void SaveJson()
    {
        string jsonContent = JsonUtility.ToJson(currentChoice);
        System.IO.File.WriteAllText("./Assets/MyData/currentChoice.json", jsonContent);

        Debug.Log("<<< Choice Saved! >>>");
    }
}


[System.Serializable]
public class MetaChoice {
    public string storyChoice;
    public string avatarChoice;
    public int levelChoice;

}
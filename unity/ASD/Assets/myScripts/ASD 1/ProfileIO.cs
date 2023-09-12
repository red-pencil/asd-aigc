using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileIO : MonoBehaviour
{

    [Header("Profile Json:")]
    [SerializeField] private ChildrenProfile childProfileArray;
    // = new ChildrenProfile(); 

    [Header("Input:")]
    [SerializeField] private GameObject nameInputField;
    [SerializeField] private GameObject ageInputField, genderInputField, likeInputField, dislikeInputField, notesInputField;
    
    [SerializeField] private bool autoSave = false;
    [SerializeField] private int i = 0;
    void Start()
    {
        OpenProfileJson();
    }

    void Update()
    {
        
    }

    private void OnGUI()
    {
        autoSave = GUI.Toggle(new Rect(100,100,100,100), autoSave, "Auto Save");
        GUI.Button (new Rect (25, 25, 100, 30), "Button");
    }

    public void ToggleAutoSave()
    {
        autoSave = !autoSave;
    }

    public void AutoSaveJson()
    {
        if (autoSave) 
        {
            ReadFromInput();
            SaveProfileJson();
        }
    }

    public void ReadFromInput()
    {
        ChildProfile childProfileItem = new ChildProfile();
        childProfileItem.index = childProfileArray.childProfile.Count;

        childProfileItem.name = nameInputField.GetComponent<TMP_InputField>().text;
        childProfileItem.age = int.Parse(ageInputField.GetComponent<TMP_InputField>().text);
        childProfileItem.gender = genderInputField.GetComponent<TMP_InputField>().text;
        childProfileItem.like = likeInputField.GetComponent<TMP_InputField>().text;
        childProfileItem.dislike = dislikeInputField.GetComponent<TMP_InputField>().text;
        childProfileItem.notes = notesInputField.GetComponent<TMP_InputField>().text;

        childProfileArray.childProfile.Insert(0, childProfileItem);

        Debug.Log("<<< Read From Input! >>>");
    }

    public void WriteToInput()
    {
        if (i >= childProfileArray.childProfile.Count-1) i = 0;

        nameInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].name;
        ageInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].age.ToString();
        genderInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].gender;
        likeInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].gender;
        dislikeInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].dislike;
        notesInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].notes;

        i++;

        Debug.Log("<<< Write to Field! >>>");
    }

    public void SaveProfileJson()
    {
        string jsonContent = JsonUtility.ToJson(childProfileArray);
        System.IO.File.WriteAllText("./Assets/MyData/Profile.json", jsonContent);

        Debug.Log("<<< Profile Saved! >>>");
    }

    public void OpenProfileJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/Profile.json").ToString();
        childProfileArray = JsonUtility.FromJson<ChildrenProfile>(jsonContent);

        Debug.Log("<<< Profile Read! >>>");
    }
}


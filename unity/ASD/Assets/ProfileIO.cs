using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileIO : MonoBehaviour
{
    public GameObject nameInputField, ageInputField, genderInputField, likeInputField, dislikeInputField, notesInputField;
    
    public ChildrenProfile childProfileArray;
    // = new ChildrenProfile(); 


    // Start is called before the first frame update
    void Start()
    {
        OpenProfileJson();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        childProfileArray.childProfile.Add(childProfileItem);

        Debug.Log("<<< Read From Input! >>>");
    }

    public void WriteToInput()
    {
        int i = 0;

        nameInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].name;
        ageInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].age.ToString();
        genderInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].gender;
        likeInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].gender;
        dislikeInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].dislike;
        notesInputField.GetComponent<TMP_InputField>().text = childProfileArray.childProfile[i].notes;

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


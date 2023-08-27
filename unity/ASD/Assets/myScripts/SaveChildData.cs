using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveChildData : MonoBehaviour
{
    public GameObject name_input_field, age_input_field, gender_input_field, like_input_field, dislike_input_field, notes_input_field;

    [SerializeField] private ChildrenInfo childInfoArray = new ChildrenInfo();
    
    public void SaveIntoJson() {
        
        ChildInfo childInfoItem = new ChildInfo();
        BasicInfo basicInfoItem = new BasicInfo();

        childInfoItem.id = childInfoArray.childInfo.Count;
        //childInfoItem.id = 99;
        
        basicInfoItem.name = name_input_field.GetComponent<TMP_InputField>().text;
        basicInfoItem.age = int.Parse(age_input_field.GetComponent<TMP_InputField>().text);
        basicInfoItem.gender = gender_input_field.GetComponent<TMP_InputField>().text;
        basicInfoItem.like = like_input_field.GetComponent<TMP_InputField>().text;
        basicInfoItem.dislike = dislike_input_field.GetComponent<TMP_InputField>().text;
        basicInfoItem.notes = notes_input_field.GetComponent<TMP_InputField>().text;
    
        childInfoItem.basicInfo.Add(basicInfoItem);

        Debug.Log(childInfoItem.id);
        childInfoArray.childInfo.Add(childInfoItem);
        string children = JsonUtility.ToJson(childInfoArray);
        
        // System.IO.File.WriteAllText(Application.persistentDataPath + "/ChildrenData.json", children);
        System.IO.File.WriteAllText("MyData/ChildrenData.json", children);
   }

}

[System.Serializable]
public class ChildrenInfo {
    public List<ChildInfo> childInfo = new List<ChildInfo>();
}

[System.Serializable]
public class ChildInfo {
    public int id;
    public List<BasicInfo> basicInfo = new List<BasicInfo>();
}

[System.Serializable]
public class BasicInfo {
    public string name;
    public int age;
    public string gender;
    public string like;
    public string dislike;
    public string notes;
}
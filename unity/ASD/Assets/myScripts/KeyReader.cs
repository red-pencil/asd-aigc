using UnityEngine;
using System;
using System.IO;
 
public class KeyReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public string apiKeyContent;
    public ApiKey keyFromJson;
 
    void Awake()
    {
        var userFolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //Debug.Log(jsonFile.text);
        //Debug.Log(userFolderPath + "/.openai/auth.json");
        apiKeyContent = File.ReadAllText(userFolderPath + "/.openai/auth.json").ToString();
        keyFromJson = JsonUtility.FromJson<ApiKey>(apiKeyContent);
 
        Debug.Log(keyFromJson.api_key);
        
    }
}
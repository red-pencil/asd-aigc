using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using OpenAI;


public class ChatGPTRecordIO : MonoBehaviour
{
    public GameObject GPTObject;
    public ChatGPTRecords messages;
    public ChatGPTRecord message;
    // private string roleToggle = ""; no long user to switch

    void Awake()
    {
        OpenJson();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenJson()
    {
        string jsonContent = System.IO.File.ReadAllText("./Assets/MyData/GPTRecords.json").ToString();
        messages = JsonUtility.FromJson<ChatGPTRecords>(jsonContent);

        Debug.Log("<<< GPT Record Read! >>>");
    }

    public void SaveJson()
    {
        string jsonContent = JsonUtility.ToJson(messages);
        System.IO.File.WriteAllText("./Assets/MyData/GPTRecords.json", jsonContent);

        Debug.Log("<<< GPT Record Saved! >>>");
    }

    public void ReadFromAI(string type)
    {
        message = new ChatGPTRecord();

        message.time = DateTime.Now.ToString();
        if (type == "Reply")
        {
            message.role = "Reply";
            message.content = GPTObject.GetComponent<MyChatGPT>().messageReply;

        } else if (type == "Sent")
        {
            message.role = "Sent";
            message.content = GPTObject.GetComponent<MyChatGPT>().messageSent;
        } else
        {
            Debug.Log("Wrong Type");
        }

        messages.records.Insert(0, message);

        SaveJson();
    }

}

[System.Serializable]
public class ChatGPTRecords {
    public List<ChatGPTRecord> records = new List<ChatGPTRecord>();
}

[System.Serializable]
public class ChatGPTRecord {
    public string time;
    public string role;
    public string content;
}

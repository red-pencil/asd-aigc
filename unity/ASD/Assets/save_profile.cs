using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class save_profile : MonoBehaviour
{

    [Header("Teh value we got from the input field")]
    [SerializeField] private string inputText;

    [Header("Showing the reaction to the player")]
    [SerializeField] private GameObject rectionGroup;
    [SerializeField] private TMP_Text reactionTextBox;

    public InputField input_child_name;
    public GameObject input_field;
    public string child_name;
    public Button save_button;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = save_button.GetComponent<save_button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        input_child_name.text = "Hello World!";
        child_name = input_field.GetComponent<TMP_InputField>().text;
    }

    void TaskOnClick(){
        Debug.Log ("You have clicked the button!");
    }
}

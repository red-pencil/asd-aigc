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

    //public InputField input_child_name;
    public GameObject name_input_field, age_input_field, gender_input_field;
    public string child_name, child_age, child_gender;
    //public Button save_button;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //input_child_name.text = "Hello World!";
        

        //if (Input.GetMouseButtonDown(0))
        //    Debug.Log("Pressed left-click.");
    }


    public void PrintLog(){
        child_name = name_input_field.GetComponent<TMP_InputField>().text;
        child_age = age_input_field.GetComponent<TMP_InputField>().text;
        child_gender = gender_input_field.GetComponent<TMP_InputField>().text;
        Debug.Log("You have clicked the button!");
    }

}

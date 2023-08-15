using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class save_profile : MonoBehaviour
{

    public InputField input_child_name;
    public string child_name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input_child_name.text = "Enter Text Here...";
    }
}

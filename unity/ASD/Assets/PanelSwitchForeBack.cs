using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitchForeBack : MonoBehaviour
{
    public GameObject panelFore, panelBack;
    // Start is called before the first frame update
    void Start()
    {
        panelBack.GetComponent<Canvas>().sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePanel()
    {
        if ( panelBack.GetComponent<Canvas>().sortingOrder == -1)
        {
            panelBack.GetComponent<Canvas>().sortingOrder = 3;
        }
        else if ( panelBack.GetComponent<Canvas>().sortingOrder == 3)
        {
            panelBack.GetComponent<Canvas>().sortingOrder = -1;
        }
    }
}

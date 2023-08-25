using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenToWorld : MonoBehaviour
{
    public Camera cam;
    public GameObject indicator_object;
    public Vector3 point = new Vector3();

    void Start()
    {
        //cam = Camera.main;
    }

    void Update()
    {
        
        indicator_object.transform.position = point;
        indicator_object.transform.Translate(0, -11, 0);
    }

    void OnGUI()
    {
        //Vector3 point = new Vector3();
        Event   currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();

        
    }
}

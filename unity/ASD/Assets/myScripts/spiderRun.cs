using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderRun : MonoBehaviour
{

    [Header("Private:")]
    [SerializeField] private Vector3 point = new Vector3();
    [SerializeField] private Vector3 aim_position = new Vector3();

    [Header("Public:")]
    public Camera cam;
    public GameObject indicator_object;
    public GameObject moving_object;
    public float speed = 1.0f;

    void Update()
    {
        
        //indicator_object.transform.position = point;
        //indicator_object.transform.Translate(0, -11, 0);
        aim_position = new Vector3(point.x, 0.0f, point.z); //0.6 the y-position of spiders
        indicator_object.transform.position = aim_position;
        moving_object.transform.LookAt(aim_position);
        
        if (Vector3.Distance(moving_object.transform.position, aim_position) > 1f)
        {
            
            var step =  speed * Time.deltaTime;
            moving_object.transform.position = Vector3.MoveTowards(moving_object.transform.position, aim_position, step);
        }
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

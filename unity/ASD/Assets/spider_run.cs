using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_run : MonoBehaviour
{

    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 point = new Vector3();
    
    public GameObject moving_object;
    public GameObject target_object;
    public GameObject indicator_object;


    public Camera cam;

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed *-1.0f;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += cam.nearClipPlane;

        Vector3 targetPos = new Vector3(point.x, 11.0f, point.z);
        indicator_object.transform.position = targetPos;

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Click.");
            mousePos = Input.mousePosition;
            


            //Vector3 mousPos3D = new Vector3 ()

            //Debug.Log("Mouse Location: （ X: " + mousePos.x.ToString() + " , Y: " + mousePos.y.ToString() + " )");
            //Vector3 mousePos2D = new Vector3(mousePos.x, moving_object.transform.translation.y, mousePos.z);

            
            moving_object.transform.LookAt(targetPos);
            

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

        GUILayout.BeginArea(new Rect(500, 500, 500, 500));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();

        
    }
            
        
        
    }

}

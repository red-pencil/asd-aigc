using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class followMouse : MonoBehaviour
{
    [SerializeField] private Vector3 mousePosition;


    public Camera primeCamera;
    void Update()
    {
        mousePosition = primeCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = primeCamera.transform.position.z + primeCamera.nearClipPlane;
        transform.position = mousePosition;
    }


}

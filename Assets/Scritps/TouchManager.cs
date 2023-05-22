using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public LayerMask sphereLayer;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        //Input.GetTouch
        if (Input.GetMouseButton(0))
        {
            Vector3 pointerPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(pointerPosition, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 15, sphereLayer))
            {

                Sphere sphere = hit.collider.GetComponent<Sphere>();
                if(sphere != null)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");  
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
}

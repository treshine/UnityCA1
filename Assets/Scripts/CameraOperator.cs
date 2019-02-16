using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{

    
    private float zoomSpeed = 0.3f;    // Rate of change of camera Field Of View
    private float lookSpeedReducer = 10.0f;     // Used to slow down yaw and pitch values (higher = slower) 
 

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float horizontalInv = -1.0f;
    public float verticalInv = -1.0f;
    
    public Selector selector;
     
    private void Update()
    {
        
   
        
        if (selector.selected == null && Input.touches.Length == 1)
        {
            // Get yaw value from touch x change
            yaw += (Input.touches[0].deltaPosition.x  / lookSpeedReducer) * horizontalInv; 
            
            // Get pitch value from touch y change
            pitch -= (Input.touches[0].deltaPosition.y / lookSpeedReducer) * verticalInv;
            
            // Clamp the pitch to limit view
            pitch = Mathf.Clamp(pitch, -30, 30);
            
            // apply yaw and pitch values to camera
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        if (selector.selected == null && Input.touches.Length == 2 && Input.GetTouch(0).phase == TouchPhase.Moved 
            && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            // Get two touches.
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Calculate the previous frame position of each touch.
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            // Calculate the vector magnitude between the two touches in each frame.
            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            // Calculate the difference in magnitudes between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

           
            // Change camera Field Of View based on magnitude change between the touches
            GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * zoomSpeed;

            // Apply a clamp between 5 and 50 to the camera Field Of View (limiting zoom)
            GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 4.0f, 60.0f); 

        }
    }
}

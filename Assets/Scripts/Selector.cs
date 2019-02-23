/*
 * This is the main Touch Control Class
 *
 *
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Selector : MonoBehaviour
{
    const float minDragDistance = 7.5f;  // Threshold value for drag distance  (7.5 works well on tablet)
    const float pinchSpeed = 0.02f;
    const float minPinchDistance = 5;  // Threshold value for pinch distance        
    const float rotateSpeed = Mathf.PI / 2; // lower divisor = higher rotation speed
    const float minRotateAngle = 2;   // Threshold value for rotate angle

    public static float pinchDistance;  //  Distance between two touches
    public static float pinchDistanceDelta; // Difference in distance between two touches  	
    public static float rotateAngle;  //  The angle between two touches
    public static float rotateAngleDelta; // The difference of the angle between two touches
    	 
    
    // Touch Boolean Settings 
    public static Boolean isTap = false;
    public static Boolean isDrag = false;
    public static Boolean isScale = false;
    public static Boolean isRotate = false;
    public static Boolean cameraActive = true;  // Camera is selected at startup
    

    private Vector2 startTouchPos; // when touch phase begins
    public static Vector2 currentTouchPos; // when touch moved
    private Vector2 endTouchPos; // when touch phase ends
    
    public static float dragDistance;   //  how far the drag moved
    
    
    public GameObject selected = null;
    
    public static Camera cam;

    public void Awake()
    {
         cam = GetComponent<Camera>();
       
    }

    public void Update()
    {
         // Use a switch statement to check for each category of touch - Tap, Drag, or Pinch
        if (Input.touchCount == 1) {
            switch (Input.touches[0].phase){
                
                case TouchPhase.Began:
		            // store startTouchPos to test if touch is a tap or touch
		            startTouchPos = Input.touches[0].position;                
		            isTap = true;

		            break; // exit Began
	            case TouchPhase.Moved:
		            //print("MOVED");  
		            currentTouchPos = Input.touches[0].position;
		            if (CheckDrag())
			            isDrag = true;
   
                    
                                       
		            break;
	            case TouchPhase.Stationary:
		            //print("STATIONARY");
		            break;
                case TouchPhase.Ended:
	                if (isTap)
		                DoTapAction();
	                isDrag = false;
                       
                 break; // exit Ended
            } // switch
            
        } // if (Input.touchCount > 0)
        

        // Check for two fingers
        else if (Input.touchCount == 2)
        {

            CheckPinchRotate();
        }

        if (selected != null){
	        foreach (Selectable s in selected.GetComponents<Selectable>())     
		        s.OnUpdate();  
	        // print("updating **********");
        }

    }
    

    private void DoTapAction(){
        
	    Ray laser = cam.ScreenPointToRay(Input.GetTouch(0).position);
	    Debug.DrawRay(laser.origin, 100 * laser.direction, Color.red, 5, true);
        
	    // What does the ray 'laser' hit
	    RaycastHit hit;

	    // If laser hits an object
	    if (Physics.Raycast(laser, out hit))
	    {
		    cameraActive = false;
	
		    Debug.Log("Hit Something");
            
		    // Clear any previous selected components
            ClearSelectedObjects();
		    
		    // Get gameobject components that are Selectable and store in Selectable[]
		    Selectable[] selectables = hit.collider.GetComponents<Selectable>();
		    // set selected to the gameObject hit
		    selected = hit.collider.gameObject;
            
		    // Each selected component will run OnSelect() method
		    foreach (Selectable s in selectables)
			    s.OnSelect();

	    }
	    else { // nothing was hit so deselect all selected components
		    ClearSelectedObjects();
			cameraActive = true;
			print("CAMERA Active");
		    
	    }
         
    } // DoTapAction
    
     private void ClearSelectedObjects()
        {
    	    // Clear any previous selected components
    	    if (selected != null){
    		    foreach (Selectable s in selected.GetComponents<Selectable>())  
    			    s.OnDeselect();            
            
    		    selected = null;
    	    }
        }
    
   
    private Boolean CheckDrag (){
         /*  ************************    Tap or Drag Test  *****************************************
                                
             User might want to tap but a slight movement will register as a drag
             Setting a minimum drag distance allows for sensitivity adjustment                  
         */  
        dragDistance = Vector2.Distance(startTouchPos, currentTouchPos);
        
        // any slight movement less than minDragDistance is still a Tap
        // check fingerIDs to make sure the same finger is used
        if (dragDistance >= minDragDistance){
            return true; // it is a drag
        }

        return false;
    } // CheckForDrag



    private void CheckPinchRotate()
    {
        // Set distance and angle values to zero
        pinchDistance = 0;
        pinchDistanceDelta = 0;
        rotateAngle = 0;
        rotateAngleDelta = 0;
        
        Touch touch0 = Input.touches[0];
        Touch touch1 = Input.touches[1];
        
        if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved) {
        				
        				
	        //  ************************    Pinch or Scale     *****************************************
        				
	        // Calculate the distance between the touches
	        pinchDistance = Vector2.Distance(touch0.position, touch1.position);
        				
	        // Calculate the previous distance between the touches
	        float prevPinchDistance = Vector2.Distance(touch0.position - touch0.deltaPosition,
		        touch1.position - touch1.deltaPosition);
        				
	        // Calculate difference between previous and current distance
	        pinchDistanceDelta = pinchDistance - prevPinchDistance;
         
	        // It is a Pinch once value is greater than the Minimum Pinch distance 
	        if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
	        {
		        isRotate = false;
				// if gameobject is selected, then Scale
		        if (selected != null)
		        {
			        isScale = true;
		        }
		        
		        
		        pinchDistanceDelta *= pinchSpeed;
	        } else {
		        // Not a Pinch so reset pinch values
		        isScale = false;
		        pinchDistance = pinchDistanceDelta = 0;
	        }
        				
	        //  ************************    ROTATE     *****************************************
        				
	        // Calculate the angle difference between the two touches
	        rotateAngle = FindAngleBetweenVectors(touch0.position, touch1.position);
        				
	        // Calculate the previous angle difference between the two touches
	        float prevRotateAngle = FindAngleBetweenVectors(touch0.position - touch0.deltaPosition, touch1.position - touch1.deltaPosition);
        				
	        // Calculate difference between previous and current angles
	        rotateAngleDelta = Mathf.DeltaAngle(prevRotateAngle, rotateAngle);
         
	        // It is a Rotate once value is greater than the Minimum Rotate Angle 
	        if (Mathf.Abs(rotateAngleDelta) > minRotateAngle)
	        {
		        isRotate = true;
		        isScale = false;
		        rotateAngleDelta *= rotateSpeed;
	        } else {
		        isRotate = false;
		        // Not a Rotate so reset values
		        rotateAngle = rotateAngleDelta = 0;       					
	        }
        } 
        
         
    } // PinchRotate()

    private void ResetActions()
    {
	    isScale = isRotate  = false;

    }

   
    
    // Calculates the angle between two vectors using cross product
    private float FindAngleBetweenVectors (Vector2 position1, Vector2 position2) {
	    Vector2 from = position2 - position1;
	    Vector2 to = new Vector2(1, 0);
          
	    float result = Vector2.Angle( from, to );
         		
	    // Returns a vector perpendicular to the plane defined by 'from' and 'to'	
	    Vector3 crossProduct = Vector3.Cross( from, to );
          
	    // Check they are not collinear
	    if (crossProduct.z > 0) {
		    result = 360f - result;
	    }
          
	    return result;
    }
    
}
    

/*
 * This is the main Touch Control Class
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Selector : MonoBehaviour
{
    const float minDragDistance = 7.5f;  // Threshold value for drag distance  (7.5 works well on tablet)
    
    // Touch Boolean Settings  
    public static Boolean isTap = false;
    public static Boolean isDrag = false;

    private Vector2 startTouchPos; // when touch phase begins
    private Vector2 currentTouchPos; // when touch moved
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
        if (Input.touchCount > 0) {
            switch (Input.touches[0].phase){
                
                case TouchPhase.Began:
                   // print("START");
                    // store startTouchPos to test if touch is a tap or touch
                    startTouchPos = Input.touches[0].position;                
                    isTap = true;
                    DoTapAction();
                    break; // exit Began
                case TouchPhase.Moved:
                    //print("MOVED");  
                    currentTouchPos = Input.touches[0].position;
                    if (CheckForDrag(currentTouchPos))
                        isDrag = true;
   
                    
                                       
                    break;
                case TouchPhase.Stationary:
                    //print("STATIONARY");
                    break;
                case TouchPhase.Ended:
                    print("ENDED");
                    
                    
                    endTouchPos = Input.touches[0].position;
                    dragDistance = Vector2.Distance(startTouchPos, endTouchPos);

                    if (dragDistance >= minDragDistance){
                        // any slight movement less than minDragDistance is still a Tap
                        isDrag = true;
                        print("IS DRAG:" + isDrag);
                        isTap = false;
                    }
                    else{
                        isTap = true;
                        isDrag = false;
                    }

                    if (isTap)
                        DoTapAction();                                
                 break; // exit Ended
            } // switch
            
        } // if (Input.touchCount > 0)
        
         if (selected != null){
                    foreach (Selectable s in selected.GetComponents<Selectable>())     
                        s.OnUpdate();  
                    print("updating **********");
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
            // Clear any previous selected components
            if (selected != null){
                foreach (Selectable s in selected.GetComponents<Selectable>())  
                    s.OnDeselect();            

                selected = null;
            }

            Debug.Log("Hit Something");
            
            // Get gameobject components that are Selectable and store in Selectable[]
            Selectable[] selectables = hit.collider.GetComponents<Selectable>();
            // set selected to the gameObject hit
            selected = hit.collider.gameObject;
            
            // Each selected component will run OnSelect() method
            foreach (Selectable s in selectables)
                s.OnSelect();

        }
        else { // nothing was hit so deselect all selected components
            if (selected != null){
                foreach (Selectable s in selected.GetComponents<Selectable>())
                    s.OnDeselect();
               
              selected = null;
            }
        }
         
    } // DoTapAction
    
    
    
   
    private Boolean CheckForDrag (Vector2 newPosition){
         /*  ************************    Tap or Drag Test  *****************************************
                                
             User might want to tap but a slight movement will register as a drag
             Setting a minimum drag distance allows for sensitivity adjustment                  
         */  
        dragDistance = Vector2.Distance(startTouchPos, newPosition);
        if (dragDistance >= minDragDistance){
            // any slight movement less than minDragDistance is still a Tap
            return true;
        }

        return false;
    } // CheckForDrag
}

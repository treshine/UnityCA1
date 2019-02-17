using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTranslate : Selectable
{
    private const float panSpeed = 0.4f;
    
    public override void OnSelect()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        
    }

    public override void OnDeselect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public override void OnUpdate()
    {
        // If there is one finger on the screen and it has moved
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Touch touch0 = Input.touches[0];
         
            
            Vector3 touchedPos = TouchCalculator.cam.ScreenToWorldPoint(new Vector3(touch0.position.x, touch0.position.y, 10));
            // lerp and set the position of the current object to that of the touch, but smoothly over time.
            transform.position = touchedPos;



        }
    }
}

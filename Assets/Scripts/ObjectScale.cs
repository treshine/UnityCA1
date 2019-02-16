using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScale : Selectable
{

    private float initialDistance = -1;
    
    public override void OnSelect()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public override void OnDeselect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public override void OnUpdate()
    {
        if (Input.touchCount == 2){   
          
         
            TouchCalculator.Calculate();
            float pinchAmount = 0;      
            float Max = 2.0f;
            float Min = 0.2f;
            
            //  If Pinch detected calculate finalRotation
            if (TouchCalculator.isPinch) { 
                pinchAmount = TouchCalculator.pinchDistanceDelta;
            }
            // Set the scale of the object based on pinch
            transform.localScale += Vector3.one * pinchAmount;
            
            // Clamp the scale values
            transform.localScale = new Vector3(
                         Mathf.Clamp(transform.localScale.x, Min, Max),
                         Mathf.Clamp(transform.localScale.y, Min, Max),
                         Mathf.Clamp(transform.localScale.z, Min, Max)
                    );
                 
             
        }
    }
}

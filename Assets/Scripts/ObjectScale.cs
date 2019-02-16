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
            
            //  If Pinch detected calculate finalRotation
            if (TouchCalculator.isPinch) { 
                pinchAmount = TouchCalculator.pinchDistanceDelta;
            }
        
            transform.localScale += Vector3.one * pinchAmount;
            
        }
    }
}

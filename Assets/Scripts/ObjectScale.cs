using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScale : Selectable
{

    public override void OnSelect()
    {

        
    }

    public override void OnDeselect()
    {

    }

    public override void OnUpdate()
    {
        if (Input.touchCount == 2 && Selector.isScale){   
  
            float scaleMax = 2.0f;
            float scaleMin = 0.5f;
            
            //  If Pinch detected calculate scale
  
            float pinchAmount = Selector.pinchDistanceDelta;
            
            // Set the scale of the object based on pinch
            transform.localScale += Vector3.one * pinchAmount;
            
            // Clamp the scale values
            transform.localScale = new Vector3(
                         Mathf.Clamp(transform.localScale.x, scaleMin, scaleMax),
                         Mathf.Clamp(transform.localScale.y, scaleMin, scaleMax),
                         Mathf.Clamp(transform.localScale.z, scaleMin, scaleMax)
                    );
        }
    }
}

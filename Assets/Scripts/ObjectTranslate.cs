using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectTranslate : Selectable
{
    private const float dragSpeed = 10.0f;
    
    public override void OnSelect()
    {
      
        
    }

    public override void OnDeselect()
    {
      
    }

    public override void OnUpdate()
    {

        //  If Drag detected calculate finalRotation
        if (Input.touchCount == 1) {

            TouchCalculator.Calculate();
            if (TouchCalculator.isDrag)
            {
                // Tried to use transform.position.z for the z position but settled for starting position of 13
                Vector3 touchedPos = TouchCalculator.cam.ScreenToWorldPoint(new Vector3(
                                                                            Input.GetTouch(0).position.x, 
                                                                            Input.GetTouch(0).position.y, 
                                                                            10f));
 
                // Use Vector3.Lerp to interpolate transform
                transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * dragSpeed);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : Selectable
{    
    public override void OnSelect()
    {
       
    }

    public override void OnDeselect()
    {
       
    }

    public override void OnUpdate()
    {
        
        if (Input.touchCount == 2){ 
                 
            Quaternion finalRotation = transform.rotation;
    
           /* TouchCalculator.Calculate();
        
            //  If Rotate detected calculate finalRotation
            if (TouchCalculator.isRotate) {
           
                Vector3 rotationDegrees = Vector3.zero;
                rotationDegrees.z = TouchCalculator.rotateAngleDelta;
                finalRotation *= Quaternion.Euler(rotationDegrees);
                transform.rotation = finalRotation;
            }       
             
        / Rotate game object  
         */    
        }
    }
}


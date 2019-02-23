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
        
        if (Input.touchCount == 2 && Selector.isRotate){ 
            
            Quaternion finalRotation = transform.rotation;
        
            //  If Rotate detected calculate finalRotation
            if (Selector.isRotate) {
           
                Vector3 rotationDegrees = Vector3.zero;
                rotationDegrees.z = Selector.rotateAngleDelta;
                finalRotation *= Quaternion.Euler(rotationDegrees);
                transform.rotation = finalRotation;
            }       

        }
    }
}


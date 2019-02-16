using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : Selectable
{    
    public override void OnSelect()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    public override void OnDeselect()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void OnUpdate()
    {
        
        if (Input.touchCount == 2){ 
                 
            Quaternion finalRotation = transform.rotation;
    
            TouchCalculator.Calculate();
        
            //  If Rotate detected calculate finalRotation
            if (TouchCalculator.isRotate) {
           
                Vector3 rotationDegrees = Vector3.zero;
                rotationDegrees.z = TouchCalculator.rotateAngleDelta;
                finalRotation *= Quaternion.Euler(rotationDegrees);
            }       
             
        // Rotate game object  
        transform.rotation = finalRotation;
        
     
       
        

            
        }
    }
}


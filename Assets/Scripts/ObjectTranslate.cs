using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectTranslate : Selectable
{
    private const float dragSpeed = 10.0f;
    private Color origMaterial; // Material gameobject
      
  
  
  
    public override void OnSelect()
    {
        origMaterial = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.magenta;
          
    }
  
    public override void OnDeselect()
    {
        GetComponent<Renderer>().material.color = origMaterial;
    }


    public override void OnUpdate()
    {
        //  If Drag detected calculate translation
        if (Input.touchCount == 1 && Selector.isDrag) {
            
                
                // Best results with transform.position.z of 10
                Vector3 touchedPos = Selector.cam.ScreenToWorldPoint(new Vector3(
                                                                            Input.GetTouch(0).position.x, 
                                                                            Input.GetTouch(0).position.y, 
                                                                            10f));
                print(touchedPos);
                // Use Vector3.Lerp to interpolate transform
                transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * dragSpeed);
            
            
            
           
        }
    }
}

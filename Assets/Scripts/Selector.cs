using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Selector : MonoBehaviour
{
   
    public GameObject selected = null;
    
    private Camera cam;

    public void Awake()
    {
         cam = GetComponent<Camera>();
       
    }

    public void Update()
    {
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray laser = cam.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.DrawRay(laser.origin, 100 * laser.direction, Color.red, 5, true);
            
            // What does the ray 'laser' hit
            RaycastHit hit;

            // If laser hits an object
            if (Physics.Raycast(laser, out hit))
            {
                // Clear any previous selected components
                if (selected != null)
                {
                    foreach (Selectable s in selected.GetComponents<Selectable>())
                    {
                        s.OnDeselect();
                    }
                    selected = null;
                }
                    

                Debug.Log("Hit Something");
                // Get gameobject components that are Selectable and store in Selectable[]
                Selectable[] selectables = hit.collider.GetComponents<Selectable>();
     
                    selected = hit.collider.gameObject;
                    
                    foreach (Selectable s in selectables)
                    {
                        s.OnSelect();
                    }                       
                
            }
            else
            {
                if (selected != null)
                {
                    foreach (Selectable s in selected.GetComponents<Selectable>())
                    {
                        s.OnDeselect();
                    }
                    selected = null;
                }
            }
        }

        if (selected != null)
        {
            foreach (Selectable s in selected.GetComponents<Selectable>())
            {
                s.OnUpdate();
            }
        }
    }
}

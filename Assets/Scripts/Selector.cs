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
        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended)
        {
            Ray laser = cam.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.DrawRay(laser.origin, 100 * laser.direction, Color.red);
            
            // What does the ray 'laser' hit
            RaycastHit hit;

            if (Physics.Raycast(laser, out hit))
            {

                Debug.Log("Hit Something");
                // hit.collider.GetComponent<Selectable>().YouveBeenSelected();
                Selectable[] selectables = hit.collider.GetComponents<Selectable>();
                
                
                if (selectables.Length > 0)
                {
                    selected = hit.collider.gameObject;
                    foreach (Selectable s in selectables)
                    {
                        s.OnSelect();
                    }
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

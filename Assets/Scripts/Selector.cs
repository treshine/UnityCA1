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
        cam = this.GetComponent<Camera>();
    }

    public void Update()
    {
        if (Input.touches.Length == 1 && Input.touches[0].phase == TouchPhase.Ended)
        {
            Ray touchRay = cam.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(touchRay, out hit))
            {

                Selectable[] selectables = hit.collider.GetComponents<Selectable>();
                
                if (selectables.Length > 0)
                {
                    selected = hit.collider.gameObject;
                    foreach (Selectable s in selectables)
                    {
                        s.onSelect();
                    }
                }
            }
            else
            {
                if (selected != null)
                {
                    foreach (Selectable s in selected.GetComponents<Selectable>())
                    {
                        s.onDeselect();
                    }

                    selected = null;
                }
            }
        }

        if (selected != null)
        {
            foreach (Selectable s in selected.GetComponents<Selectable>())
            {
                s.update();
            }
        }
    }
}

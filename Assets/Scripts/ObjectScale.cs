using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScale : Selectable
{

    private float initialDistance = -1;
    
    public override void onSelect()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    public override void onDeselect()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void update()
    {
        if (Input.touches.Length == 2)
        {
            float currentDistance = Vector2.Distance(
                Input.touches[0].position,
                Input.touches[1].position
            );

            if (initialDistance < 0)
            {
                initialDistance = currentDistance;
            }

            this.transform.localScale = (currentDistance / initialDistance) * Vector3.one;

            if (Input.touches[1].phase == TouchPhase.Ended)
            {
                initialDistance = -1;
            }
        }
    }
}

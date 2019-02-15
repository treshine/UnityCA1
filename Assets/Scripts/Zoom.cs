using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{

    private float initialDistance = -1;

    public Selector selector;
    
    private void Update()
    {
        if (selector.selected == null && Input.touches.Length == 2)
        {
            float currentDistance = Vector2.Distance(
                Input.touches[0].position,
                Input.touches[1].position
            );

            if (initialDistance < 0)
            {
                initialDistance = currentDistance;
            }

            this.GetComponent<Camera>().focalLength = (currentDistance / initialDistance);
                                                      
            if (Input.touches[1].phase == TouchPhase.Ended)
            {
                initialDistance = -1;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTranslate : Selectable
{
    private float initialRotation = -1;
    
    [SerializeField] // <-- makes variable visible in editor
    private Camera camera;

    public override void onSelect()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    public override void onDeselect()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void update()
    {
        if (Input.touches.Length == 2)
        {
            float currentAngle = Mathf.Atan(
                Input.touches[1].position.y - Input.touches[0].position.x /
                Input.touches[1].position.x - Input.touches[0].position.x
            );

            if (initialRotation == -1)
            {
                initialRotation = currentAngle;
            }
            
            this.transform.Rotate(
                camera.transform.forward,
                (currentAngle - initialRotation) * Mathf.Rad2Deg * -100
            );
            
            if (Input.touches[1].phase == TouchPhase.Ended)
            {
                initialRotation = -1;
            }
        }
    }
}

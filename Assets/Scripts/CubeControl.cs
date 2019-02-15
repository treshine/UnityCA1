using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {
            Vector2 screenPositionOfTouch = Input.touches[0].position;
            Ray laser = Camera.main.ScreenPointToRay(screenPositionOfTouch);
            Debug.DrawRay(laser.origin, 100 * laser.direction, Color.red);

            // What does the ray 'laser' hit
            RaycastHit info;
            if (Physics.Raycast(laser, out info))
            {
                Debug.Log("Hit Something");
               // info.collider.GetComponent<SphereControl>().YouveBeenSelected();
                

            }
        }
    }
}

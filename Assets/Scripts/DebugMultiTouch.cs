using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMultiTouch : MonoBehaviour
{
    List<string> touchInfos = new List<string>();
    private Boolean isTap;

    void Update()
    {
        touchInfos.Clear();
     

        for(int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            string tmp = "Touch #" + (i + 1) + " at " + touch.position.ToString();
            touchInfos.Add(tmp);
        }
        
        
        if (Input.touchCount > 0)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    print("start");        
                    isTap = true;
                    break;
                case TouchPhase.Moved:
                    print("Moved");
                    isTap = false;
                    break;
                case TouchPhase.Stationary:
                    print("stat");
                    break;
                case TouchPhase.Ended:
                    print("Ended");
                    if (isTap) print("Do raycast");
                    break;

            }

        }

        
    }

    private void OnGUI()
    {
        foreach(string s in touchInfos)
        {
            GUILayout.Label(s);
        }
    }
}

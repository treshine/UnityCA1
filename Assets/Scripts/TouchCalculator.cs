﻿using System;
using UnityEngine;
using System.Collections;
 
public class TouchCalculator : MonoBehaviour {
	
	
	const float pinchSpeed = 0.02f;
    const float minPinchDistance = 0;  // Threshold value for pinch distance
     
	const float rotateSpeed = Mathf.PI / 2; // lower divisor = higher rotation speed
	const float minRotateAngle = 0;   // Threshold value for rotate angle
 
	const float dragSpeed= 1;
	const float minDragDistance = 0;
	public static Vector3 touchedPos;

	public static Boolean isDrag = false;
	public static Boolean isRotate = false;
	public static Boolean isPinch = false;
	
	public static float pinchDistanceDelta; // Difference in distance between two touches
	public static float pinchDistance;  //  Distance between two touches
	
	public static float rotateAngle;  //  The angle between two touches
	public static float rotateAngleDelta; // The difference of the angle between two touches
	 
	public static Camera cam;
    
        public void Awake()
        {
             cam = GetComponent<Camera>();
           
        }
	
	// Calculates Pinch and Rotate values
	static public void Calculate () {
		// Set distance and angle values to zero
		pinchDistance = pinchDistanceDelta = 0;
		rotateAngle = rotateAngleDelta = 0; 
 
		
		
		
		// Check for one finger
		if (Input.touchCount == 1)
		{
			Touch touch0 = Input.touches[0];

			// If the touch is stationary or has moved, it is a drag
			if (touch0.phase == TouchPhase.Stationary || touch0.phase == TouchPhase.Moved)
			{

				//  ************************    Tap    *****************************************

				// It is a Tap once value 
			}
			
			// If the touch is stationary or has moved, it is a drag
			if (touch0.phase == TouchPhase.Stationary || touch0.phase == TouchPhase.Moved)
			{

				//  ************************    DRAG    *****************************************

				touchedPos = cam.ScreenToWorldPoint(new Vector3(touch0.position.x, touch0.position.y, 10));

				isDrag = true;
			}
			else
				{

					isDrag = false;
				}
			

		}

		// Check for two fingers
		if (Input.touchCount == 2) {
			Touch touch1 = Input.touches[0];
			Touch touch2 = Input.touches[1];
 
			// If one of the touches has moved, check for Pinch or Rotate
			if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved) {
				
				
				//  ************************    PINCH     *****************************************
				
				// Calculate the distance between the touches
				pinchDistance = Vector2.Distance(touch1.position, touch2.position);
				
				// Calculate the previous distance between the touches
				float prevPinchDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
				                                           touch2.position - touch2.deltaPosition);
				
				// Calculate difference between previous and current distance
				pinchDistanceDelta = pinchDistance - prevPinchDistance;
 
				// It is a Pinch once value is greater than the Minimum Pinch distance 
				if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
				{
					isPinch = true;
					pinchDistanceDelta *= pinchSpeed;
				} else {
					// Not a Pinch so reset pinch values
					isPinch = false;
					pinchDistance = pinchDistanceDelta = 0;
				}
				
				//  ************************    ROTATE     *****************************************
				
				// Calculate the angle difference between the two touches
				rotateAngle = angleBetweenVectors(touch1.position, touch2.position);
				
				// Calculate the previous angle difference between the two touches
				float prevRotateAngle = angleBetweenVectors(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
				
				// Calculate difference between previous and current angles
				rotateAngleDelta = Mathf.DeltaAngle(prevRotateAngle, rotateAngle);
 
				// It is a Rotate once value is greater than the Minimum Rotate Angle 
				if (Mathf.Abs(rotateAngleDelta) > minRotateAngle)
				{
					isRotate = true;
					rotateAngleDelta *= rotateSpeed;
				} else {
					isRotate = false;
					// Not a Rotate so reset rotate values
					rotateAngle = rotateAngleDelta = 0;
					
				}
			}
		}
	}
 
	// Calculates the angle between two vectors using cross product
	static private float angleBetweenVectors (Vector2 position1, Vector2 position2) {
		Vector2 from = position2 - position1;
		Vector2 to = new Vector2(1, 0);
 
		float result = Vector2.Angle( from, to );
		
		// Returns a vector perpendicular to the plane defined by 'from' and 'to'	
		Vector3 crossProduct = Vector3.Cross( from, to );
 
		// Check they are not collinear
		if (crossProduct.z > 0) {
			result = 360f - result;
		}
 
		return result;
	}
}
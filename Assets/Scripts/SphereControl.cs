using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SphereControl : MonoBehaviour {

    
    
    float sensitivity_H = 1.0f;
    float sensitivity_V = 1.0f;
  
    float GetAxisH = 0;
    float GetAxisV = 0;
    
    Vector3 baseAcceleration;
    private Vector3 currentAcceleration;
    public float speed = 2f; 
    float smoothness = 0.5f;
    
    
    private Rigidbody rb;
 

    

    
   
    void ResetAxes(){
         baseAcceleration = Input.acceleration;
         currentAcceleration = Vector3.zero;
     }

     
         void Start() 
         {
             ResetAxes();
             rb = GetComponent<Rigidbody>();
         }
     
         void Update()
         {
             currentAcceleration = Vector3.Lerp(currentAcceleration, Input.acceleration-baseAcceleration, Time.deltaTime/smoothness);
             GetAxisV = Mathf.Clamp(currentAcceleration.y * sensitivity_V, -1, 1);
             GetAxisH = Mathf.Clamp(currentAcceleration.x * sensitivity_H, -1, 1);
             
             float moveHorizontal = GetAxisH;
             float moveVertical = GetAxisV;
           
             Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
             rb.AddForce(movement * speed);
         }
         
         
       
         
     }

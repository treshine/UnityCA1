﻿     
// loads current scene again
using UnityEngine;
     using UnityEngine.SceneManagement;
     using System.Collections;
     
     public class ButtonScripts : MonoBehaviour {
     
         public void RestartGame() {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
         }
     
     }
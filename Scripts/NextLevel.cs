using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   
   public void OnTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.CompareTag("FinishLevel"))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
   }
    
}
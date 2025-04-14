using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
     public void OnTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.CompareTag("Finish"))
    {
        SceneManager.LoadScene("CreditsMenu");
    }
   }
}

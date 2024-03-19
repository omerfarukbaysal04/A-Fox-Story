using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    
    [SerializeField] AudioClip clickSound;

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Blocker"))
        {
            AudioSource.PlayClipAtPoint(clickSound,other.transform.position);
            
        }
    }
}
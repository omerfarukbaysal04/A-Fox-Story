using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{

    [SerializeField] AudioClip clickSound;

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("HealUpper"))
        {
            AudioSource.PlayClipAtPoint(clickSound,other.transform.position);
            Destroy(other.gameObject); 
        }
    }
}
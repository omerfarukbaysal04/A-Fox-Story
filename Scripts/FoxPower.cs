using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoxPower : MonoBehaviour
{
  [SerializeField] public AudioClip audioClip;
  [SerializeField] public AudioClip audioClip2;
    public ScoreBar scoreBar;
    public PlayerHealth playerHealth;
    public CharacterController characterController;
    int minPower=0;
    int currentPower=0;

    void Start()
    {
        currentPower=minPower;
        scoreBar.SetScore(minPower);
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentPower!=0)
        {
            
            if(playerHealth.currentHealth>=100)
            {
                playerHealth.currentHealth -= 20;
            }
            playerHealth.currentHealth += 20;
            currentPower -=1;
            scoreBar.SetScore(currentPower);
            playerHealth.healthBar.SetHealth(playerHealth.currentHealth);  
            characterController.jumpForce +=1;
            characterController.speed +=1;
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FoxPowerGems"))
        {
            AudioSource.PlayClipAtPoint(audioClip,other.transform.position);
            currentPower +=1;
            Destroy(other.gameObject); 
        }
        scoreBar.SetScore(currentPower);
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentPower==minPower+1)
        {
          AudioSource.PlayClipAtPoint(audioClip2,other.transform.position);  
        }
     
    }
}
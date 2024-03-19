using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth=100;
    public int currentHealth;
    public HealthBar healthBar;
    
    
    void Start()
    {
        currentHealth=maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }
   
    public void TakeDamage(int damage=20)
    {   
        currentHealth -= damage;
      
        if (currentHealth<=0)
        {   
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        healthBar.SetHealth(currentHealth);
    }
   
      
    
    
        public void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("HealUpper"))
        {   if(currentHealth>=100)
            {
                currentHealth -=25;
            }
            currentHealth +=25;
        }
        healthBar.SetHealth(currentHealth);

        if(other.gameObject.CompareTag("Blocker"))
        {   
            currentHealth -=20;
            if(currentHealth<=0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
        healthBar.SetHealth(currentHealth);
        
    }
    
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] public AudioClip audioClip;
    public ScoreBar scoreBar;
    int minScore=0;
    int currentScore;

    void Start()
    {
        currentScore=minScore;
        scoreBar.SetScore(minScore);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectibles"))
        {
            AudioSource.PlayClipAtPoint(audioClip,other.transform.position);
            currentScore +=1;
            Destroy(other.gameObject); 
        }
        scoreBar.SetScore(currentScore);
        
        if(other.gameObject.CompareTag("TutorialCollectibles"))
        {
            AudioSource.PlayClipAtPoint(audioClip,other.transform.position);
            currentScore +=1;
            Destroy(other.gameObject); 
           
        }
        scoreBar.SetScore(currentScore);
        
    }

   
}
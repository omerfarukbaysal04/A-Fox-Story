using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
     public void Oyna()
    {
        SceneManager.LoadScene("WelcomeScene TR");
    }
     public void Basla()
    {
        SceneManager.LoadScene("Level1 TR");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Secenekler()
    {
        SceneManager.LoadScene("Options TR");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
    public void Jenerik()
    {
        SceneManager.LoadScene("CreditsMenu TR");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Turkish()
    {
        SceneManager.LoadScene("MainMenu TR");
    }
    public void English()
    {
        SceneManager.LoadScene("MainMenu");
    }



}

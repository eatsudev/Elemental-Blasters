using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Map");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("quitted");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1.0f;
    }
}

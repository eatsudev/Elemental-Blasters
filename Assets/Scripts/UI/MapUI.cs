using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUI : MonoBehaviour
{
    private Animator animator;

    public void TutorialButton()
    {
        SceneManager.LoadScene("TutorialLab");
        Time.timeScale = 1.0f;
    }
    public void City2Button()
    {
        SceneManager.LoadScene("CityLv2");
        Time.timeScale = 1.0f;
    }
    public void City3Button()
    {
        SceneManager.LoadScene("CityLv3");
        Time.timeScale = 1.0f;
    }
    public void LabButton()
    {
        SceneManager.LoadScene("LabLv4");
        Time.timeScale = 1.0f;
    }
    public void HoverEnter()
    {
        animator.SetBool("Hover", true);    }
    public void HoverExit()
    {
        animator.SetBool("Hover", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUI : MonoBehaviour
{
    public void TutorialButton()
    {
        SceneManager.LoadScene("TutorialLab");
    }
    public void City2Button()
    {
        SceneManager.LoadScene("CityLv2");
    }
    public void City3Button()
    {
        SceneManager.LoadScene("CityLv3");
    }
    public void LabButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject Dead;
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Dead.SetActive(false);
        Time.timeScale = 1f;
    }
}

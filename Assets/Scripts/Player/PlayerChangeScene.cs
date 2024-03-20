using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerChangeScene : MonoBehaviour
{
    public string sceneName;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "sceneCollider")
        {
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1.0f;
        }
    }
}

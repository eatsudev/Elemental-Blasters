using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private GameObject pausePanel;
    private bool pause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UserInput.instance.controls.Interact.Pause.WasPressedThisFrame() && !pause)
        {
            Pause();
        }
        else if(UserInput.instance.controls.Interact.Pause.WasPressedThisFrame() && pause)
        {
            UnPause();
        }
    }

    private void Pause()
    {
        Animator[] allAnimator = GameObject.FindObjectsOfType<Animator>();

        foreach (Animator anim in allAnimator) {
            anim.speed = 0f;
            anim.enabled = false;
        }
        
        pause = true;
        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }
    private void UnPause()
    {
        Animator[] allAnimator = GameObject.FindObjectsOfType<Animator>();

        foreach (Animator anim in allAnimator)
        {
            anim.speed = 1f;
            anim.enabled = true;
        }

        pause = false;
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }
}

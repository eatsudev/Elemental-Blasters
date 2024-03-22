using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementalWheelController : MonoBehaviour
{
    public int elementID;
    public Animator animator;
    public Image selectedItem;
    public Sprite noImage;

    private GameManager gameManager;

    private bool weaponWheelSelected;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Update()
    {
        if (UserInput.instance.controls.Interact.ElementalWheel.WasPressedThisFrame())
        {
            Interacted();
        }

        switch (elementID)
        {
            case 0:
                Debug.Log("0");
                break;
            case 1:
                Debug.Log("1");
                break;
            case 2:
                Debug.Log("2");
                break;
            case 3:
                Debug.Log("3");
                break;
            case 4:
                Debug.Log("4");
                break;
            case 5:
                Debug.Log("5");
                break;
            case 6:
                Debug.Log("6");
                break;
        }
    }
    public void Interacted()
    {
        weaponWheelSelected = !weaponWheelSelected;
        if (weaponWheelSelected)
        {
            animator.SetTrigger("OpenWheel");
            WheelSlowDown();
        }
        else
        {
            animator.SetTrigger("CloseWheel");
            WheelResume();
        }
    }
    public int GetElementID()
    {
        return elementID;
    }
    public void WheelSlowDown()
    {
        gameManager.elementalWheelSlowDown = true;
        animator.speed = 5f;

        Time.timeScale = 0.2f;
    }

    public IEnumerator WheelStop()
    {
        gameManager.elementalWheelSlowDown = true;
        animator.speed = 5f;

        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 0f;
    }

    public void WheelResume()
    {
        gameManager.elementalWheelSlowDown = false;
        animator.speed = 1f;

        Time.timeScale = 1f;
    }
}
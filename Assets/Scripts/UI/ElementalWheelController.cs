using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementalWheelController : MonoBehaviour
{
    public static int elementID;
    public Animator animator;
    public Image selectedItem;
    public Sprite noImage;

    private bool weaponWheelSelected;
    void Update()
    {
        if (UserInput.instance.controls.Interact.ElementalWheel.WasPressedThisFrame())
        {
            weaponWheelSelected = !weaponWheelSelected;
            if (weaponWheelSelected)
            {
                animator.SetTrigger("OpenWheel");
            }
            else
            {
                animator.SetTrigger("CloseWheel");
            }
        }
        
        switch (elementID)
        {
            case 0:
                //selectedItem.sprite = noImage;
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
}

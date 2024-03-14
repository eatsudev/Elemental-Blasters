using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;
    [HideInInspector] public InputKeyboardController controls;
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool attack;
    [HideInInspector] public bool enterAim;
    [HideInInspector] public bool elementalWheel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        controls = new InputKeyboardController();

        controls.movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.attack.Attack.performed += ctx => attack = ctx.ReadValueAsButton();
        controls.attack.EnterAimMode.performed += ctx => enterAim = ctx.ReadValueAsButton();
        controls.Interact.ElementalWheel.performed += ctx => elementalWheel = ctx.ReadValueAsButton();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

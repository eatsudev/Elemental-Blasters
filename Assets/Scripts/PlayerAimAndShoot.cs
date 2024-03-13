using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private Player player;
    private GameObject bulletInst;
    private Vector2 worldPosition;
    private Vector2 direction;

    private Vector3 originalScale;
    private Vector3 originalPlayerScale;

    private bool isAiming;

    void Start()
    {
        player = GetComponent<Player>();
        isAiming = false;

        originalScale = gun.transform.localScale;
        originalPlayerScale = transform.localScale;
    }

    private void Update()
    {
        if (IsAiming())
        {
            HandleGunRotation();
            HandleGunShooting();
            AimModePlayerRotation();
        }
        else
        {
            PlayerRotation();
        }

        gun.SetActive(IsAiming());

        if (Mouse.current.rightButton.wasPressedThisFrame) isAiming = !isAiming;
    }
    public bool IsAiming()
    {
        return isAiming;
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 scale = gun.transform.localScale;

        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (!player.PlayerFacingRight())
        {
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            scale.x = originalScale.x * -1f;
            scale.y = originalScale.y * -1f;
            gun.transform.localScale = scale;
        }
        else
        {
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            scale.x = originalScale.x * 1f;
            scale.y = originalScale.y * 1f;
            gun.transform.localScale = scale;
        }
    }

    private void PlayerRotation()
    {
        Vector3 playerScale = player.transform.localScale;

        if(player.PlayerMoveDirection() > 0)
        {
            playerScale.x = originalPlayerScale.x;
        }
        else if(player.PlayerMoveDirection() < 0)
        {
            playerScale.x = -originalPlayerScale.x;
        }
       
        player.transform.localScale = playerScale;
    }

    private void AimModePlayerRotation()
    {
        Vector3 playerScale = player.transform.localScale;

        if (direction.x > 0)
        {
            playerScale.x = originalPlayerScale.x;
        }
        else if (direction.x < 0)
        {
            playerScale.x = -originalPlayerScale.x;
        }

        player.transform.localScale = playerScale;
    }

    private void HandleGunShooting()
    {
        // Enable or disable the gun based on the player's movement
        

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }
    }
}

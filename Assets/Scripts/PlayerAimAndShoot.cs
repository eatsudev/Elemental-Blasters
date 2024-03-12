using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private GameObject bulletInst;
    private Vector2 worldPosition;
    private Vector2 direction;

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle > 90 || angle < -90)
        {
            gun.transform.rotation = Quaternion.Euler(0f, 180f, -angle);
        }
        else
        {
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }
    }
}

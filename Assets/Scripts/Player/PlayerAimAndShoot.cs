using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject fireProjectile;
    [SerializeField] private GameObject waterProjectile;
    [SerializeField] private GameObject dirtProjectile;
    [SerializeField] private GameObject shockProjectile;
    [SerializeField] private GameObject windProjectile;
    [SerializeField] private Transform bulletSpawnPoint;
    private Player player;
    private ElementalWheelController wheelController;
    
    private Vector2 worldPosition;
    private Vector2 direction;

    private Vector3 originalScale;
    private Vector3 originalPlayerScale;

    void Start()
    {
        player = GetComponent<Player>();
        wheelController = FindObjectOfType<ElementalWheelController>();

        originalScale = gun.transform.localScale;
        originalPlayerScale = transform.localScale;
    }
    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
        HandleElementalShooting();

        //Debug.Log(wheelController.elementID);
        //Debug.Log(wheelController.GetElementID() == 1);    
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 scale = gun.transform.localScale;
        Vector3 playerScale = player.transform.localScale;

        playerScale.x = (direction.x > 0) ? originalPlayerScale.x : -originalPlayerScale.x;

        player.transform.localScale = playerScale;

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

    private void HandleGunShooting()
    {
        // Enable or disable the gun based on the player's movement
        gun.SetActive(!player.IsMoving());

        if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            GameObject bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }
        
    }

    private void HandleElementalShooting()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {

            Debug.Log(wheelController.elementID);
            if (wheelController.GetElementID() == 1)
            {
                GameObject fireInst = Instantiate(fireProjectile, bulletSpawnPoint.position, gun.transform.rotation);
            }
            if (wheelController.GetElementID() == 2)
            {
                GameObject waterInst = Instantiate(waterProjectile, bulletSpawnPoint.position, gun.transform.rotation);
            }
            if (wheelController.GetElementID() == 3)
            {
                GameObject shockInst = Instantiate(shockProjectile, bulletSpawnPoint.position, gun.transform.rotation);
            }
            if (wheelController.GetElementID() == 4)
            {
                GameObject windInst = Instantiate(windProjectile, bulletSpawnPoint.position, gun.transform.rotation);
            }
            if (wheelController.GetElementID() == 5)
            {
                GameObject dirtInst = Instantiate(dirtProjectile, bulletSpawnPoint.position, gun.transform.rotation);
            }
            else
            {
                Debug.Log("No Elements");
            }

        }
    }
}

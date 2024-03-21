using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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
    [SerializeField] private AudioSource shootSFX;
    [SerializeField] private Text elementalCounterText;
    [SerializeField] private int maxElementalShots = 3;

    private Player player;
    private ElementalWheelController wheelController;
    
    private Vector2 worldPosition;
    private Vector2 direction;
    private int elementalShotsRemaining;

    private Vector3 originalScale;
    private Vector3 originalPlayerScale;

    void Start()
    {
        player = GetComponent<Player>();
        wheelController = FindObjectOfType<ElementalWheelController>();

        originalScale = gun.transform.localScale;
        originalPlayerScale = transform.localScale;

        elementalShotsRemaining = maxElementalShots;
        UpdateElementalCounterUI();
    }
    private void Update()
    {
        HandleGunRotation();
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

    /*private void HandleGunShooting()
    {
        // Enable or disable the gun based on the player's movement
        gun.SetActive(!player.IsMoving());

        if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            GameObject bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
        }
        
    }*/

    private void HandleElementalShooting()
    {
        gun.SetActive(!player.IsMoving());
        if (wheelController.GetElementID() != 0 && Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject() && elementalShotsRemaining > 0)
        {
            elementalShotsRemaining--;

            UpdateElementalCounterUI();

            //sfx
            shootSFX.Play();

            Debug.Log(wheelController.elementID);
            switch (wheelController.GetElementID())
            {
                case 1:
                    Instantiate(fireProjectile, bulletSpawnPoint.position, gun.transform.rotation);
                    break;
                case 2:
                    Instantiate(waterProjectile, bulletSpawnPoint.position, gun.transform.rotation);
                    break;
                case 3:
                    Instantiate(shockProjectile, bulletSpawnPoint.position, gun.transform.rotation);
                    break;
                case 4:
                    Instantiate(windProjectile, bulletSpawnPoint.position, gun.transform.rotation);
                    break;
                case 5:
                    Instantiate(dirtProjectile, bulletSpawnPoint.position, gun.transform.rotation);
                    break;
                default:
                    Debug.Log("No Elements");
                    break;
            }

        }
    }


    private void UpdateElementalCounterUI()
    {
        elementalCounterText.text = ": " + elementalShotsRemaining.ToString();
    }
}

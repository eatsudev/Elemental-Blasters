using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxVirusBlasterSpawner : MonoBehaviour
{
    [SerializeField] private VirusBlaster virusBlasterPrefabs;
    [SerializeField] private float phase1ShootCooldown;
    [SerializeField] private float phase2ShootCooldown;

    public Animator animator;

    private VirusBlasterSpawnPoint[] virusBlasterSpawnPoints;

    private VirusBlaster spawnedVirusBlaster;
    private ChronoxHealth chronoxHealth;

    void Start()
    {
        virusBlasterSpawnPoints = FindObjectsOfType<VirusBlasterSpawnPoint>();
        chronoxHealth = GetComponent<ChronoxHealth>();
    }

    public IEnumerator SpawnAllVirusBlaster(int phase)
    {

        yield return new WaitForSeconds(2f);

        foreach (VirusBlasterSpawnPoint spawnPoint in virusBlasterSpawnPoints)
        {
            if(spawnPoint.virusBlaster == null)
            {
                spawnedVirusBlaster = Instantiate(virusBlasterPrefabs, spawnPoint.transform.position, Quaternion.identity);

                spawnedVirusBlaster.SetShootCooldown(phase == 1 ? phase1ShootCooldown : phase2ShootCooldown);

                spawnPoint.virusBlaster = spawnedVirusBlaster;
            }
            else
            {
                spawnedVirusBlaster.SetShootCooldown(phase == 1 ? phase1ShootCooldown : phase2ShootCooldown);
            }
        }

        chronoxHealth.flag = 1;
}
}

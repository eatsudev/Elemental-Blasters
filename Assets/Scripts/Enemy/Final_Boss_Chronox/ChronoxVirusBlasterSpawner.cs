using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxVirusBlasterSpawner : MonoBehaviour
{
    [SerializeField] private VirusBlaster virusBlasterPrefabs;
    [SerializeField] private VirusBlasterSeed virusBlasterSeedPrefabs;
    [SerializeField] private float phase1ShootCooldown;
    [SerializeField] private float phase2ShootCooldown;
    [SerializeField] private float seedSpeed;

    public Animator animator;

    private VirusBlasterSpawnPoint[] virusBlasterSpawnPoints;

    private VirusBlaster spawnedVirusBlaster;
    private VirusBlasterSeed virusBlasterSeed;
    private ChronoxHealth chronoxHealth;

    void Start()
    {
        virusBlasterSpawnPoints = FindObjectsOfType<VirusBlasterSpawnPoint>();
        chronoxHealth = GetComponent<ChronoxHealth>();
    }

    public IEnumerator SpawnAllVirusBlaster(int phase)
    {
        chronoxHealth.flag = 1;

        yield return new WaitForSeconds(1f);

        foreach (VirusBlasterSpawnPoint spawnPoint in virusBlasterSpawnPoints)
        {
            virusBlasterSeed = Instantiate(virusBlasterSeedPrefabs, transform.position, Quaternion.identity);
            virusBlasterSeed.target = spawnPoint.transform.position;
            virusBlasterSeed.speed = seedSpeed;
        }

        yield return new WaitForSeconds(1f);

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
                spawnedVirusBlaster.currHealth = spawnedVirusBlaster.MaxHP();
            }
        }

        chronoxHealth.flag = 2;
    }
}

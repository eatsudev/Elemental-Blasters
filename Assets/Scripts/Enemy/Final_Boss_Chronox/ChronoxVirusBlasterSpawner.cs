using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoxVirusBlasterSpawner : MonoBehaviour
{
    [SerializeField] private VirusBlaster virusBlasterPrefabs;
    [SerializeField] private float phase1ShootCooldown;
    [SerializeField] private float phase2ShootCooldown;

    private VirusBlasterSpawnPoint[] virusBlasterSpawnPoints;

    private VirusBlaster spawnedVirusBlaster;

    void Start()
    {
        virusBlasterSpawnPoints = FindObjectsOfType<VirusBlasterSpawnPoint>();
    }

    public void SpawnAllVirusBlaster(int phase)
    {
        foreach(VirusBlasterSpawnPoint spawnPoint in virusBlasterSpawnPoints)
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
    }
}

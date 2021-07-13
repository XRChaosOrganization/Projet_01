using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawnerComponent : SpawnerBehavior
{
    public Transform splashContainer; 

    public override void SpawnSplash()
    {
        if (spawnTraj.spawnDirections[0] != null)
        {
            Instantiate(splashPrefab, spawnTraj.spawnDirections[0].target.transform.position, spawnTraj.spawnDirections[0].target.transform.rotation, splashContainer.transform);
            
        }
    }
}

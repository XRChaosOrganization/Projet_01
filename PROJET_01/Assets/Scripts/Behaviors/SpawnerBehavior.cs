using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{

    public GameObject splashPrefab;
    [SerializeField] float timeOffset;
    [SerializeField] float spawnCooldown;
    public bool stopSpawning = false;

    [HideInInspector] public SpawnerTrajectoryComponent spawnTraj;     

    void Awake()
    {
        spawnTraj = this.gameObject.GetComponent<SpawnerTrajectoryComponent>();
    }

    void Start()
    {
        InvokeRepeating("SpawnSplash", timeOffset, spawnCooldown);
    }

    public virtual void SpawnSplash()
    {

        if (spawnTraj.spawnDirections[0] != null)
        {
            Instantiate(splashPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            if (stopSpawning)
            {
                CancelInvoke("SpawnSplash");
            }
        }

        
        
    }
    /*


    Liste de splash a spawn dans ce spawner (dans l'ordre de spawn)
        - type se splash
        - nombre de rebonds


    Spawn chaque splash dans la direction sp�cifi�e (ou random dans la zone de spread)
    avec v�locit� (sp�cifique � la direction, no random )


    si pas de direction sp�cifi�e ?
        - target pos = spawner center, velocity depend on target


    si plusieurs directions ?
        - une liste de splash par direction ?


















    */
}

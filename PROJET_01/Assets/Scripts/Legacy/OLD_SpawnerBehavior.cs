using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_SpawnerBehavior : MonoBehaviour
{
    [SerializeField] float timeOffset;
    [SerializeField] float spawnCooldown;
    bool removed;

    int selectedDir;

    public Transform splashContainer;
    [HideInInspector] public OLD_SpawnerSetupComponent Setup;
    
    void Awake()
    {
        Setup = this.gameObject.GetComponent<OLD_SpawnerSetupComponent>();

        selectedDir = 0;

        foreach (OLD_SpawnerSetupComponent.SpawnSetup item in Setup.spawnSetup)
        {
            if (item.splashList.Length <= 0)
            {
                Setup.spawnSetup.Remove(item);
            }
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnSplash", timeOffset, spawnCooldown); //Time dependant, if timescale is changed, this will be invoked at a different scale 
    }

    public virtual void SpawnSplash()
    {
        if (Setup.spawnSetup.Count > 0 )
        {
            do
            {
                if (Setup.spawnSetup[selectedDir].index < Setup.spawnSetup[selectedDir].splashList.Length)
                {
                    InitSpawn();
                    break;

                }

            } while (Setup.spawnSetup[selectedDir].index < Setup.spawnSetup[selectedDir].splashList.Length);

            if (Setup.spawnSetup.Count > 0 && !removed)
            {
                CycleDir();

            }
            else if (selectedDir >= Setup.spawnSetup.Count)
            {
                selectedDir = 0;
            } 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void CycleDir()
    {
        if (selectedDir >= Setup.spawnSetup.Count - 1)
            selectedDir = 0;
        else selectedDir++;
    }

    void InitSpawn()
    {
        removed = false;

        GameObject obj = Instantiate(Setup.spawnSetup[selectedDir].splashList[Setup.spawnSetup[selectedDir].index].splashType, this.gameObject.transform.position, this.gameObject.transform.rotation);
        SplashBehavior sb = obj.GetComponent<SplashBehavior>();
        SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();


        sb.splash.InitSplash(Setup.spawnSetup[selectedDir].splashList[Setup.spawnSetup[selectedDir].index].initialBounces);
        sr.color = sb.splashColor[Setup.spawnSetup[selectedDir].splashList[Setup.spawnSetup[selectedDir].index].initialBounces];

        float xMax = Setup.spawnSetup[selectedDir]._dir.x * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)) + (Setup.spawnSetup[selectedDir]._dir.y * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)));
        float xMin = Setup.spawnSetup[selectedDir]._dir.x * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)) - (Setup.spawnSetup[selectedDir]._dir.y * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)));

        float yMax = Setup.spawnSetup[selectedDir]._dir.y * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)) - (Setup.spawnSetup[selectedDir]._dir.x * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)));
        float yMin = Setup.spawnSetup[selectedDir]._dir.y * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)) + (Setup.spawnSetup[selectedDir]._dir.x * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle/2)));

        Vector2 spread = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));


        obj.GetComponent<Rigidbody2D>().AddForce(spread.normalized * Setup.spawnSetup[selectedDir].velocity * sb.maxVelocity);

        Setup.spawnSetup[selectedDir].index++;

        if (Setup.spawnSetup[selectedDir].index >= Setup.spawnSetup[selectedDir].splashList.Length)
        {
            Setup.spawnSetup.RemoveAt(selectedDir);
            removed = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{


    [SerializeField] float timeOffset;
    [SerializeField] float spawnCooldown;
    bool removed;


    public int selectedDir;


    public Transform splashContainer;
    [HideInInspector] public SpawnerSetupComponent Setup;
    
    void Awake()
    {
        Setup = this.gameObject.GetComponent<SpawnerSetupComponent>();

        selectedDir = 0;

        foreach (SpawnerSetupComponent.SpawnSetup item in Setup.spawnSetup)
        {
            if (item.splashList.Length <= 0)
            {
                Setup.spawnSetup.Remove(item);
            }
        }

    }

    void Start()
    {

        InvokeRepeating("SpawnSplash", timeOffset, spawnCooldown);
        
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

        float xMax = Setup.spawnSetup[selectedDir]._dir.x * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)) + (Setup.spawnSetup[selectedDir]._dir.y * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)));
        float xMin = Setup.spawnSetup[selectedDir]._dir.x * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)) - (Setup.spawnSetup[selectedDir]._dir.y * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)));

        float yMax = Setup.spawnSetup[selectedDir]._dir.y * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)) - (Setup.spawnSetup[selectedDir]._dir.x * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)));
        float yMin = Setup.spawnSetup[selectedDir]._dir.y * Mathf.Cos(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)) + (Setup.spawnSetup[selectedDir]._dir.x * Mathf.Sin(Setup.ToRadian(Setup.spawnSetup[selectedDir].angle)));

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

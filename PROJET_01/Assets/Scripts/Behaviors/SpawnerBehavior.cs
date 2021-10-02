using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    Transform splashContainer;


    [Header("Settings")]
    public float startOffset;
    public float spawnCooldown;

    [Space]
    [Header("Debug")]
    private bool hasWaitedForStartOffset = false;
    private float time = 0.0f;
    private int currentSpawnSetup = 0;

    //public enum TargetAimingMode { WORLD, LOCAL };
    //public TargetAimingMode targetAimingMode;

    private SpawnerSetupComponent setup;

    void Awake()
    {
        splashContainer = GameObject.Find("SplashesContainer").transform;

        setup = this.gameObject.GetComponent<SpawnerSetupComponent>();

        foreach (SpawnerSetupComponent.SpawnSetup item in setup.spawnSetup)
        {
            if (item.splashList.Count <= 0)
            {
                setup.spawnSetup.Remove(item);
            }
        }

        //if (loopMovement)
        //    originalPos = this.transform.position;
    }

    void Start()
    {
        GetComponent<GameObjectAnchoringComponent>().enabled = false; //If we want to move the Go deactivate anchoring component
    }

    private void Update()
    {
        //if (isSpawnerMoving)
        //{
        //    MoveSpawner();
        //    
        //    if(targetAimingMode == TargetAimingMode.WORLD)
        //        KeepTargetsAtPosition();
        //}

        if (setup.spawnSetup.Count <= 0) return; 

        time += Time.deltaTime;
        if (time >= startOffset && hasWaitedForStartOffset == false)
        {
            hasWaitedForStartOffset = true;
            time = 0.0f;
        }

        if(hasWaitedForStartOffset)
        {
            if(time >= spawnCooldown)
            {
                SpawnSplash(setup.spawnSetup[currentSpawnSetup]);
                
                if (currentSpawnSetup + 1 > setup.spawnSetup.Count - 1)
                    currentSpawnSetup = 0;
                else
                    currentSpawnSetup++;

                time = 0.0f;
            }
        }
    }

    private void SpawnSplash(SpawnerSetupComponent.SpawnSetup _setup)
    {
        GameObject obj = Instantiate(_setup.splashList[0].splashPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        obj.transform.parent = splashContainer;
        SplashBehavior sb = obj.GetComponent<SplashBehavior>();
        

        sb.splash.InitSplash(_setup.splashList[0].initialBounces);

        foreach (SpriteRenderer sr in sb.colorChangeLayers)
        {
            sr.color = sb.splashColor[_setup.splashList[0].initialBounces];
        }
        

        float xMax = _setup.Direction.x * Mathf.Cos(setup.ToRadian(_setup.angle / 2)) + (_setup.Direction.y * Mathf.Sin(setup.ToRadian(_setup.angle / 2)));
        float xMin = _setup.Direction.x * Mathf.Cos(setup.ToRadian(_setup.angle / 2)) - (_setup.Direction.y * Mathf.Sin(setup.ToRadian(_setup.angle / 2)));

        float yMax = _setup.Direction.y * Mathf.Cos(setup.ToRadian(_setup.angle / 2)) - (_setup.Direction.x * Mathf.Sin(setup.ToRadian(_setup.angle / 2)));
        float yMin = _setup.Direction.y * Mathf.Cos(setup.ToRadian(_setup.angle / 2)) + (_setup.Direction.x * Mathf.Sin(setup.ToRadian(_setup.angle / 2)));

        Vector2 spread = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

        obj.GetComponent<Rigidbody2D>().AddForce(spread.normalized * _setup.velocity * sb.maxVelocity);
        obj.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2f, 2f));

        if (_setup.infiniteSpawn == false)
        {
            _setup.splashList.RemoveAt(0);

            if(_setup.splashList.Count <= 0)
            {
                setup.spawnSetup.Remove(_setup);
            }
        }
    }

    //public void KeepTargetsAtPosition ()
    //{
    //    for (int i = 0; i < setup.spawnSetup.Count; i++)
    //    {
    //        setup.spawnSetup[i].target.transform.position = setup.spawnSetup[i].originalPos;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnerSetupComponent : MonoBehaviour
{
    [System.Serializable]
    public class SplashSetup
    {
        public GameObject splashPrefab;
        [Range(1, 7)] public int initialBounces = 7;
    }

    [System.Serializable]
    public class SpawnSetup
    {
        public SpawnSetup() {}

        [Header("Debug")]
        public bool drawRays = true;
        public bool infiniteSpawn = false; 

        [Header("Trajectory")]
        public GameObject target;
        public Vector3 originalPos; 
        [Range(0, 100)] [Tooltip("en % de la vitesse max d'un splash")] public int velocity;
        [Range(0f, 180f)] [Tooltip("Angle en Degrés °")] public float angle;

        [Header("Splashes")]
        public List<SplashSetup> splashList;

        //Angles of the directions we're spawning splashes 
        private Vector2 direction;
        public Vector2 Direction { get; set; }

        private Vector2 spread1;
        public Vector2 Spread1 { get; set; }

        private Vector2 spread2;
        public Vector2 Spread2 { get; set; }
    }

    public Transform targetRoot; 
    public Transform splashRoot; 
    public List<SpawnSetup> spawnSetup;

    private void Start()
    {
        for (int i = 0; i < spawnSetup.Count; i++)
        {
            spawnSetup[i].originalPos = spawnSetup[i].target.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (SpawnSetup direction in spawnSetup)
        {
            if (direction != null && direction.drawRays)
            {
                // Draw Ray to Target
                direction.Direction = direction.target.transform.localPosition;
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.Direction * 30);

                // Draw Rays at specified angle
                float x1 = direction.Direction.x * Mathf.Cos(ToRadian(direction.angle / 2)) + direction.Direction.y * Mathf.Sin(ToRadian(direction.angle / 2));
                float y1 = direction.Direction.y * Mathf.Cos(ToRadian(direction.angle / 2)) - direction.Direction.x * Mathf.Sin(ToRadian(direction.angle / 2));

                direction.Spread1 = new Vector2(x1, y1);

                float x2 = direction.Direction.x * Mathf.Cos(ToRadian(direction.angle / 2)) - direction.Direction.y * Mathf.Sin(ToRadian(direction.angle / 2));
                float y2 = direction.Direction.y * Mathf.Cos(ToRadian(direction.angle / 2)) + direction.Direction.x * Mathf.Sin(ToRadian(direction.angle / 2));

                direction.Spread2 = new Vector2(x2, y2);

                Gizmos.color = Color.green;

                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.Spread1 * 30);
                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.Spread2 * 30);
            }
            else return;
        }
    }

    public float ToRadian(float angle)
    {
        float rAngle = Mathf.PI * angle / 180;
        return rAngle;
    }

    public void AddNewSetup ()
    {
        //Create new setup
        SpawnSetup setup = new SpawnSetup();

        //Create new target & assign it to the setup + set good parenting 
        GameObject temp = Instantiate(new UnityEngine.GameObject());
        temp.transform.SetParent(targetRoot.transform);
        temp.name = spawnSetup.Count.ToString();
        temp.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 5.5f, this.transform.position.z);
        temp.gameObject.layer = this.gameObject.layer;
        setup.target = temp;

        //Add setup to list 
        spawnSetup.Add(setup);
    }

    public void CleanSetupTargets ()
    {
        List<GameObject> existingTargets = new List<GameObject>();

        //First we fetch all existing targets in the hierarchy 
        //Populate list of targets with existing one
        for (int i = 0; i < targetRoot.transform.childCount; i++)
        {
            existingTargets.Add(targetRoot.transform.GetChild(i).gameObject);
        }
        if(spawnSetup.Count > 0)
        {
            //Remove all the target that are referenced in the spawn setup 
            for (int i = 0; i < spawnSetup.Count; i++)
            {
                existingTargets.Remove(spawnSetup[i].target);
            }
        }
        //Destroy existing targets without references in setup 
        for (int i = 0; i < existingTargets.Count; i++)
        {
            DestroyImmediate(existingTargets[i].gameObject);
        }
    }
}

//we need this check if we want to be able to build the project, else it will count as a function used in the game (which is not) 
#if UNITY_EDITOR 
[CustomEditor(typeof(SpawnerSetupComponent))]
public class SpawnerSetupComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SpawnerSetupComponent component = (SpawnerSetupComponent)target;
        GUI.backgroundColor = Color.green;

        if(GUILayout.Button("Add new setup"))
        {
            component.AddNewSetup();
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clean setup target(s) in herarchy"))
        {
            component.CleanSetupTargets();
        }

        GUI.backgroundColor = Color.white;

        //Draw base inspector
        base.OnInspectorGUI();
    }
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_SpawnerSetupComponent : MonoBehaviour
{
    [System.Serializable]
    public class SplashSetup
    {
        public GameObject splashType;
        [Range(1, 7)] public int initialBounces = 7;
    }

    [System.Serializable]
    public class SpawnSetup
    {
        [HideInInspector] public bool stopSpawning;

        [Header("Trajectory")]
        public bool drawRays = false;
        public GameObject target;
        [Range(0, 100)] [Tooltip("en % de la vitesse max d'un splash")] public int velocity;  
        [Range(0f, 180f)][Tooltip("Angle en Degrés °")]public float angle;
        
        //For SpawnerBehavior
        [HideInInspector] public Vector2 _dir;
        [HideInInspector] public Vector2 spread1;
        [HideInInspector] public Vector2 spread2;

        //[Header("Timing")]
        //public float timeOffset;
        //public float spawnCooldown;

        [Header("")]
        public SplashSetup[] splashList;
        [HideInInspector] public int index = 0;
    }

    public List<SpawnSetup> spawnSetup;

    private void OnDrawGizmos()
    { 
        foreach (SpawnSetup direction in spawnSetup)
        {
            if (direction != null && direction.drawRays)
            {
                // Draw Ray to Target
                direction._dir = direction.target.transform.localPosition;
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction._dir * 30);


                // Draw Rays at specified angle
                float x1 = direction._dir.x * Mathf.Cos(ToRadian(direction.angle/2)) + direction._dir.y * Mathf.Sin(ToRadian(direction.angle/2));
                float y1 = direction._dir.y * Mathf.Cos(ToRadian(direction.angle/2)) - direction._dir.x * Mathf.Sin(ToRadian(direction.angle/2));

                direction.spread1 = new Vector2(x1, y1);

                float x2 = direction._dir.x * Mathf.Cos(ToRadian(direction.angle/2)) - direction._dir.y * Mathf.Sin(ToRadian(direction.angle/2));
                float y2 = direction._dir.y * Mathf.Cos(ToRadian(direction.angle/2)) + direction._dir.x * Mathf.Sin(ToRadian(direction.angle/2));

                direction.spread2 = new Vector2(x2, y2);

                Gizmos.color = Color.green;

                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.spread1 * 30);
                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.spread2 * 30);
            }
            else return;
        }
    }

    public float ToRadian(float angle)
    {
        float rAngle = Mathf.PI * angle / 180;
        return rAngle;
    }
}

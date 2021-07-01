using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrajectoryComponent : MonoBehaviour
{
    [System.Serializable]
    public class SpawnDirection
    {
        public GameObject target;
        //public float velocity;
        [Range(0f, 90f)]public float angle; // En °
        public bool draw = false;

        //Can be used in SpawnerBehavior
        [HideInInspector] public Vector2 _dir;
        [HideInInspector] public Vector2 spread1;
        [HideInInspector] public Vector2 spread2;
        
        
    }


    public SpawnDirection[] spawnDirections;


    private void OnDrawGizmos()
    {

        foreach (SpawnDirection direction in spawnDirections)
        {
            if (direction != null && direction.draw)
            {
                // Draw Ray to Target
                direction._dir = direction.target.transform.localPosition;
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction._dir * 30);


                // Draw Rays at specified angle
                float x1 = direction._dir.x * Mathf.Cos(ToRadian(direction.angle)) + direction._dir.y * Mathf.Sin(ToRadian(direction.angle));
                float y1 = direction._dir.y * Mathf.Cos(ToRadian(direction.angle)) - direction._dir.x * Mathf.Sin(ToRadian(direction.angle));

                direction.spread1 = new Vector2(x1, y1);

                float x2 = direction._dir.x * Mathf.Cos(ToRadian(direction.angle)) - direction._dir.y * Mathf.Sin(ToRadian(direction.angle));
                float y2 = direction._dir.y * Mathf.Cos(ToRadian(direction.angle)) + direction._dir.x * Mathf.Sin(ToRadian(direction.angle));

                direction.spread2 = new Vector2(x2, y2);

                Gizmos.color = Color.green;

                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.spread1 * 30);
                Gizmos.DrawRay(this.gameObject.transform.localPosition, direction.spread2 * 30);
            }
            else return;
            
        }

    }



    float ToRadian(float angle)
    {
        float rAngle = Mathf.PI * angle / 180;
        return rAngle;
    }




}

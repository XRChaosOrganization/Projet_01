using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallComponent : ObstacleBehavior
{

    public int hitNumber;


    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.CompareTag("Splash"))
        {
            hitNumber--;

            if (hitNumber <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }


}

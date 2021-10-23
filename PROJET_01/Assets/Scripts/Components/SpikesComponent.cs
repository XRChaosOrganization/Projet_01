using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesComponent : ObstacleBehavior
{

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.CompareTag("Splash"))
        {
            Destroy(_collision.gameObject);
        }
    }


}

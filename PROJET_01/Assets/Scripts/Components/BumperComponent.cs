using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class BumperComponent : ObstacleBehavior
{
    BoxCollider2D trigger;
    SpriteRenderer sr;

    [Range(0f, 1f)]
    public float bumpSpeed;


    private void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (trigger.size.y != sr.size.y - 0.1f)
        {
            Vector2 resize = new Vector2(trigger.size.x, sr.size.y - 0.05f);
            trigger.size = resize;
        }
    }

    private void OnCollisionEnter2D(Collision2D _col)
    {
        

        if (_col.collider.CompareTag("Splash") && _col.otherCollider.GetType() == typeof(BoxCollider2D))
        {
            Rigidbody2D rb = _col.gameObject.GetComponent<Rigidbody2D>();
            Splash splash = _col.gameObject.GetComponent<SplashBehavior>().splash;
            rb.velocity = Vector2.zero;
            rb.AddForce(this.transform.right * splash.maxVelocity * bumpSpeed, ForceMode2D.Impulse);
            
        }
    }

}

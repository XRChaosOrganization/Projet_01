using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBehavior : MonoBehaviour
{

    public Splash splash;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public Color[] splashColor;

    //Pour tester les metrics
    public float maxVelocity;
    [Range(0, 7)] public int initialBounces;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        splash.InitSplash(initialBounces);
        sr.color = splashColor[splash.CurrentBounces];
    }

    private void Update()
    {
        splash.maxVelocity = maxVelocity; //Pour tester les metrics

        if (rb.velocity.magnitude > splash.maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * splash.maxVelocity;
        }
    }


    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.CompareTag("Obstacle"))
        {
            if (splash.CurrentBounces <= 0)
                Destroy(this.gameObject);
            else
            {
                splash.CurrentBounces--;
                sr.color = splashColor[splash.CurrentBounces];
            }
        }

        AdditionnalCollisionEnter(_collision);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other.CompareTag("Goal"))
        {
            Debug.Log("Scored !");
            Destroy(this.gameObject);
        }

        if (_other.CompareTag("Killzone"))
        {
            Destroy(this.gameObject);
        }

        AdditionnalTriggerEnter(_other);
    }
    public virtual void AdditionnalCollisionEnter(Collision2D _collision = null) { }
    public virtual void AdditionnalTriggerEnter(Collider2D _trigger = null) { }
}
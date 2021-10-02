using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBehavior : MonoBehaviour
{

    public Splash splash;
    Rigidbody2D rb;
    public SpriteRenderer[] colorChangeLayers;

    public Color[] splashColor;

    //Pour tester les metrics
    public float maxVelocity;
    [Range(0, 7)] public int initialBounces;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        splash.maxVelocity = maxVelocity; ; //Pour tester les metrics

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
                foreach (SpriteRenderer sr in colorChangeLayers)
                {
                    sr.color = splashColor[splash.CurrentBounces];
                }
                
            }
        }
        else if (_collision.collider.CompareTag("Border"))
        {
            if (rb.velocity.normalized.y > 0 && (rb.velocity.normalized.x <= 0.4f || rb.velocity.normalized.x >= -0.4f))
            {
                rb.AddForce(Vector2.up * 1.7f, ForceMode2D.Impulse);
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
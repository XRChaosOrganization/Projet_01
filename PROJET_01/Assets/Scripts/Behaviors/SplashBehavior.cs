using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBehavior : MonoBehaviour
{

    Splash splash;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public Color[] splashColor;

    //Pour tester les metrics
    public float mVelocity;
    [Range(0,7)] public int sBounces;


    private void Awake()
    {
        splash = new Splash();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        //Pour tester les metrics
        splash.baseBounce = sBounces;
        splash.remainingBounces = splash.baseBounce;
        sr.color = splashColor[splash.remainingBounces];

    }


    private void Update()
    {
        splash.maxVelocity = mVelocity; //Pour tester les metrics

        if (rb.velocity.magnitude > splash.maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * splash.maxVelocity;
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (collision.collider.CompareTag("Obstacle"))
        {
            if (splash.remainingBounces <= 0)
                Destroy(this.gameObject);
            else
            {
                splash.remainingBounces--;
                sr.color = splashColor[splash.remainingBounces];
            }
            
        }
    }

    public virtual void AdditionnalCollisionEnter(Collision2D collision)
    {

    }

    void OnTriggerEnter2D(Collider2D trigger)
    {

        if (trigger.CompareTag("Goal"))
        {
            Debug.Log("Scored !");
            Destroy(this.gameObject);
        }

        if (trigger.CompareTag("Killzone"))
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void AdditionnalTriggerEnter(Collider2D _trig = null)
    {

    }









}

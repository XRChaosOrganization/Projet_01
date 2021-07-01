using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash 
{

    public int baseBounce;
    public int baseScore;

    int mBounces;
    public int remainingBounces
    {
        get
        {
            return mBounces;
        }

        set
        {
            if (value >= baseBounce)
                mBounces = baseBounce;
            else
                mBounces = value;
        }
    }



    public float baseRotSpeed;
    public float baseVelocity;

    //Max Velocity != Base Velocity ?
    public float maxVelocity = 8f;

    bool collideWithOtherSplash = false;

    public AudioClip bounceSound;
    public AudioClip deathSound;




}

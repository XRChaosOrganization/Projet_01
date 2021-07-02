using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Splash 
{
    private int baseBounces; //We don't need this to be public if we set it in the prefab 
    public int baseScore;

    private int currentBounces; //instead of remaining bounces, makes more sense ? 
    public int CurrentBounces
    {
        get
        {
            return currentBounces;
        }
        set
        {
            if (value >= baseBounces)
                currentBounces = baseBounces;
            else
                currentBounces = value;
        }
    }

    public float baseRotSpeed;
    public float baseVelocity; //@Alex : base velocity = velocity when splash spawn ? if else, we don't need this 

    //Max Velocity != Base Velocity ? 
    public float maxVelocity = 8f;

    //bool collideWithOtherSplash = false;

    public AudioClip bounceSound;
    public AudioClip deathSound;

    //@Alex use this function to init properties & variables proper to the splash 
    public void InitSplash (int _maxBounces) 
    {
        baseBounces = _maxBounces; //Init inital bounces 
        CurrentBounces = baseBounces; // Init property that handles bounces 
    }
}

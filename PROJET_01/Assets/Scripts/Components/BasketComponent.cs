using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketComponent : MonoBehaviour
{
    public delegate void Scoring(int points);
    public static event Scoring Score;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Splash"))
        {
            if (Score != null)
            {
                Score(_other.GetComponent<SplashBehavior>().splash.baseScore);
            }
            Debug.Log("Scored !");

            Destroy(_other.gameObject);
        }
    }
}
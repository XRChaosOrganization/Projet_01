using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    enum SpreadMode { LINEAR, CUBIC_IN, CUBIC_OUT, SINE_IN, SINE_OUT, CIRCLE_IN, CIRCLE_OUT };
    BoxCollider2D col;
    Splash splash;

    float d;

    [Header("Debug")]
    [SerializeField] [Range(2, 6)] int nbRays;


    [Header("Metrics")]
    [SerializeField] [Range(0, 90)] [Tooltip("en degrés")] int maxAngle;
    [SerializeField] SpreadMode spreadMode;
    [SerializeField] [Range(0, 100)] [Tooltip("en % de la vitesse max d'un splash")] int velocity;


    

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        splash = new Splash();
        
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.localPosition + Vector3.up / 10;
        Vector3 border = transform.lossyScale / 2 - Vector3.up / 10;
        border.z = 0;

        Debug.DrawRay(origin, Vector3.up / 3, Color.red);

        for (int i = 1; i < nbRays + 1; i++)
        {
            float ratio = (i / (float)nbRays);
            Vector3 neg = origin - border * ratio;
            Vector3 pos = origin + border * ratio;


            switch (spreadMode)
            {
                case SpreadMode.LINEAR:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, ratio * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, ratio * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;

                case SpreadMode.CUBIC_IN:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, EaseInCubic(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, EaseInCubic(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;
                case SpreadMode.CUBIC_OUT:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, EaseOutCubic(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, EaseOutCubic(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;

                case SpreadMode.SINE_IN:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, EaseInSine(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, EaseInSine(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;
                case SpreadMode.SINE_OUT:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, EaseOutSine(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, EaseOutSine(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;

                case SpreadMode.CIRCLE_IN:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, EaseInCircle(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, EaseInCircle(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;
                case SpreadMode.CIRCLE_OUT:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, EaseOutCircle(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, EaseOutCircle(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;

                default:
                    break;
            }



        }

    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.CompareTag("Splash"))
        {

            d = (_collision.GetContact(0).point.x - transform.position.x) / (transform.lossyScale.x / 2);
            _collision.rigidbody.velocity = Vector3.zero;
            _collision.rigidbody.angularVelocity = 0;


            switch (spreadMode)
            {
                case SpreadMode.LINEAR:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -d * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -d * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CUBIC_IN:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -EaseInCubic(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -EaseInCubic(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CUBIC_OUT:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -EaseOutCubic(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -EaseOutCubic(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.SINE_IN:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -EaseInSine(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -EaseInSine(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.SINE_OUT:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -EaseOutSine(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -EaseOutSine(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CIRCLE_IN:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -EaseInCircle(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -EaseInCircle(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CIRCLE_OUT:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -EaseOutCircle(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -EaseOutCircle(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                default:
                    break;
            }

            

        }


    }

    float EaseInCubic(float x)
    {
        return Mathf.Pow(x, 3);
    }

    float EaseOutCubic(float x)
    {
        if (x < 0)
            return -(1 - Mathf.Pow(1 - Mathf.Abs(x), 3));
        else return 1 - Mathf.Pow(1 - x, 3);

    }

    float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }

    float EaseOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

    float EaseInCircle(float x)
    {
        if (x < 0)
            return -(1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2)));
        else return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
    }

    float EaseOutCircle(float x)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
    }


}

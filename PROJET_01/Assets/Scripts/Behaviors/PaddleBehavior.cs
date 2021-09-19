using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib_Math;

public class PaddleBehavior : MonoBehaviour
{
    public enum SpreadMode { LINEAR, CUBIC_IN, CUBIC_OUT, SINE_IN, SINE_OUT, CIRCLE_IN, CIRCLE_OUT };
    private BoxCollider2D col;

    private float d;

    [Header("Debug")]
    [SerializeField] [Range(2, 6)] int nbRays;

    [Header("Metrics")]
    [Range(0, 90)] [Tooltip("en degrés")] public int maxAngle;
    public SpreadMode spreadMode;
    [Range(0, 100)] [Tooltip("en % de la vitesse max d'un splash")] public int velocity;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
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
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, Easing.CubicIn(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, Easing.CubicIn(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;
                case SpreadMode.CUBIC_OUT:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, Easing.CubicOut(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, Easing.CubicOut(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;

                case SpreadMode.SINE_IN:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, Easing.SineIn(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, Easing.SineIn(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;
                case SpreadMode.SINE_OUT:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, Easing.SineOut(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, Easing.SineOut(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;

                case SpreadMode.CIRCLE_IN:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, Easing.CircleIn(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, Easing.CircleIn(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
                    break;
                case SpreadMode.CIRCLE_OUT:
                    Debug.DrawRay(neg, Quaternion.Euler(0, 0, Easing.CircleOut(ratio) * maxAngle) * Vector3.up / 3, Color.red);
                    Debug.DrawRay(pos, Quaternion.Euler(0, 0, Easing.CircleOut(ratio) * maxAngle * -1) * Vector3.up / 3, Color.red);
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
            Splash splash = _collision.gameObject.GetComponent<SplashBehavior>().splash;
            d = (_collision.GetContact(0).point.x - transform.position.x) / (transform.lossyScale.x / 2);
            _collision.rigidbody.velocity = Vector3.zero;
            

            switch (spreadMode)
            {
                case SpreadMode.LINEAR:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -d * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -d * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CUBIC_IN:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -Easing.CubicIn(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -Easing.CubicIn(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CUBIC_OUT:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -Easing.CubicOut(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -Easing.CubicOut(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.SINE_IN:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -Easing.SineIn(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -Easing.SineIn(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.SINE_OUT:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -Easing.SineOut(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -Easing.SineOut(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CIRCLE_IN:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -Easing.CircleIn(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -Easing.CircleIn(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                case SpreadMode.CIRCLE_OUT:
                    _collision.rigidbody.AddForce(Quaternion.Euler(0, 0, -Easing.CircleOut(d) * maxAngle) * Vector3.up * velocity / 100 * splash.maxVelocity, ForceMode2D.Impulse);
                    Debug.DrawRay(_collision.GetContact(0).point, Quaternion.Euler(0, 0, -Easing.CircleOut(d) * maxAngle) * Vector3.up * 10, Color.green);
                    break;

                default:
                    break;

                
            }
            _collision.rigidbody.angularVelocity = 0;
            _collision.rigidbody.AddTorque(-10*d);
        }
    }

}

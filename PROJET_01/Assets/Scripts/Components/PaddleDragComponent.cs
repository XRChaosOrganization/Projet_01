using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleDragComponent : MonoBehaviour
{

    Vector2 screenPosition;
    Vector3 worldPosition;

    bool isDragActve = false;

    SpriteRenderer sr;

    float paddleWidth;
    float leftClamp;
    float rightClamp;


    //No Velocity Cap to avoid Paddle lagging behind finger




    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        paddleWidth = sr.gameObject.transform.localScale.x;
        leftClamp = -2.815f + paddleWidth / 2;
        rightClamp = 2.815f - paddleWidth / 2;
    }



    void Update()
    {

        if (isDragActve && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))) // => Quand on relâche le Drag
        {
            Drop();
            return;
        }

        // =======================================================
        //                 Detect Touch on Sceen
        // =======================================================


        if (Input.GetMouseButton(0)) // => Mouse Controls for testing in Unity
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);

        }
        else if (Input.touchCount > 0) // => Touch Controls on Mobile
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else return;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;



        // =======================================================
        //                What to do with the Touch
        // =======================================================



        if (isDragActve)
        {
            Drag();
        }
        else // To detect only when paddle is not already being dragged, otherwise it causes conflicts
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("DragBox"))
                {
                    PickUp();
                }
            }

            
        }

        

    }


    void PickUp()
    {
        isDragActve = true;
    }

    void Drag()
    {
        
        float xClamped = Mathf.Clamp(worldPosition.x, leftClamp, rightClamp);
        this.gameObject.transform.position = new Vector2(xClamped, this.gameObject.transform.position.y);
    }

    void Drop()
    {
        isDragActve = false;
    }
}

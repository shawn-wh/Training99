using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LV_BallBounce_Endless : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed = 3f;
    private float liveTime_Const = 30f; // how long the ball objcet exist
    
    private Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue;

    //*********  Endless  ***********
    public float accelateInterval = 10.0f;  // Tune this
    //*********  Endless  ***********
    
    // To allow bounce after hitting wall
    Rigidbody2D rb;
    // Vector3 ballVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 
    }

    private void OnEnable()
    {
        // Called when the gameobject is loaded.
        // Pull out from the object pool, and start count living time.

        // Decide the living time
        Invoke("DestroyBall", liveTime_Const);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void FixedUpdate()
    {
        //*********  Endless  ***********
        float startSpeed = 2f;
        
        moveSpeed = startSpeed; 
        float accelation = (float)(Math.Floor(Time.timeSinceLevelLoad / accelateInterval));  // accelates every  interval
        moveSpeed += accelation; 

        transform.Translate(moveDirection * Time.fixedDeltaTime * moveSpeed * 0.3f); // previous
        //*********  Endless  ***********

        // Debug.Log("rb.velocity = " + rb.velocity);
    }

    public void SetMoveDirection(Vector2 _direction)
    {
        moveDirection = _direction;
    }

    private void DestroyBall()
    {
        // Put into the object pool
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // gameObject.SetActive(false);
        // ProcessCollision(collision.gameObject);
        
        if (other.gameObject.CompareTag("Player") ||  
            other.gameObject.CompareTag("BulletRecycler"))
        {
            gameObject.SetActive(false);
        }
    }


    private void onCollisionEnter2D(Collision2D other)
    {
        Debug.Log("BallBounce onCollisionEnter2D"); 
    }

    

}

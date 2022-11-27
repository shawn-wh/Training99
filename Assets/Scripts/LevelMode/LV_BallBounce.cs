using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_BallBounce : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed = 3f;
    private float liveTime_Const = 15f; // how long the ball objcet exist
    
    private Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue;

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
        transform.Translate(moveDirection * Time.fixedDeltaTime * moveSpeed * 0.3f);
        // ballVelocity = rb.velocity;

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
        Debug.Log("onCollisionEnter2D outside"); 
        // Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        // Vector2 inNormal = other.contacts[0].normal;
        // Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);        

        // // Reflective when hitting walls
        // if (other.gameObject.CompareTag("Walls") || 
        //     other.gameObject.CompareTag("CrossWall"))
        // {
        //     Vector2 wallNormal = other.contacts[0].normal;
        //     // set new move direction
        //     moveDirection = Vector2.Reflect() 

        //     // var ballSpeed = ballVelocity.magnitude;
        //     // var ballDirection = Vector3.Reflect(ballVelocity.normalized, other.contacts[0].normal);
        //     // rb.velocity = ballDirection * Mathf.Max(ballSpeed, 0f); 
        //     Debug.Log("rb.velocity = " + rb.velocity);

        // }

    }

    

}

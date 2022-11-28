using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LV_Bullet_Endless : MonoBehaviour
{
    private GameObject targetPlayer = null;

    public float bulletSpeed = 1f;     // adjustable bullet speed
    public float liveTime_Const = 15f;    // adjustable bullet live time
    private float liveTime;
    private Color _color;
    private SpriteRenderer spriteRenderer;
    
            
    //*********  Endless  ***********
    // [Endless] player change rate
    private float[] speedSetup = {1, 1.5f, 2, 2.5f, 3, 3.5f, 4, 4.5f, 5, 5.5f, 6, 6.5f, 7, 7.5f, 8};
    public float accelateInterval = 6.0f;  // Tune this
    //*********  Endless  ***********


    // Start is called before the first frame update
    void Start()
    {
        liveTime = liveTime_Const;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Find bullet target
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        
        // https://www.youtube.com/watch?v=mJi0NwSsJig
        // This video explains why LookAt won't work
        // transform.LookAt(targetPlayer.transform);
        
        Vector2 direction = targetPlayer.transform.position - gameObject.transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        
    }

    private void OnEnable()
    {
        liveTime = liveTime_Const;
    }


    // Update is called once per frame
    // FixedUpdate is called once every 0.02 seconds
    void FixedUpdate()    
    {
        //*********  Endless  ***********
        float startSpeed = 1f;
        
        bulletSpeed = startSpeed; 
        float accelation = (float)(Math.Floor(Time.timeSinceLevelLoad / accelateInterval));  // accelates every  interval
        bulletSpeed += accelation; 

        // if (Time.timeSinceLevelLoad >= 10.0) {
        //     bulletSpeed = 16;
        // }else {
        //     bulletSpeed = speedSetup[Convert.ToInt32(Math.Floor(Time.timeSinceLevelLoad / 20.0))];
        // }

        // transform.Translate(Vector3.up * Time.fixedDeltaTime * bulletSpeed * 0.3f); // previous 
        transform.Translate(Vector3.up * Time.fixedDeltaTime * bulletSpeed); // Endless 
        //*********  Endless  ***********

        liveTime -= Time.fixedDeltaTime;
        if (liveTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetColor(Color newColor)
    {
        _color = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // gameObject.SetActive(false);
        ProcessCollision(collision.gameObject);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject collider)
    {
        if (collider.CompareTag("Player") || 
            collider.CompareTag("BulletRecycler"))
        {
            gameObject.SetActive(false);
        }
    }
}

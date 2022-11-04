using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LV_Bullet : MonoBehaviour
{
    private GameObject targetPlayer = null;

    public float bulletSpeed = 1f;     // adjustable bullet speed
    public float liveTime_Const = 15f;    // adjustable bullet live time
    private float liveTime;
    private Color _color;
    private SpriteRenderer spriteRenderer;
    
    

    // Start is called before the first frame update
    void Start()
    {
        liveTime = liveTime_Const;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Find bullet target
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
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
        transform.Translate(Vector3.up * Time.fixedDeltaTime * bulletSpeed * 0.3f);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LV_Bullet : MonoBehaviour
{
    private GameObject targetPlayer = null;

    public float bulletSpeed = 1f;     // adjustable bullet speed
    public float liveTime = 15f;    // adjustable bullet live time
    private bool _hitted = false;
    private float _hittedTime;
    private Color _color;
    private SpriteRenderer spriteRenderer;
    
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
        
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
        _hitted = false;
        liveTime = 15f;
    }


    // Update is called once per frame
    void Update()
    {
        if (_hitted)
        {
            Color newColor = new Color(_color.r, _color.g, _color.b, 0.5f);
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            transform.Translate(Vector3.down * Time.deltaTime * bulletSpeed * 1);
            // Destroy after 0.5 seconds of bullet gets hitted
            if (Time.time - _hittedTime >= 0.5f)
            {
                Debug.Log("gameObject get destroyed");
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
        else
        {
            transform.Translate(Vector3.up * Time.fixedDeltaTime * bulletSpeed * 0.3f);
        }

        liveTime -= Time.deltaTime;
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
        Color playerColor = collision.GetComponent<SpriteRenderer>().color;
        if (playerColor == _color)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            _hitted = true;
            _hittedTime = Time.time;
        }
    }
}

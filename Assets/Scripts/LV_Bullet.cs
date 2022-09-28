using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LV_Bullet : MonoBehaviour
{
    private GameObject targetPlayer = null;

    public float bulletSpeed = 1f;     // adjustable bullet speed
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

        transform.LookAt(targetPlayer.transform);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (_hitted)
        {
            Color newColor = new Color(_color.r, _color.g, _color.b, 0.5f);
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            transform.Translate(Vector3.back * Time.deltaTime * bulletSpeed * 3);
            // Destroy after 1 seconds of bullet gets hitted
            if (Time.time - _hittedTime >= 1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * bulletSpeed);
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
            Destroy(gameObject);
        }
        else
        {
            _hitted = true;
            _hittedTime = Time.time;
        }
    }
}

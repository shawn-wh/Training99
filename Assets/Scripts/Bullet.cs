using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject player = null;
    
    public float bulletSpeed = 0.7f;     // adjustable bullet speed
    private bool _hitted = false;
    private float _hittedTime;
    private Color _color;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(player.transform);
        //this.gameObject.transform.GetChild(0).GetComponent<Image>().color = colors[Random.Range(0, colors.Length)];
        //colors[0] = new Color(47, 55, 91);
        //colors[1] = new Color(162, 52, 25);
        //colors[2] = new Color(248, 177, 21);
        //this.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
        //Debug.Log("bullet color in bullet: " + this.GetComponentInChildren<Renderer>().material.color);
    }


    // Update is called once per frame
    void Update()
    {
        if (_hitted) 
        {
            Color newColor = new Color(_color.r, _color.g, _color.b, 0.5f);
            gameObject.transform.GetChild(0).GetComponent<Image>().color = newColor;
            transform.Translate(Vector3.back * Time.deltaTime * bulletSpeed * 3);
            // Destroy after 1 seconds of bullet gets hitted
            if (Time.time - _hittedTime >= 1f) 
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        }
    }
    
    public void SetColor(Color newColor)
    {
        _color = newColor;
        gameObject.transform.GetChild(0).GetComponent<Image>().color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color playerColor = collision.GetComponent<Image>().color;
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

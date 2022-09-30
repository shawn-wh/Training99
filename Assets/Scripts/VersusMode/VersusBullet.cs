using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VersusBullet : MonoBehaviour
{
    public GameObject player = null;

    public float bulletSpeed = 0.7f;     // adjustable bullet speed default 0.7f
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(player.transform);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        gameObject.transform.GetChild(0).GetComponent<Image>().color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

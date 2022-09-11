using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public GameObject player = null;
    public Color[] colors = new Color[3];
    


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
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

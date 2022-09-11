using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CreateArea[] createAreas = null;
    public Bullet clone = null;
    public GameObject bulletNode = null;

    public Color[] colors = new Color[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < createAreas.Length; i++)
        {
            CreateArea ca = createAreas[i];
            if (ca.CheckTime())
            {
                Vector3 pos = ca.GetRandomPos();
                // This function makes a copy of an object in a similar way to the Duplicate command in the editor.
                // Instantiate(clone, createArea.transform.position, Quaternion.identity, canvas.transform);
                Bullet bullet = Instantiate(clone, pos, Quaternion.identity, bulletNode.transform);
                //Debug.Log("Child color: " + bullet.GetComponentInChildren<Renderer>().material.color);
                //bullet.GetComponentInChildren<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
                bullet.transform.GetChild(0).GetComponent<Image>().color = colors[Random.Range(0, colors.Length)];
                ca.NextTime();
            }
        }
        
        
    }
}

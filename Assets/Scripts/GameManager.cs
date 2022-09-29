using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // global variable
    public static float endlessTimeScore = 0f;
    public const string url = "https://www.dreamlo.com/lb/";
    public const string privateCode = "pvRmq2uOCkS2mOf4UkgrrQ4H6IwWeH5E6IIdSedi4CCg";
    public const string publicCode = "6330fb5a8f40bc0fe885f629";

    public CreateArea[] createAreas = null;
    public Bullet clone = null;
    public GameObject bulletNode = null;

    public Color[] colors = new Color[3];
    public Sprite[] sprites = new Sprite[3];

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
                bullet.SetColor(colors[Random.Range(0, colors.Length)]);
                bullet.transform.GetChild(0).GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
                ca.NextTime();
            }
        }
        
        
    }
}

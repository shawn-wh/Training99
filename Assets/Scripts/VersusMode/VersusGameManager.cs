using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersusGameManager : MonoBehaviour
{
    public CreateArea[] createAreas = null;
    public VersusBullet clone = null;
    public GameObject bulletNode = null;
    public Sprite sprite = null;
    public Color[] colors = new Color[2];

    public CardController[] availableCards;

    public VersusPlayer player1;
    public VersusPlayer player2;
    public PropSelectPanelController propPanel1;
    public PropSelectPanelController propPanel2;
    
    public static string winner = null;
    public static bool isPaused = true;
    
    void Start()
    {
        colors[0] = new Color32(153, 0, 0, 255);
        colors[1] = new Color32(39, 116, 174, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            propPanel1.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            propPanel2.gameObject.SetActive(true);
        }
        
        for (int i = 0; i < createAreas.Length; i++)
        {
            CreateArea ca = createAreas[i];
            if (ca.CheckTime())
            {
                Vector3 pos = ca.GetRandomPos();
                // This function makes a copy of an object in a similar way to the Duplicate command in the editor.
                VersusBullet bullet = Instantiate(clone, pos, Quaternion.identity, bulletNode.transform);
                bullet.SetColor(colors[Random.Range(0, colors.Length)]);
                bullet.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                ca.NextTime();
            }
        }
    }
}

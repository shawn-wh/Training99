using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_GameManager : MonoBehaviour
{
    // global variable
    public static float endlessTimeScore = 0f;
    public const string url = "http://dreamlo.com/lb/";
    public const string privateCode = "pvRmq2uOCkS2mOf4UkgrrQ4H6IwWeH5E6IIdSedi4CCg";
    public const string publicCode = "6330fb5a8f40bc0fe885f629";

    public LV_BulletGenerator[] bulletGenerator = null;

    public Color[] colors = new Color[3];
    public Sprite[] sprites = new Sprite[3];
    
    // Whether show dialogue box or not
    [SerializeField] private bool showingDialogueBox = true;
    
    // Load the dialogue box 
    public GameObject dialogueBox;
    
    public static bool IsGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Load dialogue box
        if (showingDialogueBox)
        {
            dialogueBox.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When showing dialogueBox, game should not generate bullets.
        if (!dialogueBox.activeSelf)
        {
            bulletGenerating();
        }

    }

    void bulletGenerating() 
    {
        for (int i = 0; i < bulletGenerator.Length; i++)
        {
            LV_BulletGenerator bg = bulletGenerator[i];
            if (bg.CheckTime())
            {
                Vector3 pos = bg.GetRandomPos();

                //GameObject bullet = LV_BulletGenerator.bulletsPoolInstance.GetPoolObj();
                GameObject bullet = bg.GetPoolObj();
                if (bullet != null)
                {
                    bullet.SetActive(true);
                    bullet.transform.position = pos;
                    bullet.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                    bullet.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                }

                bg.NextTime();
            }
        }
    }
}

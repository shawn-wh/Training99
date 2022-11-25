using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LV_ActiveSkillsTutorial: MonoBehaviour
{
    public GameObject[] tutorialText;
    private int textIndex;
    public GameObject player;
    public GameObject square;
    public GameObject triangle;
    public GameObject circle;
    public float waitTime = 2f;

    public LV_BulletGenerator[] bulletGenerator = null;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // show the tutorial text one by one
        for (int i = 0; i < tutorialText.Length; i++) {
            if (i == textIndex) {
                tutorialText[i].SetActive(true);
            } else {
                tutorialText[i].SetActive(false);
            }
        }

        // which tutorial steps are doing currently
        if (textIndex == 0) {
            if (player.GetComponent<SpriteRenderer>().sprite == Resources.Load<Sprite>("Sprites/Triangle")) {
                textIndex++;
            }
        } else if (textIndex == 1) {
            if (Input.GetKeyDown(KeyCode.E)) {
                textIndex++;
            }
        } else if (textIndex == 2) {
            if (player.GetComponent<SpriteRenderer>().sprite == Resources.Load<Sprite>("Sprites/Circle")) {
                textIndex++;
            }
        } else if (textIndex == 3) {
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
                        //bullet.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                        //bullet.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                    }

                    bg.NextTime();
                }
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                textIndex++;
            }
        } else if (textIndex == 4) {
            if (player.GetComponent<SpriteRenderer>().sprite == Resources.Load<Sprite>("Sprites/Square")) {
                textIndex++;
            }
        } else if (textIndex == 5) {
            if (Input.GetKeyDown(KeyCode.E)) {
                textIndex++;
            }
        } else if (textIndex == 6) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                textIndex++;
                SceneManager.LoadScene("LevelCleared");
            }
        }
    }
}
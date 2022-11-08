using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LV_TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialText;
    private int textIndex;
    public GameObject square;
    public GameObject triangle;
    public GameObject circle;
    public GameObject key;
    public GameObject door;
    public GameObject changeColor;
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) {
                textIndex++;
            }
        } else if (textIndex == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                textIndex++;
            }
        } else if (textIndex == 2) {
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
            if (key.activeSelf) {
                textIndex++;
            }
        } else if (textIndex == 3) {
            if (door.activeSelf) {
                textIndex++;
            }
        }
    }
}
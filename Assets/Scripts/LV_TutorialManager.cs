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
    public float waitTime = 2f;
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
                textIndex++;
            }
        } else if (textIndex == 1) {
            if (waitTime <= 0) {
                circle.SetActive(true);
                circle.transform.position = new Vector3((float)2.82, (float)-7.57, 0);
                circle.GetComponent<SpriteRenderer>().color = new Color32(244, 187, 15, 255);
                square.SetActive(true);
                square.transform.position = new Vector3((float)0.52, (float)7.42, 0);
                square.GetComponent<SpriteRenderer>().color = new Color32(244, 187, 15, 255);
                triangle.SetActive(true);
                triangle.transform.position = new Vector3((float)-8.02, (float)-7.2, 0);
                triangle.GetComponent<SpriteRenderer>().color = new Color32(244, 187, 15, 255);
                waitTime = 10f;
            } else {
                waitTime -= Time.deltaTime;
            }
            if (key.activeSelf) {
                textIndex++;
            }
        } else if (textIndex == 2) {
            if (door.activeSelf) {
                textIndex++;
            }
        }
    }
}

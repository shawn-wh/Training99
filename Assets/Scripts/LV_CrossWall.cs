using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_CrossWall : MonoBehaviour
{
    // Wall color before & after
    //public Color originalColor = new Color32(0,24,100,255);
    //public Color newColor = new Color32(0, 24, 100, 100);
    public Color[] wallColors;

    
    [Header("To PassThroughWallButton")]
    public float timer = 5;
    private float timeRemaining = 5;
    // private bool timerIsRunning = false;
    private bool isUsingSkill = false;
    
    public LV_ActiveSkill1 skill1;
    // public CrossPowerBar crossPowerBar;
    // public TextMeshProUGUI timerText = null;

    private GameObject[] crossWalls;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crossWalls = GameObject.FindGameObjectsWithTag("CrossWall");
        foreach (GameObject crossWall in crossWalls)
        {
            crossWall.GetComponent<SpriteRenderer>().color = wallColors[Random.Range(0, wallColors.Length)];
        }
        Debug.Log("walls: " + crossWalls.Length);

    }

    // Update is called once per frame
    void Update()
    {
        // Original with crossPowerBar 
        // if (!timerIsRunning && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && crossPowerBar.GetPowerAmount() > 0)

        // New with PassThroughWallButton 
        if (!isUsingSkill 
            && (skill1.GetPowerAmount() > 0) 
            && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)))
        {
            Debug.Log("1 key was pressed.");
            isUsingSkill = true;
        }

        if (isUsingSkill)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                // DisplayTime(timeRemaining);
                EnableCrossWalls();
            }
            else
            {
                isUsingSkill = false;
                timeRemaining = timer;
                // DisplayTime(0);
                DisableCrossWalls();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        // timerText.text = string.Format("Cross Wall Remaining: {0:00}:{1:00}", seconds, milliSeconds);
    }

    void EnableCrossWalls()
    {
        foreach(GameObject crossWall in crossWalls)
        {
            Debug.Log("Player: " + player.GetComponent<SpriteRenderer>().color);
            

            if (isSameColor(player, crossWall))
            {
                //Debug.Log("can wall: " + crossWall.GetComponent<SpriteRenderer>().color);
                crossWall.GetComponent<Collider2D>().enabled = false;
                Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.42f;
                crossWall.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                //Debug.Log("cannot wall: " + crossWall.GetComponent<SpriteRenderer>().color);
                crossWall.GetComponent<Collider2D>().enabled = true;
                Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                crossWall.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }

    void DisableCrossWalls()
    {
        foreach (GameObject crossWall in crossWalls)
        {
            crossWall.GetComponent<Collider2D>().enabled = true;
            //crossWall.GetComponent<SpriteRenderer>().color = originalColor;
            Color tmp = crossWall.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            crossWall.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    bool isSameColor(GameObject a, GameObject b)
    {
        return (a.GetComponent<SpriteRenderer>().color.r == b.GetComponent<SpriteRenderer>().color.r)
            && (a.GetComponent<SpriteRenderer>().color.g == b.GetComponent<SpriteRenderer>().color.g)
            && (a.GetComponent<SpriteRenderer>().color.b == b.GetComponent<SpriteRenderer>().color.b);
    }

}
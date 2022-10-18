using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_CrossWall : MonoBehaviour
{
    // Wall color before & after
    public Color originalColor = new Color32(0,24,100,255);
    public Color newColor = new Color32(0, 24, 100, 100);

    
    [Header("To PassThroughWallButton")]
    public float timer = 5;
    private float timeRemaining = 5;
    // private bool timerIsRunning = false;
    private bool isUsingSkill = false;
    
    public LV_ActiveSkill1 skill1;
    // public CrossPowerBar crossPowerBar;
    // public TextMeshProUGUI timerText = null;

    private GameObject[] crossWalls;

    // Start is called before the first frame update
    void Start()
    {
        crossWalls = GameObject.FindGameObjectsWithTag("CrossWall");
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
            crossWall.GetComponent<Collider2D>().enabled = false;
            crossWall.GetComponent<SpriteRenderer>().color = newColor;
        }
    }

    void DisableCrossWalls()
    {
        foreach (GameObject crossWall in crossWalls)
        {
            crossWall.GetComponent<Collider2D>().enabled = true;
            crossWall.GetComponent<SpriteRenderer>().color = originalColor;
        }
    }

}
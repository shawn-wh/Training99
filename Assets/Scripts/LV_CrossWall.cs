using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_CrossWall : MonoBehaviour
{
    public float timer = 5;
    private float timeRemaining = 5;
    private bool timerIsRunning = false;

    public Color originalColor = new Color(58,58,58,255);
    public Color newColor = new Color(58, 58, 58, 100);

    public CrossPowerBar crossPowerBar;

    public TextMeshProUGUI timerText = null;
    public GameObject[] crossWalls;

    // Start is called before the first frame update
    void Start()
    {
        crossWalls = GameObject.FindGameObjectsWithTag("CrossWall");
        Debug.Log("walls: " + crossWalls.Length);

    }

    // Update is called once per frame
    void Update()
    {
        if (!timerIsRunning && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && crossPowerBar.GetPowerAmount() > 0)
        {
            Debug.Log("1 key was pressed.");
            timerIsRunning = true;
            crossPowerBar.UpdatePowerAmount(-1);
        }

        if (timerIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                EnableCrossWalls();
            }
            else
            {
                timerIsRunning = false;
                timeRemaining = timer;
                DisplayTime(0);
                DisableCrossWalls();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timerText.text = string.Format("Cross Wall Remaining: {0:00}:{1:00}", seconds, milliSeconds);
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
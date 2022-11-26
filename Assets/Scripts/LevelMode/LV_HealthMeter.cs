using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_HealthMeter : MonoBehaviour
{
    public Image damageRatio;  
    public TextMeshProUGUI healthText;
    
    private float maxHP;
    private float currentHP;

    private GameObject player = null;
    private Sprite[] shapes = new Sprite[3];
    private Sprite playerShape;

    // Start is called before the first frame update
    void Start()
    {
        // Get player gameObject
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        SetShapes();
        DetectPlayerShape();              
    }

    // Update is called once per frame
    void Update()
    {
        // Need to check player's shape
        DetectPlayerShape();      
    }

    private void SetShapes()
    {
        shapes[0] = Resources.Load<Sprite>("Sprites/Circle");         // Must exist in "Resources" folder
        shapes[1] = Resources.Load<Sprite>("Sprites/Triangle");     // Must exist in "Resources" folder
        shapes[2] = Resources.Load<Sprite>("Sprites/Square");         // Must exist in "Resources" folder
        // Debug.Log("Length of playerShapes = " + playerShapes.Length);
    }

    // Detect player's shape and change the mask to the matching sprite
    void DetectPlayerShape()
    {
        playerShape = player.GetComponent<SpriteRenderer>().sprite;
        if (playerShape.name == "Circle")
        {
            damageRatio.GetComponent<Image>().sprite = shapes[0];
        }
        else if (playerShape.name == "Triangle")
        {
            damageRatio.GetComponent<Image>().sprite = shapes[1];
        }
        else if (playerShape.name == "Square")
        {
            damageRatio.GetComponent<Image>().sprite = shapes[2];
        }
        else
        {
            Debug.Log("Error! Cannot find the corresponding shape.");
        }
    }

    public void SetMaxHealth(int maxValue)
    {
        maxHP = (float)maxValue;
    }

    public void SetHealth(int currentValue)
    {
        currentHP = (float)currentValue;

        // Use float to avoid "int/int = int"
        if (currentHP == maxHP) 
        {
            healthText.text = "";
        }
        else
        {
            healthText.text = (currentHP / maxHP).ToString("P0");   // Show percentage (0 decimal point)
        }

        // damage ration = fill amount
        damageRatio.fillAmount =  (maxHP - currentHP) / maxHP;
        // Debug.Log("damageRatio = " + damageRatio.fillAmount);  
    }

    public void SetActive(bool displayStatus) 
    {
        if (displayStatus == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }   
}

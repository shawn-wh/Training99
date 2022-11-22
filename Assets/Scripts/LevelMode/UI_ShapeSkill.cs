using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ShapeSkill : MonoBehaviour
{
    // -----------------------------------------------------------------------------
    // Skill0
    [Header("Skill1 Change Shape")]
    [SerializeField] private GameObject button;
    [SerializeField] private Image skill_mask;
    [SerializeField] private TextMeshProUGUI skill_text;

    private bool enableShapeChanging = false;

    // Count the cooldown time
    private bool isCooldown = false;
    public float cooldownTimeLimit = 0.0f;
    private float timer = 0f;


    // Get next color
    private GameObject player = null;
    // private Color playerColor;
    // private Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue
    
    [SerializeField] private Sprite[] skillSprite = new Sprite[3];

    private float[] skillsCooldownSetup = new float[3];

    private void SetSkillSprite()
    {
        skillSprite[0] = Resources.Load<Sprite>("Sprites/ShapeSkill_bomb");         // Must exist in "Resources" folder
        skillSprite[1] = Resources.Load<Sprite>("Sprites/ShapeSkill_dash");     // Must exist in "Resources" folder
        skillSprite[2] = Resources.Load<Sprite>("Sprites/ShapeSkill_PaintBrush");         // Must exist in "Resources" folder
        // Debug.Log("Length of skillSprite = " + skillSprite.Length);
    }

    void GetIcon()
    {
        // Check current player's shape
        Sprite currentShape = player.GetComponent<SpriteRenderer>().sprite;

        // Player use "sprite"
        // UI use "Image"
        if (currentShape.name == "Circle")
        {
            Debug.Log("Read player's shape = " + currentShape.name);
            gameObject.GetComponent<Image>().sprite = skillSprite[0];
            cooldownTimeLimit = skillsCooldownSetup[0]; 
        }
        else if (currentShape.name == "Triangle")
        {
            Debug.Log("Read player's shape = " + currentShape.name);
            gameObject.GetComponent<Image>().sprite = skillSprite[1];
            cooldownTimeLimit = skillsCooldownSetup[1]; 
        }
        else if (currentShape.name == "Square")
        {
            Debug.Log("Read player's shape = " + currentShape.name);
            gameObject.GetComponent<Image>().sprite = skillSprite[2];
            cooldownTimeLimit = skillsCooldownSetup[2]; 
        }
        else
        {
            Debug.Log("Error! Cannot find the corresponding shape.");
        }
        // Debug.Log("nextColor = " + nextColor);
    }

    void SetDisplay()
    {
        //
        enableShapeChanging = player.GetComponent<LV_PlayerMovement>().GetEnableShapeChanging();
        if (enableShapeChanging == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void GetCooldownSetup()
    {
        skillsCooldownSetup = player.GetComponent<LV_PlayerMovement>().GetShapeSkillsCooldown();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Get player gameObject
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        SetDisplay();
        SetSkillSprite(); 
        GetIcon();
        
        // Player can use skill, mask = 0
        skill_mask.fillAmount = 0; 

        // Get cooldown time
        // s0CooldownTiime = player.GetComponent<LV_PlayerMovement>().Get();
        GetCooldownSetup();
    }

    // Update is called once per frame
    void Update()
    {
        Skill_Use();
    }

    void Skill_Use()
    {
        // Chech whether color-changing ability is enable/disable
        enableShapeChanging = player.GetComponent<LV_PlayerMovement>().GetEnableShapeChanging();
        // Debug.Log("enableShapeChanging =" + enableShapeChanging);
        if (enableShapeChanging == false)
        {
            // when disable, no need to change UI button
            return;
        }
        
        GetIcon();
        
        // Cooldown 
        if (Input.GetKeyDown(KeyCode.E) && isCooldown == false)
        {
            // Skill used, need a cooldown 
            isCooldown = true;
            skill_mask.fillAmount = 1;
            skill_text.text = cooldownTimeLimit.ToString();
            timer = cooldownTimeLimit;   // Reset Timer 
        }

        if (isCooldown)
        {
            // start to count down
            timer -= Time.deltaTime;

            skill_mask.fillAmount -= Time.deltaTime / cooldownTimeLimit;

            skill_text.text = timer.ToString("F1");  // show 1 Decimal Point 
            // skill_text.text = Mathf.RoundToInt(timer).ToString(); // show integer only

            if ( skill_mask.fillAmount <= 0)
            {
                // Can use skill 0 again
                skill_mask.fillAmount = 0;
                skill_text.text = " ";
                isCooldown = false; 
                timer = cooldownTimeLimit;   // Reset Timer 
            } 
        }

    }

    public void SetCooldownTime(float value)
    {
        cooldownTimeLimit = value; 
    }
}


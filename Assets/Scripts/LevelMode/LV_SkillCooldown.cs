using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_SkillCooldown : MonoBehaviour
{

    // input game objects
    [SerializeField]
    private Image cooldownMask;

    [SerializeField]
    private TMP_Text cooldownText;    

    // Count the cooldown time
    private bool isCooldown = false;
    public float cooldownTimeSetup = 5f;
    private float cooldownTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Player can use skill when game starts, isCooldown = false
        cooldownText.gameObject.SetActive(false);
        cooldownMask.fillAmount = 0.0f;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isCooldown == false)
        {
            UseSkill(); 
        }

        // if (isCooldown)
        // {
        //     CountCooldown();
        // }
    }

    public void UseSkill() 
    {
        // Cooldown state => cannot use skill 
        if (isCooldown)
        {
            // return false;
        }
        // After using skill, isCooldown = true use
        else
        {
            isCooldown = true;      // ?
            cooldownText.gameObject.SetActive(true);
            cooldownTimer = cooldownTimeSetup;
            // return true; 
        }
    }

    void CountCooldown() 
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0.0f)
        {
            // Reach the end, can use skill again
            isCooldown = false;
            cooldownText.gameObject.SetActive(false);
            cooldownMask.fillAmount = 0.0f;     // 0 = no mask
 
        }
        else
        {
            // Count down
            cooldownText.text = Mathf.RoundToInt(cooldownTimer).ToString(); // Text shows the coundown time.
            cooldownMask.fillAmount = cooldownTimer / cooldownTimeSetup; 
        }
    }
}

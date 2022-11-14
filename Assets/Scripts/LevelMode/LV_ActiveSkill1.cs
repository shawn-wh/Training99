using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_ActiveSkill1 : MonoBehaviour
{


    // private GameObject player = null;

    // -----------------------------------------------------------------------------
    // Skill1
    [Header("Skill1 Pass wall")]
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private TextMeshProUGUI powerAmountText;

    // Number of power-up that can be applied
    private int powerAmount = 0;

    public float remainingTimeSetup = 5.0f;
    private bool isUsingSkill = false;   // whether player is using the skill
    private float remainingTime = 0f;

    // Counter
    [SerializeField] private int collectedTarget = 5;
    private int collectedCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitSetup();
    }

    void InitSetup()
    {
        mask.fillAmount = 1;    // cannot use the skill until reaching collected target        
        collectedCount = 0;
        remainingTime = remainingTimeSetup;     
    }

    // Update is called once per frame
    void Update()
    {
        Skill1();
    }

    // Skill1: pass wall
    void Skill1()
    {
        
        // Player press skill1 button
        // Valid when powerAmount >= 1 && skill is not using now
        if (!isUsingSkill 
            && (powerAmount > 0)
            && (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)))
        {
            // Skill1 is used, 
            isUsingSkill = true;
            Debug.Log("usingSkill: powerAmount = " + powerAmount);
        }

        // Player is using the skill, countdown the remaining time.
        if (isUsingSkill)
        {
            if (remainingTime >= 0)
            {
                // Skill in use, countdown time
                remainingTime -= Time.deltaTime;

                remainingTimeText.text = remainingTime.ToString("F1");    // Show 1 Decimal Point 
                // remainingTimeText.text = Mathf.RoundToInt(remainingTime).ToString();    // display on UI

            }
            else
            {
                // Reset status
                UpdatePowerAmount(-1);      // reduce only when the skill is completed. 
                isUsingSkill = false;
                remainingTime = remainingTimeSetup;
                remainingTimeText.text = " ";
            }

        }
    }

    public void ResourcesCounter(int val)
    {
        collectedCount += val;
        if (collectedCount == collectedTarget)
        {
            collectedCount = 0;
            UpdatePowerAmount(1);
            Debug.Log("collected all: powerAmount = " + powerAmount);
        }
        Debug.Log("collectedCount " + collectedCount);
        updateCollectedNum();   

    }

    void updateCollectedNum()
    {
        mask.fillAmount = (collectedTarget - collectedCount) * 1.0f / collectedTarget;
        Debug.Log("mask.fillAmount " + mask.fillAmount);
    }
 

    public int GetPowerAmount()
    {
        return powerAmount;
    }

    public void UpdatePowerAmount(int val)
    {
        // Avoid error.
        if (powerAmount == 0 && val < 0)
            return;

        powerAmount += val;
        powerAmountText.text = "x " + powerAmount.ToString();

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossPowerBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI powerAmountText;

    private int powerAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitValue(0f);
        powerAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitValue(float val)
    {
        slider.value = val;
    }

    public void UpdateValue(float val)
    {
        slider.value += val;

        if (slider.value == slider.maxValue)
        {
            InitValue(0f);
            UpdatePowerAmount(1);
        }

        powerText.text = "Blue: " + slider.value.ToString();
    }

    public int GetPowerAmount()
    {
        return powerAmount;
    }

    public void UpdatePowerAmount(int val)
    {
        powerAmount += val;
        powerAmountText.text = powerAmount.ToString();
    }
}

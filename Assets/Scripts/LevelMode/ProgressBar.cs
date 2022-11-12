using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{

    public Slider slider;
    //public TextMeshProUGUI statusText;

    //private string STATUS_1 = "Complete the Task";
    //private string STATUS_2 = "Find the Key";
    //private string STATUS_3 = "Go to Door";

    // Start is called before the first frame update
    void Start()
    {
        InitValue(0f);
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
        slider.value = val;

        //if (slider.value == 1)
        //{
        //    statusText.text = STATUS_1;
        //}
        //else if(slider.value == 2)
        //{
        //    statusText.text = STATUS_2;

        //}
        //else if (slider.value == slider.maxValue)
        //{
        //    statusText.text = STATUS_3;
        //}

        
    }
}

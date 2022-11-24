using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    // public float displayTime = 4f;
    private TMP_Text speechText;

    private void Awake()
    {
        speechText = GetComponentInChildren<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeechText(string content)
    {
        speechText.text = "" + content.ToString();
    }

    public void ChangeEmoji(Sprite emotion)
    {
        // Link to the position of "PatrolFace" in Hierarchy
        // Image[] childrenImages = gameObject.GetComponentInChildren<Image>();
        // childrenImages.Last().sprite = emotion; 
        gameObject.GetComponentInChildren<Image>().sprite = emotion; 
        // Debug.Log("childrenImages  = " + childrenImages);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LV_DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    [TextArea(3,10)]
    public string[] speakingLines;
    public float typingSpeed = 10f;

    private int index = 0;
    private bool isLineFinished;    // isLineFinished=true, after finishing Typing1Line()
    public static bool isGamePaused = false; 

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.anyKey && isLineFinished)
        {
            if (dialogueText.text == speakingLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = speakingLines[index];
            }
        }
        
    }

    void StartDialogue()
    {
        index = 0; 
        StartCoroutine(Typing1Line());
    }

    IEnumerator Typing1Line()
    {
        isLineFinished = false;
        foreach (char letter in speakingLines[index].ToCharArray())
        {
            dialogueText.text += letter;
        }
        yield return new WaitForSeconds(1f/typingSpeed);
        isLineFinished = true;
    }

    void NextLine()
    {
        // Show next dialog line
        if (index < speakingLines.Length -1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(Typing1Line());
        }
        // When finish dialogue
        else
        {
            gameObject.SetActive(false);
        }
    }
}

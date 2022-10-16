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

    private float typingSpeed = 80f;
    private int index = 0;
    private bool isLineFinished;    // isLineFinished=true, after finishing Typing1Line()
    public static bool isGamePaused = false; 

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.anyKeyDown)
        {
            if (dialogueText.text == speakingLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                // Immediate show the whole line
                dialogueText.text =  speakingLines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0; 
        StartCoroutine(Typing1Line(speakingLines[index]));
    }

    // Print each line
    IEnumerator Typing1Line(string line)
    {
        dialogueText.text = string.Empty; // Empty the text area.
        isLineFinished = false;
        foreach (char letter in line.ToCharArray())
        {
            // Typing effect
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/typingSpeed);
        }
        isLineFinished = true;
    }

    void NextLine()
    {
        // Show next dialog line
        if (index < speakingLines.Length -1)
        {
            index++;
            StartCoroutine(Typing1Line(speakingLines[index]));
        }
        // When finish dialogue
        else
        {
            gameObject.SetActive(false);
        }
    }
}

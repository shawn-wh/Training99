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
    public static bool isGamePaused = false;

    [SerializeField] public Image tutorialDiagram;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialDiagram != null)
        {
            tutorialDiagram.enabled = false;
        }
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.anyKeyDown)
        {
            if (index == speakingLines.Length)
            {
                if (tutorialDiagram != null)
                {
                    tutorialDiagram.enabled = false;
                }
                gameObject.SetActive(false);
            }
            else
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
        foreach (char letter in line.ToCharArray())
        {
            // Typing effect
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/typingSpeed);
        }
    }

    void NextLine()
    {
        // Show next dialog line
        if (index < speakingLines.Length -1)
        {
            index++;
            StartCoroutine(Typing1Line(speakingLines[index]));
        }
        else if (index == speakingLines.Length - 1 && tutorialDiagram != null)
        {
            index++;
            dialogueText.text = string.Empty; // Empty the text area.
            tutorialDiagram.enabled = true;
        }
        // When finish dialogue
        else
        {
            gameObject.SetActive(false);
        }
    }
}

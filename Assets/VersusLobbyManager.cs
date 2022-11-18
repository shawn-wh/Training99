using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VersusLobbyManager : MonoBehaviour
{
    public TMP_Text Player1Text;
    public TMP_Text Player2Text;
    public GameObject Player1ControlSet;
    public GameObject Player2ControlSet;

    private bool isPlayer1Computer = false;
    private bool isPlayer2Computer = true;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            Switch("Player1");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Switch("Player2");
        }
    }
    
    public void Switch(string playerName)
    {
        if (playerName == "Player1")
        {
            isPlayer1Computer = !isPlayer1Computer;
            if (isPlayer1Computer)
            {
                Player1Text.text = "Computer";
                Player1ControlSet.SetActive(false);
                VersusGameManager.isPlayer1Computer = true;
            }
            else
            {
                Player1Text.text = "Player1";
                Player1ControlSet.SetActive(true);
                VersusGameManager.isPlayer1Computer = false;
            }
        }
        else
        {
            isPlayer2Computer = !isPlayer2Computer;
            if (isPlayer2Computer)
            {
                Player2Text.text = "Computer";
                Player2ControlSet.SetActive(false);
                VersusGameManager.isPlayer2Computer = true;
            }
            else
            {
                Player2Text.text = "Player2";
                Player2ControlSet.SetActive(true);
                VersusGameManager.isPlayer2Computer = false;
            }
        }
    }
}

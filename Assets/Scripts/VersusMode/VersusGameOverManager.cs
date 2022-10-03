using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class VersusGameOverManager : MonoBehaviour
{
    public TMP_Text Winner;
    // Start is called before the first frame update

    void Start()
    {
        Winner.text = VersusGameManager.winner;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

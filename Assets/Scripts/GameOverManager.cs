using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text Score;
    public TMP_InputField UserName;
    // Start is called before the first frame update
    int curScore = 0;

    void Start()
    {
        Debug.Log(GameManager.endlessTimeScore);
        Score.text = GameManager.endlessTimeScore.ToString("0.000");
        curScore = (int)Mathf.Round(GameManager.endlessTimeScore * 1000);
    }

    public void onSubmit() {
        Debug.Log("onSubmit");
        if (curScore != 0) {
            StartCoroutine(SubmitNewScore(UserName.text, curScore));
        }
        LoadScene("Menu");
    }

    IEnumerator SubmitNewScore(string userName, int score) {
        if (userName == "") userName = "Guest";
        string getUrl = GameManager.url + GameManager.privateCode + "/add/" + UnityWebRequest.EscapeURL(userName) + "/" + score;
        Debug.Log(getUrl);
        UnityWebRequest webRequest = UnityWebRequest.Get(getUrl);
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

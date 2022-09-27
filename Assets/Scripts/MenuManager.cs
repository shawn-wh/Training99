using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Networking;
using LitJson;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private const string url = "http://dreamlo.com/lb/";
    private const string privateCode = "pvRmq2uOCkS2mOf4UkgrrQ4H6IwWeH5E6IIdSedi4CCg";
    private const string publicCode = "6330fb5a8f40bc0fe885f629";
    private string[] leaderName = new string[10];
    private int[] leaderScore = new int[10];
    
    public TMP_Text LeaderBoardTop10;
    

    private void Start() {
        for(int i = 0; i < 10; i++) {
            leaderName[i] = "None";
            leaderScore[i] = 0;
        }
        StartCoroutine(GetHighestScore());
    }

    IEnumerator GetHighestScore() {
        string getUrl = url + publicCode + "/json/";
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
                var data  = JsonMapper.ToObject(webRequest.downloadHandler.text);
                var userData = data["dreamlo"]["leaderboard"]["entry"];
                
                
                if (userData.IsArray) {
                    Debug.Log(userData.Count);
                    int leaderCnt = Math.Min(userData.Count, 10);
                    string leaderboard = "";
                    for(int i = 0; i < leaderCnt; i++) {
                        leaderName[i] = userData[i]["name"].ToString();
                        leaderScore[i] = Int32.Parse(userData[i]["score"].ToString());
                    }
                    for(int i = 0; i < 10; i++) {
                        leaderboard += ((i+1).ToString() + ". \t" + leaderName[i] + "\t " + leaderScore[i] + "\n");
                    }
                    LeaderBoardTop10.text = leaderboard;
                    // int rank = 1;
                    // string leaderboard = "";
                    // foreach(JsonData user in userData) {
                    //     leaderboard += (rank + ". \t" + user["name"] + "\t " + user["score"] + "\n");
                    //     Debug.Log(user["name"] + ": " + user["score"]);
                    //     rank++;
                    // }
                    // LeaderBoardTop10.text = leaderboard;
                } else {
                    Debug.Log(userData["name"] + ": " + userData["score"]);
                }
                break;
        }
        
    }

    IEnumerator SubmitNewScore(string userName, int score) {
        string getUrl = url + privateCode + "/add/" + UnityWebRequest.EscapeURL(userName) + "/" + score;
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

    public void onStartGame(string SceneName) {
        SceneManager.LoadScene(SceneName);
    }
    
    public void QuitGame() {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

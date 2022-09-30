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
    private string[] leaderName = new string[10];
    private int[] leaderScore = new int[10];
    
    public TMP_Text LeaderBoardTop10Player;
    public TMP_Text LeaderBoardTop10Score;
    

    private void Start() {
        for(int i = 0; i < 10; i++) {
            leaderName[i] = "None";
            leaderScore[i] = 0;
        }
        StartCoroutine(GetHighestScore());
    }

    IEnumerator GetHighestScore() {
        string getUrl = GameManager.url + GameManager.publicCode + "/json/";
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
                    string leaderBoardPlayer = "";
                    string leaderBoardScore = "";
                    for(int i = 0; i < leaderCnt; i++) {
                        leaderName[i] = userData[i]["name"].ToString();
                        leaderScore[i] = Int32.Parse(userData[i]["score"].ToString());
                    }
                    for(int i = 0; i < 10; i++) {
                        leaderBoardPlayer += ((i+1).ToString() + ". \t" + leaderName[i] + "\n");
                        TimeSpan time = TimeSpan.FromSeconds((((float)leaderScore[i]) / 1000f));
                        String ts = string.Format("{0}:{1:D2}.{2}", (int)time.TotalMinutes, time.Seconds, time.Milliseconds);
                        leaderBoardScore += (ts + "\n");
                    }
                    LeaderBoardTop10Player.text = leaderBoardPlayer;
                    LeaderBoardTop10Score.text = leaderBoardScore;
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

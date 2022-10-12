using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string URL;
    // endless & level: https://docs.google.com/forms/u/1/d/e/1FAIpQLSdy6YYwcr26sC440S2r2_DmBnbaFcu_S3D5aq9VFtsM1g6a0w/formResponse

    //versus: 

    public void Send(string mode, string time)
    {
        StartCoroutine(Post(mode, time));
    }

    private IEnumerator Post(string mode, string time) 
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.237423642", mode); //game mode
        form.AddField("entry.1513281372", time); //game time

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }



}

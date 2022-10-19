using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string URL;
    // endless & level: https://docs.google.com/forms/u/1/d/e/1FAIpQLSdy6YYwcr26sC440S2r2_DmBnbaFcu_S3D5aq9VFtsM1g6a0w/formResponse

    //versus: https://docs.google.com/forms/u/1/d/e/1FAIpQLScE3x4_FsGn2qCutgGRXtL9N_JjeKy_Q9ZNfJmgYQADHjqasQ/formResponse

    public void Send(string mode, string time)
    {
        StartCoroutine(Post(mode, time));
    }

    public void SendVersus(string time, string winner, string prop1, string prop2, string prop3)
    {
        StartCoroutine(PostVersus(time, winner, prop1, prop2, prop3));
    }

    private IEnumerator Post(string mode, string time) 
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.237423642", mode); //game mode
        form.AddField("entry.1513281372", time); //game time

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }

    private IEnumerator PostVersus(string time, string winner, string prop1, string prop2, string prop3) 
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1449534939", time);
        form.AddField("entry.1957372487", winner);
        form.AddField("entry.1951083910", prop1);
        form.AddField("entry.1102651022", prop2);
        form.AddField("entry.1869320944", prop3);


        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }



}

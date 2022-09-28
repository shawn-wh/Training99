using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [SerializeField] private string URL;
    // https://docs.google.com/forms/u/1/d/e/1FAIpQLSfKNrOTpYJb-gMMYHeqQQgS4JqxlZFVN-k1uwohcMCM21acLg/formResponse

    public void Send(string mode, string time)
    {
        StartCoroutine(Post(mode, time));
    }

    private IEnumerator Post(string mode, string time) 
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.596436681", mode);
        form.AddField("entry.465407387", time);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }


}

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
        form.AddField("entry.596436681", mode); //gamemode
        
        if (string.Equals(mode, "Endless")) {
            form.AddField("entry.465407387", time); //gametime
        }

        if (string.Equals(mode, "Level1")) {
            form.AddField("entry.1255610277", time); //L1complete
        }

        if (string.Equals(mode, "Level2")) {
            form.AddField("entry.839314249", time); //L2complete
        }
        
        


        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }


}

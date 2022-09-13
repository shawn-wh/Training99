using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void onStartGame(string SceneName) {
        Application.LoadLevel(SceneName);
    }
}

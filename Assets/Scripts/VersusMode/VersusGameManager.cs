using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersusGameManager : MonoBehaviour
{
    public CreateArea[] createAreas = null;
    public VersusBullet clone = null;
    public GameObject bulletNode = null;
    public Sprite sprite = null;

    [SerializeField] private PropSelectPanelController panel;

    public VersusPlayer[] players;
    public static string winner = null;
    public static bool isPaused = true;
    
    void Start()
    {
        IEnumerator panelCoroutine = panel.ShowPanelInterval();
        StartCoroutine(panelCoroutine);
        panel.Show();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < createAreas.Length; i++)
        {
            CreateArea ca = createAreas[i];
            if (ca.CheckTime())
            {
                Vector3 pos = ca.GetRandomPos();
                // This function makes a copy of an object in a similar way to the Duplicate command in the editor.
                VersusBullet bullet = Instantiate(clone, pos, Quaternion.identity, bulletNode.transform);
                bullet.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                ca.NextTime();
            }
        }
    }
}

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
    public Color[] colors = new Color[2];

    [SerializeField] private PropSelectPanelController panel;

    public VersusPlayer[] players;
    public static string winner = null;
    public static bool isPaused = true;
    
    void Start()
    {
        colors[0] = new Color32(153, 0, 0, 255);
        colors[1] = new Color32(39, 116, 174, 255);
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
                bullet.SetColor(colors[Random.Range(0, colors.Length)]);
                bullet.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                ca.NextTime();
            }
        }
    }
}

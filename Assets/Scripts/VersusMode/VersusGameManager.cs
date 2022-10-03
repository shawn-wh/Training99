using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusGameManager : MonoBehaviour
{
    public CreateArea[] createAreas = null;
    public VersusBullet clone = null;
    public GameObject bulletNode = null;
    public Sprite sprite = null;
    public GameObject propSelectPanel = null;
    public static string winner = null;
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
            {
                propSelectPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
            }
            isPaused = !isPaused;
        }
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

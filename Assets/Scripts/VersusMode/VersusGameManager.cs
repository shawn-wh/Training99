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

    [SerializeField] private GameObject propSelectPanel = null;
    [SerializeField] private TextMeshProUGUI panelTimer;
    [SerializeField] private CardController[] availableCards;

    public VersusPlayer[] players;
    public static string winner = null;
    public static bool isPaused = false;
    private float panelTimeLeft = 5.0f;

    private CardController card0;
    private CardController card1;
    private CardController card2;
    private CardController card3;

    // Start is called before the first frame update
    void Start()
    {
        Transform cardsRoot = propSelectPanel.transform.Find("Cards");
        card0 = Instantiate(availableCards[0], cardsRoot);
        card1 = Instantiate(availableCards[0], cardsRoot);
        card2 = Instantiate(availableCards[0], cardsRoot);
        card3 = Instantiate(availableCards[0], cardsRoot);
        
        IEnumerator panelCoroutine = ShowPanel();
        StartCoroutine(panelCoroutine);
    }
    
    private IEnumerator ShowPanel()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            propSelectPanel.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            panelTimeLeft = 5.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            if (panelTimeLeft < 0)
            {
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
                panelTimeLeft = 0;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                PropPrototype prop = Instantiate(card0.prop, players[0].transform);
                prop.AssignAndEnable(players[0]);
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                PropPrototype prop = Instantiate(card1.prop, players[0].transform);
                prop.AssignAndEnable(players[0]);
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PropPrototype prop = Instantiate(card2.prop, players[1].transform);
                prop.AssignAndEnable(players[1]);
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PropPrototype prop = Instantiate(card3.prop, players[1].transform);
                prop.AssignAndEnable(players[1]);
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }

            panelTimeLeft -= Time.unscaledDeltaTime;
            panelTimer.text = "Time Left: " + panelTimeLeft.ToString("0.0") + " seconds";
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

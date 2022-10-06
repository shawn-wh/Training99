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

    [SerializeField] private GameObject propSelectPanel = null;
    [SerializeField] private CardController[] availableCards;

    public VersusPlayer[] players;
    public static string winner = null;
    public static bool isPaused = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.A))
        {
            PropPrototype prop = Instantiate(card0.prop, players[0].transform);
            prop.AssignAndEnable(players[0]);
            propSelectPanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.D))
        {
            PropPrototype prop = Instantiate(card1.prop, players[0].transform);
            prop.AssignAndEnable(players[0]);
            propSelectPanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PropPrototype prop = Instantiate(card2.prop, players[1].transform);
            prop.AssignAndEnable(players[1]);
            propSelectPanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.RightArrow))
        {
            PropPrototype prop = Instantiate(card3.prop, players[1].transform);
            prop.AssignAndEnable(players[1]);
            propSelectPanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {   
            if (isPaused)
            {
                propSelectPanel.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                propSelectPanel.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
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

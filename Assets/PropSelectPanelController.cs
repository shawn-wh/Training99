using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropSelectPanelController : MonoBehaviour
{
    [SerializeField] private VersusGameManager manager;
    [SerializeField] private TextMeshProUGUI panelTimer;
    [SerializeField] private TextMeshProUGUI player1ConfirmedText;
    [SerializeField] private TextMeshProUGUI player2ConfirmedText;
    [SerializeField] private CardController[] availableCards;
    private float panelTimeLeft = 5.0f;
    private bool isPlayer1Confirmed = false;
    private bool isPlayer2Confirmed = false;
    
    private CardController card0;
    private CardController card1;
    private CardController card2;
    private CardController card3;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform cardsRoot = transform.Find("Cards");
        card0 = Instantiate(availableCards[0], cardsRoot);
        card1 = Instantiate(availableCards[0], cardsRoot);
        card2 = Instantiate(availableCards[0], cardsRoot);
        card3 = Instantiate(availableCards[0], cardsRoot);
    }

    public IEnumerator ShowPanelInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            Show();
            panelTimeLeft = 5.0f;
            isPlayer1Confirmed = false;
            isPlayer2Confirmed = false;
            player1ConfirmedText.enabled = false;
            player2ConfirmedText.enabled = false;
        }
    }
    
    void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        VersusGameManager.isPaused = true;
    }
    
    void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        VersusGameManager.isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (VersusGameManager.isPaused)
        {
            if (panelTimeLeft < 0)
            {
                Hide();
                panelTimeLeft = 0;
            }
            else if (!isPlayer1Confirmed && Input.GetKeyDown(KeyCode.F))
            {
                PropPrototype prop = Instantiate(card0.prop, manager.players[0].transform);
                prop.AssignAndEnable(manager.players[0]);
                isPlayer1Confirmed = true;
                player1ConfirmedText.enabled = true;
                if (isPlayer2Confirmed)
                {
                    Hide();
                }
            }
            else if (!isPlayer1Confirmed && Input.GetKeyDown(KeyCode.G))
            {
                PropPrototype prop = Instantiate(card1.prop, manager.players[0].transform);
                prop.AssignAndEnable(manager.players[0]);
                isPlayer1Confirmed = true;
                player1ConfirmedText.enabled = true;
                if (isPlayer2Confirmed)
                {
                    Hide();
                }
            }
            else if (!isPlayer2Confirmed && Input.GetKeyDown(KeyCode.Comma))
            {
                PropPrototype prop = Instantiate(card2.prop, manager.players[1].transform);
                prop.AssignAndEnable(manager.players[1]);
                isPlayer2Confirmed = true;
                player2ConfirmedText.enabled = true;
                if (isPlayer1Confirmed)
                {
                    Hide();
                }
            }
            else if (!isPlayer2Confirmed && Input.GetKeyDown(KeyCode.Period))
            {
                PropPrototype prop = Instantiate(card3.prop, manager.players[1].transform);
                prop.AssignAndEnable(manager.players[1]);
                isPlayer2Confirmed = true;
                player2ConfirmedText.enabled = true;
                if (isPlayer1Confirmed)
                {
                    Hide();
                }
            }
            panelTimeLeft -= Time.unscaledDeltaTime;
            panelTimer.text = "Time Left: " + panelTimeLeft.ToString("0.0") + " seconds";
        }
    }
}

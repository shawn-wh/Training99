using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropSelectPanelController : MonoBehaviour
{
    [SerializeField] private VersusGameManager manager;
    
    private CardController card1;
    private CardController card2;
    
    void OnEnable()
    {
        Transform card1Root = transform.Find("Card1");
        Transform card2Root = transform.Find("Card2");
        
        foreach (Transform cardTransfrom in card1Root)
        {
            GameObject.Destroy(cardTransfrom.gameObject);
        }
        foreach (Transform cardTransfrom in card2Root)
        {
            GameObject.Destroy(cardTransfrom.gameObject);
        }
        
        // Randomly select 2 props
        int take1 = Random.Range(0, manager.AvailableCards.Length);
        card1 = Instantiate(manager.AvailableCards[take1], card1Root);
        card1.name = manager.AvailableCards[take1].name; // to remove the `(Clone)` suffix for the anylytics purpose

        int take2 = Random.Range(0, manager.AvailableCards.Length);
        while (take1 == take2)
        {
            take2 = Random.Range(0, manager.AvailableCards.Length);
        }
        card2 = Instantiate(manager.AvailableCards[take2], card2Root);
        card2.name = manager.AvailableCards[take2].name; // to remove the `(Clone)` suffix for the anylytics purpose
    }
    
    public void CreateProp(VersusPlayer player, CardController card = null)
    {
        card = (card == null)? card1 : card;
        PropPrototype prop = Instantiate(card.prop);
        prop.name = card.prop.name;
        player.UseProp(prop);
        gameObject.SetActive(false);
        manager.PropUsage[card.name] += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "PropPanel1")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CreateProp(manager.player1, card1);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                CreateProp(manager.player1, card2);
            }
        }
        else if (name == "PropPanel2")
        {
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                CreateProp(manager.player2, card1);
            }
            else if (Input.GetKeyDown(KeyCode.Period))
            {
                CreateProp(manager.player2, card2);
            }
        }
    }
}

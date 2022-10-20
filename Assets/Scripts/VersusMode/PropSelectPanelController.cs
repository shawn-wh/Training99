using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropSelectPanelController : MonoBehaviour
{
    [SerializeField] private VersusGameManager manager;
    
    private CardController card1;
    private CardController card2;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform card1Root = transform.Find("Card1");
        Transform card2Root = transform.Find("Card2");
        
        // Randomly select 2 props
        int take1 = Random.Range(0, manager.availableCards.Length);
        card1 = Instantiate(manager.availableCards[take1], card1Root);

        int take2 = Random.Range(0, manager.availableCards.Length);
        while (take1 == take2)
        {
            take2 = Random.Range(0, manager.availableCards.Length);
        }
        card2 = Instantiate(manager.availableCards[take2], card2Root);
    }

    // Update is called once per frame
    void Update()
    {
        if (name == "PropPanel1")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PropPrototype prop = Instantiate(card1.prop, manager.player1.transform);
                manager.player1.ReceiveProp(prop);
                gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                PropPrototype prop = Instantiate(card2.prop, manager.player1.transform);
                manager.player1.ReceiveProp(prop);
                gameObject.SetActive(false);
            }
        }
        else if (name == "PropPanel2")
        {
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                PropPrototype prop = Instantiate(card1.prop, manager.player2.transform);
                manager.player2.ReceiveProp(prop);
                gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Period))
            {
                PropPrototype prop = Instantiate(card2.prop, manager.player2.transform);
                manager.player2.ReceiveProp(prop);
                gameObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_WarpedTrap : MonoBehaviour
{
    private GameObject player = null;
    private float originalSpeed = 0f;
    [SerializeField] float speedLimit = 3f;
    private bool canShow = true;

    // Start is called before the first frame update
    void Start()
    {
        // Find target player
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
            originalSpeed = player.GetComponent<LV_PlayerMovement>().GetPlayerSpeed();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<LV_PlayerMovement>().SetPlayerSpeed(speedLimit);
            player.GetComponent<LV_PlayerMovement>().SetColorChanging(false);
            player.GetComponent<LV_PlayerMovement>().ReactionInWarpedTrap();
            // Show warning message
            if (canShow == true)
            {
                canShow = false;
                StartCoroutine(CreateWarning()); 
            }
        }
    }

    IEnumerator CreateWarning()
    {
        player.GetComponent<LV_PlayerMovement>().SetWarning("Trap!\n Cannot change color.");
        yield return new WaitForSeconds(5);     // Delay for 4 seconds
        canShow = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player got out of the trap.");
            player.GetComponent<LV_PlayerMovement>().SetPlayerSpeed(originalSpeed);
            player.GetComponent<LV_PlayerMovement>().SetColorChanging(true); 
            player.GetComponent<LV_PlayerMovement>().OutOfWarpedTrap(); 

        }    
    }

}

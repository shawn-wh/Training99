using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndpointOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            Debug.Log("Player found the endpoint!");
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                SceneManager.LoadScene("LV_GameOver");
            }
            else
            {
                SceneManager.LoadScene("Level2");
            }
            
        }
    }
}

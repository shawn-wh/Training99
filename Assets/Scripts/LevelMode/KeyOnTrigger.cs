using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _endpoint;

    public ProgressBar progressBar;
    
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
            Debug.Log("Player found the key!");
            _endpoint.GetComponent<Renderer> ().material.color = new Color(0, 0, 0, 255);
            _endpoint.GetComponent<Collider2D>().enabled = true;
            // _endpoint.SetActive(true);
            
            
            progressBar.UpdateValue(2f);
            Destroy(gameObject);
        }
    }
}

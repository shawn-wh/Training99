using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndpointOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;

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
            Debug.Log("Player found the endpoint!");
            string current = SceneManager.GetActiveScene().name;

            progressBar.UpdateValue(progressBar.slider.maxValue);

            Debug.Log(current + " finished time: " + Time.timeSinceLevelLoad.ToString("0.0"));
            DataManager dm = gameObject.GetComponent<DataManager>();
            dm.Send(current, Time.timeSinceLevelLoad.ToString("0.0"));
            Destroy(dm);

            SceneManager.LoadScene("LevelCleared");
        }
    }
}

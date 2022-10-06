using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusBullet : MonoBehaviour
{
    public VersusGameManager gameManager;

    public float bulletSpeed;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        VersusPlayer player = gameManager.players[Random.Range(0, gameManager.players.Length)];
        transform.LookAt(player.transform);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        gameObject.transform.GetChild(0).GetComponent<Image>().color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusBullet : MonoBehaviour
{
    public VersusGameManager gameManager;

    public float bulletSpeed;
    private Color color;
    private VersusPlayer target;

    // Start is called before the first frame update
    void Start()
    {
        target = gameManager.player1;
        if (Random.Range(0, 2) == 1)
        {
            target = gameManager.player2;
        }
    }


    // FixedUpdate is called once every 0.02 seconds
    void FixedUpdate()   
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.fixedDeltaTime * bulletSpeed);
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            VersusPlayer player = other.gameObject.GetComponent<VersusPlayer>();
            if (player.isInvincible)
            {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

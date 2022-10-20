using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusBullet : MonoBehaviour
{
    public VersusGameManager gameManager;

    public float bulletSpeed;
    private Color color;
    private VersusPlayer targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = gameManager.player1;
        if (Random.Range(0, 2) == 1)
        {
            targetPlayer = gameManager.player2;
        }
        transform.rotation = Quaternion.FromToRotation(Vector3.up, targetPlayer.transform.position - transform.position);
    }


    // FixedUpdate is called once every 0.02 seconds
    void FixedUpdate()   
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * bulletSpeed);

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

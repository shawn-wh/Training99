using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusBullet : MonoBehaviour, IColor
{
    public VersusGameManager gameManager;
    // Adding @ prefix to avoid reserved keyword
    public Color @Color
    {
        get
        {
            return color;
        }
        set {
            color = value;
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
    public bool IsRandomMoving = false;
    public VersusPlayer TargetPlayer;
    public float Speed = 0f;
    
    private Color color;
    private float liveTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        if (TargetPlayer.Opponent.Prop && TargetPlayer.Opponent.Prop.name == "RandomBulletProp")
        {
            IsRandomMoving = true;
        }
        
        transform.rotation = Quaternion.FromToRotation(Vector3.up, TargetPlayer.transform.position - transform.position);
        Destroy(gameObject, liveTime);
    }


    // FixedUpdate is called once every 0.02 seconds
    void FixedUpdate()   
    {
        if (IsRandomMoving)
        {
            transform.Rotate(0, 0, Random.Range(-45f, 45f));
        }
        float speed = (Speed == 0f)? gameManager.BulletSpeed : Speed;
        transform.Translate(Vector3.up * Time.fixedDeltaTime * speed);
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
                if (player.m_Hp > 1)
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }
}

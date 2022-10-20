using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusPlayer : MonoBehaviour
{
    public float speed;
    public int Hp_max;
    public int Energy_max;
    
    private int m_Hp;
    private int m_Energy;
    public HealthBar healthBar;
    public HealthBar energyBar;
    public string controlSet = null;  // Valid values: Player1, Player2
    public GameObject floatingTextPrefab; // Prefab to show damage/collectable text
    private PropPrototype prop;
    public bool isInvincible { get; set; } = false;
    [SerializeField] private VersusGameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        m_Hp = Hp_max;
        healthBar.SetMaxHealth(m_Hp);
        healthBar.SetHealth(m_Hp);
        m_Energy = 0;
        energyBar.SetMaxHealth(Energy_max);
        energyBar.SetHealth(m_Energy);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis(controlSet + " Horizontal"); // player1: A, D; player2: left, right
        float v = Input.GetAxis(controlSet + " Vertical");   // player1: W, S; player2: up, down

        Vector2 pos = transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        transform.position = pos;

        if (prop)
        {
            if (name == "Player1" && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.G)))
            {
                prop.enabled = true;
            }
            else if (name == "Player2" && (Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.Period)))
            {
                prop.enabled = true;
            }
        }
    }
    
    public void Heal(int amount)
    {
        m_Hp = Math.Min(m_Hp + amount, Hp_max);
        healthBar.SetHealth(m_Hp);
    }
    
    public void ReceiveProp(PropPrototype prop)
    {
        this.prop = prop;
        prop.owner = this;
        prop.manager = manager;
    }
    
    public void RemoveProp()
    {
        this.prop = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // No damage when colliding with walls.
        if (collision.gameObject.CompareTag("Walls") || isInvincible)
        {
            return;
        }

        Color bulletColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
        Color playerColor = gameObject.GetComponent<SpriteRenderer>().color;

        FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        if (bulletColor == playerColor) {
            m_Energy += 1;
            energyBar.SetHealth(m_Energy);
            printer.SetFloatingValue(+1);   // gain = positive value
        } else {
            m_Hp -= 1;
            healthBar.SetHealth(m_Hp);
            printer.SetFloatingValue(-1);
        }

        // Game over condition
        if (m_Hp <= 0)
        {
            string winner = "Player1";
            if (name == "Player1")
            {
                winner = "Player2";
            }
            VersusGameManager.winner = winner;
            SceneManager.LoadScene("VersusGameOver");
        }
        
        if (m_Energy >= Energy_max)
        {
            if (name == "Player1")
            {
                manager.propPanel1.gameObject.SetActive(true);
            }
            else
            {
                manager.propPanel2.gameObject.SetActive(true);
            }
            m_Energy = 0;
            energyBar.SetHealth(m_Energy);
        }

    }
} // class

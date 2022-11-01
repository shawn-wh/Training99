using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusPlayer : MonoBehaviour
{
    public bool isInvincible { get; set; } = false;

    private float speed;
    private int m_Hp;
    private int m_Energy;
    private PropPrototype prop;
    
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private HealthBar energyBar;
    [SerializeField] private VersusGameManager manager;
    [SerializeField] private GameObject floatingTextPrefab; // Prefab to show damage/collectable text

    // Start is called before the first frame update
    void Start()
    {
        m_Hp = manager.Hp_max;
        healthBar.SetMaxHealth(m_Hp);
        healthBar.SetHealth(m_Hp);
        m_Energy = 0;
        energyBar.SetMaxHealth(manager.Energy_max);
        energyBar.SetHealth(m_Energy);
        speed = manager.PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        if (name == "Player1")
        {
            h = Input.GetAxis("Player1 Horizontal"); // player1: A, D
            v = Input.GetAxis("Player1 Vertical");   // player1: W, S
        }
        else // Player2
        {
            h = Input.GetAxis("Player2 Horizontal"); // player2: left, right
            v = Input.GetAxis("Player2 Vertical");   // player2: up, down
        }

        Vector2 pos = transform.position;
        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;
        transform.position = pos;
    }
    
    public void Heal(int amount)
    {
        m_Hp = Math.Min(m_Hp + amount, manager.Hp_max);
        healthBar.SetHealth(m_Hp);
    }
    
    public void UseProp(PropPrototype prop)
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
            manager.SendForm();
            SceneManager.LoadScene("VersusGameOver");
        }
        
        if (m_Energy >= manager.Energy_max)
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

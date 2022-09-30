using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusPlayer : MonoBehaviour
{
    public float speed = 5f;
    public int maxHealth = 10;
    public int m_Hp = 10;
    public HealthBar healthBar;
    public string controlSet = null;  // Valid values: Player1, Player2
    // Prefab to show damage/collectable text
    public GameObject floatingTextPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = -1;
        m_Hp += damage;
        healthBar.SetHealth(m_Hp);

        // Show damage text
        FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        printer.SetFloatingValue(damage);   // damage = negative value

        // Game over condition
        if (m_Hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
} // class

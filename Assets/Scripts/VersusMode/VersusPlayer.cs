using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersusPlayer : MonoBehaviour
{
    public float speed = 5f;
    private int m_Hp = 3;
    public HealthBar healthBar;
    public string controlSet = null;  // Valid values: Player1, Player2
    public GameObject floatingTextPrefab; // Prefab to show damage/collectable text
    public CircleCollider2D m_collider;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(m_Hp);
        m_collider = GetComponent<CircleCollider2D>();
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
        m_Hp -= 1;
        healthBar.SetHealth(m_Hp);

        // Show damage text
        FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        printer.SetFloatingValue(-1);

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
    }
} // class

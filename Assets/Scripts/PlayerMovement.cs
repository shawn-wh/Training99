using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Player player = new Player(100, 0, "Player");

    public TextMeshProUGUI m_HpText = null;
    public TextMeshProUGUI m_TimeText = null;
    public int m_Hp = 1;

    public Color[] colors = new Color[3];
    public float timeToChange = 5f;
    private float timeSinceChange = 0f;


    // Start is called before the first frame update
    void Start()
    {
        RefreshHpText();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // key: A, D, left, right
        float v = Input.GetAxis("Vertical"); // key: W, S, up, down

        Vector2 pos = transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        transform.position = pos;

        m_TimeText.text = "Time: " + Time.time.ToString("0.0");

        ChangeColor();
    }

    private void ChangeColor()
    {
        timeSinceChange += Time.deltaTime;

        if (timeSinceChange >= timeToChange)
        {
            Color newColor = colors[Random.Range(0, colors.Length)];

            while (newColor == gameObject.GetComponent<Image>().color)
            {
                newColor = colors[Random.Range(0, colors.Length)];
            }
            gameObject.GetComponent<Image>().color = newColor;
            timeSinceChange = 0f;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError("OnTriggerEnter2D");
        Debug.Log("Collision obj color: " + collision.transform.GetChild(0).GetComponent<Image>().color);
        Debug.Log("Collision player color: " + gameObject.GetComponent<Image>().color);
        if (collision.transform.GetChild(0).GetComponent<Image>().color != gameObject.GetComponent<Image>().color)
        {
            m_Hp--;
        }
        
        RefreshHpText();
    }

    private void RefreshHpText()
    {
        m_HpText.text = "HP: " + m_Hp;
    }
} // class

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LV_PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;

    public HealthBar healthBar;

    public TextMeshProUGUI m_HpText = null;
    public TextMeshProUGUI m_TimeText = null;
    public int m_Hp = 10;

    public int maxHealth = 10;
    public int currentHealth;

    // Colors = red, yellow, blue ; // set aphla = 1
    // private Color[] coloris = { new Color(162,52,25,255), new Color(244,187,15,255), new Color(47,55,91,255)};
    public Color[] colors;
    public float timeToChange = 5f;
    private float timeSinceChange = 0f;

    public float timeToWarn = 3f;
    private float timeSinceWarn = 0f;

    // UI show collectables (Collect 3 types of bullets)
    public TextMeshProUGUI UI_Collectable1 = null;
    public TextMeshProUGUI UI_Collectable2 = null;
    public TextMeshProUGUI UI_Collectable3 = null;
    private int[] collectables = new int[3]; // Record values for collectables. 

    // Required type and number of bullets
    private const int CIRCLE_GOAL = 0;
    private const int TRIANGLE_GOAL = 1;
    private const int SQUARE_GOAL = 0;

    // Prefab to show damage/collectable text
    public GameObject floatingTextPrefab;

    //prefab to show warning text
    public GameObject warnTextPrefab;


    // SpriteRenderer
    private SpriteRenderer sRenderer;

    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _endpoint;


    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(sRenderer);
        
        RefreshHpText();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        // _key & _endpoint invisible
        _key.SetActive(false);
        _endpoint.SetActive(false);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // key: A, D, left, right
        float v = Input.GetAxis("Vertical"); // key: W, S, up, down

        Vector2 pos = transform.position;

        pos.x += h * playerSpeed * Time.deltaTime;
        pos.y += v * playerSpeed * Time.deltaTime;

        transform.position = pos;

        // Time.timeSinceLevelLoad  will reset time when loading new scence.
        m_TimeText.text = "Survived: " + Time.timeSinceLevelLoad.ToString("0.0");

        // Change player color every "timeToChange" sec
        ChangeColor(); 
    }

    private void ChangeColor()
    {
        timeSinceChange += Time.deltaTime;
        timeSinceWarn += Time.deltaTime;
        Color newColor = colors[Random.Range(0, colors.Length)];
       
        

        if(timeSinceWarn >= timeToWarn)
        {
            WarnText printer = Instantiate(warnTextPrefab, transform.position, Quaternion.identity).GetComponent<WarnText>();

           
            timeSinceWarn = 0f;
            
        }
        
        
        if (timeSinceChange >= timeToChange)
        {
            
            while (newColor == gameObject.GetComponent<SpriteRenderer>().color)
            {
                newColor = colors[Random.Range(0, colors.Length)];
            }

			newColor.a = 1f;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            timeSinceWarn = 0f;
            timeSinceChange = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != _key && collision.gameObject != _endpoint)
        {
            Color bulletColor = collision.GetComponent<SpriteRenderer>().color;
            Color playerColor = gameObject.GetComponent<SpriteRenderer>().color;
            Debug.Log("Collision obj color: " + bulletColor); 
            Debug.Log("Collision player color: " + playerColor);
            
            int damage = -1; 
            // Different color, player take damage
            if ( playerColor != bulletColor)
            {
                m_Hp += damage;
                currentHealth = m_Hp;
                healthBar.SetHealth(currentHealth);
    
    
                // Show damage text
                FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
                printer.SetFloatingValue(damage);   // damage = negative value
            }
            // Same color: player can collect the bullet as resources
            else
            {
                Sprite bulletType = collision.GetComponent<SpriteRenderer>().sprite;
                if (bulletType.name == "Circle")
                {
                    collectables[0] += 1;
                }
                else if (bulletType.name == "Triangle")
                {
                    collectables[1] += 1;
                }
                else if (bulletType.name == "Square")
                {
                    collectables[2] += 1;
                }

                // player collects required type and number of bullets, show the key
                if (collectables[0] == CIRCLE_GOAL && collectables[1] == TRIANGLE_GOAL &&
                    collectables[2] == SQUARE_GOAL)
                {
                    _key.SetActive(true);
                }

                // Show gain text

                FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
                printer.SetFloatingValue(+1);   // gain = positive value
            }

            RefreshHpText();
            

        }

        // Game over condition
        if (m_Hp <= 0)
        {
            DataManager dm = gameObject.GetComponent<DataManager>();
            // Debug.Log(Time.timeSinceLevelLoad.ToString("0.0"));
            float levelTimeScore =  Time.timeSinceLevelLoad;
            // GameManager.endlessTimeScore = endlessTimeScore;
            // dm.Send("Level", endlessTimeScore.ToString("0.0"));
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
        
    }

    private void RefreshHpText()
    {
        m_HpText.text = "HP: " + m_Hp;
        UI_Collectable1.text = collectables[0].ToString();  // Circle	
        UI_Collectable2.text = collectables[1].ToString();  // Triangle	
        UI_Collectable3.text = collectables[2].ToString();  // Square	
    }
} // class

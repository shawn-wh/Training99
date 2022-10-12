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
    public int m_Hp = 5;

    public int maxHealth = 5;
    public int currentHealth;

    // Colors = red, yellow, blue ; // set aphla = 1
    // private Color[] coloris = { new Color(162,52,25,255), new Color(244,187,15,255), new Color(47,55,91,255)};
    public Color[] colors;
    public Color nextColor;
    public TextMeshProUGUI nextColorText;
    public string returnColor;
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
    public int CIRCLE_GOAL = 1;
    public int TRIANGLE_GOAL = 0;
    public int SQUARE_GOAL = 0;

    // Prefab to show damage/collectable text
    public GameObject floatingTextPrefab;

    //prefab to show warning text
    public GameObject warnTextPrefab;


    // SpriteRenderer
    private SpriteRenderer sRenderer;
    private GameObject movingCameraBound = null;


    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _endpoint;


    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        if (movingCameraBound == null )
        {
            movingCameraBound = GameObject.Find("MovingCameraBound");
        }

        RefreshHpText();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // _key & _endpoint invisible
        _key.SetActive(false);
        _endpoint.SetActive(false);

        //Generate next color
        nextColor = colors[Random.Range(0, colors.Length)];
        while (nextColor == gameObject.GetComponent<SpriteRenderer>().color)
        {
            nextColor = colors[Random.Range(0, colors.Length)];
        }
        nextColor.a = 1f;

        //initialize nextColorText
        RefreshNextColorText();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // key: A, D, left, right
        float v = Input.GetAxis("Vertical"); // key: W, S, up, down

        // Normalized to have the same moving speed for all directions
        Vector3 direction = new Vector3(h, v, 0).normalized;
        transform.position += direction * playerSpeed * Time.deltaTime;

        // Replaced codes (Degree 45, 135, 225, 315 will be faster than 4 directions)
        // Vector2 pos = transform.position;
        // pos.x += h * playerSpeed * Time.deltaTime;
        // pos.y += v * playerSpeed * Time.deltaTime;
        // transform.position = pos;

        // Time.timeSinceLevelLoad  will reset time when loading new scence.
        m_TimeText.text = "Survived: " + Time.timeSinceLevelLoad.ToString("0.0");

        // Change player color every "timeToChange" sec
        ChangeColor();
        RefreshNextColorText();
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
            gameObject.GetComponent<SpriteRenderer>().color = nextColor;
            
            while (nextColor == gameObject.GetComponent<SpriteRenderer>().color)
            {
                nextColor = colors[Random.Range(0, colors.Length)];
            }

			nextColor.a = 1f;
            timeSinceWarn = 0f;
            timeSinceChange = 0f;
        }

    }

    private void checkGameOver()
    {
        // Game over condition
        if (currentHealth <= 0)
        {
            DataManager dm = gameObject.GetComponent<DataManager>();
            // Debug.Log("player dead time: " + Time.timeSinceLevelLoad.ToString("0.0"));
            string currentLevel = SceneManager.GetActiveScene().name;
            dm.Send(currentLevel, "-1");
            Destroy(dm);
            gameObject.SetActive(false);
            SceneManager.LoadScene("LV_GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
        checkGameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
        checkGameOver();
    }

    // A Better Way To Manage Collision in Unity 
    // https://www.youtube.com/watch?v=TRvnN4bfAxM&t=163s&ab_channel=LostRelicGames
    private void ProcessCollision(GameObject collider)
    {
        if (collider.CompareTag("Bullets"))
        {
            DamageOrGain(collider);
        }
    }

    // Generic Method for both Collision2D & Collider2D
    private void DamageOrGain(GameObject other)
    {
        Color bulletColor = other.GetComponent<SpriteRenderer>().color;
        Color playerColor = gameObject.GetComponent<SpriteRenderer>().color;
        Debug.Log("Collision obj color: " + bulletColor); 
        Debug.Log("Collision player color: " + playerColor);
        
        int damage = -1;
        int gain = 1;
        // Different color, player take damage
        if (bulletColor.Equals(Color.black))
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
            m_Hp += gain;
            m_Hp = m_Hp < maxHealth ? m_Hp : maxHealth;
            currentHealth = m_Hp;
            healthBar.SetHealth(currentHealth);
            
            Sprite bulletType = other.GetComponent<SpriteRenderer>().sprite;
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
            if (collectables[0] >= CIRCLE_GOAL && 
                collectables[1] >= TRIANGLE_GOAL &&
                collectables[2] >= SQUARE_GOAL)
            {
                _key.SetActive(true);
            }

            // Show gain text

            FloatingText printer = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity).GetComponent<FloatingText>();
            printer.SetFloatingValue(+1);   // gain = positive value
        }

        RefreshHpText();
    }


    private void RefreshHpText()
    {
        m_HpText.text = "HP: " + m_Hp;
        UI_Collectable1.text = collectables[0].ToString() + " / " + CIRCLE_GOAL.ToString();  // Circle	
        UI_Collectable2.text = collectables[1].ToString() + " / " + TRIANGLE_GOAL.ToString();  // Triangle	
        UI_Collectable3.text = collectables[2].ToString() + " / " + SQUARE_GOAL.ToString();  // Square	
    }


    private string ConvertColorToString()
    {
        if (nextColor == colors[0])
        {
            return "Blue";
        }
        else if (nextColor == colors[1])
        {
            return "Red";
        }
        else
        {
            return "Yellow";
        }
    }
    
    private void RefreshNextColorText()
    {
        returnColor = ConvertColorToString();
        nextColorText.text = "Next Color: " + returnColor;
        
    }
} // class

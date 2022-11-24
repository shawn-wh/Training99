using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LV_PlayerMovement : MonoBehaviour
{

    // colors array: red, yellow, blue ; // set aphla = 1 = 255
    [SerializeField] Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue;
    
    private Color nextColor;
    private TextMeshProUGUI nextColorText;
    private string returnColor;


    
    [Header("Connect to UI_States")]
    private float playerSpeed;



    
    // public HealthBar healthBar;  // old Red color HealthBar
    public LV_HealthMeter healthBar;    // new HealthMeter that displays on Player

    // Progress Bar
    public ProgressBar progressBar;

    public TextMeshProUGUI m_HpText = null;
    public TextMeshProUGUI m_TimeText = null;
    public int m_Hp = 5;

    public int maxHealth = 5;
    public int currentHealth;


    // UI show collectables (Collect 3 types of bullets)
    public TextMeshProUGUI UI_Collectable1 = null;
    public TextMeshProUGUI UI_Collectable2 = null;
    public TextMeshProUGUI UI_Collectable3 = null;
    private int[] collectables = new int[3]; // Record values for collectables. 

    // Required type and number of bullets
    [Header("Task goal setup")]
    public int CIRCLE_GOAL = 1;
    public int TRIANGLE_GOAL = 0;
    public int SQUARE_GOAL = 0;

    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _endpoint;
    private bool isKeyShowedUp = false;

    // Prefab to show damage/collectable text
    [Header("Damage or gain")]
    public GameObject floatingTextPrefab;

    //prefab to show warning text
    public GameObject warnTextPrefab;


    // SpriteRenderer
    private SpriteRenderer sRenderer;
    private GameObject movingCameraBound = null;


    //player changing color parameter
    [Header("Color-changing related")]
    public int colorIdx;
    public float cooldownTime = 0.0f;
    private float nextChangeTime = 0;
    private bool enableColorChanging = true;

    //lock for minimizing
    int yellowLock;
    
    //lock for speeding up
    int blueLock;

    // Shape skills:
    [Header("Shape Related")]
    [SerializeField] private bool enableShapeChanging = false;  // Default is disable.

    private Sprite[] playerShapes = new Sprite[3];
    // [SerializeField] private bool[] isShapeSkillEnabled = new bool[3];  // Default is false 
    private int shapeIdx = 0;

    [Header("Shape skills CD setup")]
    [SerializeField] private float cooldownTime_skill1 = 2f;
    [SerializeField] private float cooldownTime_skill2 = 2f;
    [SerializeField] private float cooldownTime_skill3 = 2f;
    private bool canUseSkill1 = true; 
    private bool canUseSkill3 = true; 

    // Circle shape skill 
    [Header("For circle shape skill")]
    public Color circleAreaColor_0;
    public Color circleAreaColor_1;

    // Triangle shape skill 
    [Header("For triangle shape skill")]
    public float activeSpeed = .5f;
    public float dashSpeed;
    public float dashLength = .5f; //  dashCoolDown = cooldownTime_skill2 
    private float dashCounter;
    private float dashCoolCounter;

    // Square shape skill 
    [Header("For Square shape skill")]
    [SerializeField] private int shootAmount = 8;
    private Vector2 objMoveDirection;



    // For Warped Trap
    private Vector3 loadingRotation = new Vector3(0, 0, 30);
    private bool isInTrap = false;
    private Sprite previousSprite;

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

        playerSpeed = activeSpeed;

        // _key.SetActive(true); 
        // _key.GetComponent<Renderer> ().material.color = new Color(249, 253, 157, 80);
        // _endpoint.SetActive(true);
        // _endpoint.GetComponent<Renderer> ().material.color = new Color(113, 227, 229, 80);
        _key.GetComponent<Collider2D>().enabled = false;
        _endpoint.GetComponent<Collider2D>().enabled = false;
        

        //Generate next color
        colorIdx = 1;
        nextColor = colors[colorIdx];
        nextColor.a = 1f;
        yellowLock = 0;
        blueLock = 0;

        //initialize nextColorText
        // RefreshNextColorText();
        SetPlayerShapes();

        // For circle skill
        InitCircleArea();

    }

    
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
        // m_TimeText.text = "Survived: " + Time.timeSinceLevelLoad.ToString("0.0");

        ChangeColor();
        ChangeShape();
        // RefreshNextColorText();
        
        FindShapeSkill();
        TrappedEffect();
    }

    private void TrappedEffect()
    {
        // Rotate transform when in WarpedTrap
        if (isInTrap)
        {
            transform.Rotate(loadingRotation * playerSpeed * Time.deltaTime);
            healthBar.SetActive(false);     // Do not show healthMeter
        }
        else
        {
            // Rotate back to default:(0,0,0)
            transform.localRotation = Quaternion.identity;
            healthBar.SetActive(true);     // Show healthMeter
        }
    }

    private void ChangeColor()
    {
        if (enableColorChanging) 
        {
            if (Time.time > nextChangeTime)
            {
                if (Input.GetKeyDown("space"))
                {
                    colorIdx++;
                    gameObject.GetComponent<SpriteRenderer>().color = nextColor;
                    nextColor = colors[colorIdx % 3];
                    // RefreshNextColorText();
                    nextChangeTime = Time.time + cooldownTime;
                }
            }
        }
    }

    private void ChangeShape()
    {
        // if (enableShapeChanging
        //     && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))) 
        if (enableShapeChanging && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Change Shape!");
            shapeIdx++;
            shapeIdx = shapeIdx % playerShapes.Length;
            
            GameObject area = GameObject.FindGameObjectWithTag("CircleArea");    
            if (shapeIdx == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerShapes[0];
                area.GetComponent<Renderer>().material.color = circleAreaColor_1;
            }
            else if (shapeIdx == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerShapes[1];
                area.GetComponent<Renderer>().material.color = circleAreaColor_0;
            }
            else if (shapeIdx == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerShapes[2];
                area.GetComponent<Renderer>().material.color = circleAreaColor_0;
            }
            else
            {
                Debug.Log("Error! The number of shapes for player is greater than 3.");
            }
        } 
    }

    private void checkGameOver()
    {
        // Game over condition
        if (m_Hp <= 0)
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
        if (collider.CompareTag("Bullets") || collider.CompareTag("BulletPatrol") )
        {
            DamageOrGain(collider);
        }
    }

    // Generic Method for both Collision2D & Collider2D
    private void DamageOrGain(GameObject other)
    {
        Color bulletColor = other.GetComponent<SpriteRenderer>().color;
        Color playerColor = gameObject.GetComponent<SpriteRenderer>().color;
        // Debug.Log("Collision obj color: " + bulletColor);
        // Debug.Log("Collision player color: " + playerColor);

        int damage = -1;
        // Different color, player take damage
        if (playerColor != bulletColor || bulletColor.Equals(Color.black))
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


            int gain = 1;
            //each color has its own feature

            //color red can gain hp
            if (bulletColor == colors[0])
            {
                m_Hp += gain;
                m_Hp = m_Hp < maxHealth ? m_Hp : maxHealth;
                currentHealth = m_Hp;
                healthBar.SetHealth(currentHealth);
            }
            //color yellow can shrink player
            else if (bulletColor == colors[1] && yellowLock < 3)
            {
                yellowLock += 1;
                transform.localScale -= new Vector3(0.15f, 0.15f, 0);
            }
            //color blue can pass wall
            else if (bulletColor == colors[2] && blueLock < 3)
            {
                blueLock += 1;
                playerSpeed += 0.8f;
            }

            // player collects required type and number of bullets, show the key
            // Key is never acquired by player before
            if (!isKeyShowedUp) 
            {
                if (collectables[0] >= CIRCLE_GOAL &&
                    collectables[1] >= TRIANGLE_GOAL &&
                    collectables[2] >= SQUARE_GOAL)
                {
                    // _key.SetActive(true);
                    _key.GetComponent<Renderer> ().material.color = new Color(0, 0, 0, 255);
                    _key.GetComponent<Collider2D>().enabled = true;
                    isKeyShowedUp = true;
                    progressBar.UpdateValue(1f);
                }

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
        if (nextColor == colors[2])
        {
            return "Blue";
        }
        else if (nextColor == colors[0])
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

    public Color[] GetColorArray()
    {
        return colors;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public void SetPlayerSpeed(float value)
    {
        playerSpeed = value;
    }


    // Check color-changing ability
    public bool GetColorChanging()
    {
       return enableColorChanging; 
    }

    // Enable/disable color-changing ability
    public void SetColorChanging(bool value)
    {
       enableColorChanging = value; 
    }

    // Show Warning Text
    public void SetWarning(string warning)
    {
        WarnText printer = Instantiate(warnTextPrefab, transform.position, Quaternion.identity).GetComponent<WarnText>();
        printer.setContent(warning);
    }

    public void BeforeTrap()
    {
        previousSprite = gameObject.GetComponent<SpriteRenderer>().sprite; 
        Debug.Log("previousSprite = " + previousSprite);
    }

    // When player enter WarpedTrap
    public void ReactionInWarpedTrap()
    {
        isInTrap = true;
        Sprite busySprite = Resources.Load<Sprite>("Sprites/loading-100-1");  // Must exist in "Resources" folder
        gameObject.GetComponent<SpriteRenderer>().sprite = busySprite;
    }

    // When player get out of WarpedTrap
    public void OutOfWarpedTrap()
    {
        isInTrap = false;
        Sprite circleSprite = Resources.Load<Sprite>("Sprites/Circle");  // Must exist in "Resources" folder
        gameObject.GetComponent<SpriteRenderer>().sprite = previousSprite;
    }

    private void SetPlayerShapes()
    {
        playerShapes[0] = Resources.Load<Sprite>("Sprites/Circle");         // Must exist in "Resources" folder
        playerShapes[1] = Resources.Load<Sprite>("Sprites/Triangle");     // Must exist in "Resources" folder
        playerShapes[2] = Resources.Load<Sprite>("Sprites/Square");         // Must exist in "Resources" folder
        // Debug.Log("Length of playerShapes = " + playerShapes.Length);
    }

    // Find the corresponding skill for each shape
    private void FindShapeSkill()
    {
        // if (enableShapeChanging 
        //     && (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)))         
        if (enableShapeChanging && Input.GetKeyDown(KeyCode.E))
        {
            // Check current player's shape
            Sprite currentShape = gameObject.GetComponent<SpriteRenderer>().sprite;
            if (currentShape.name == "Circle")
            {
                LoadSkill1(); 
            }
            else if (currentShape.name == "Triangle")
            {
                LoadSkill2(); 
            }
            else if (currentShape.name == "Square")
            {
                LoadSkill3();
            }
            else
            {
                Debug.Log("Error! Cannot find the corresponding skill for the current shape.");
            }
        }

        // Part of Dashing
        if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Triangle")
        {
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    playerSpeed = activeSpeed;
                    dashCoolCounter = cooldownTime_skill2;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        
    }
    
    public float[] GetShapeSkillsCooldown()
    {
        float[] skillsCooldownSetup = new float[3];
        skillsCooldownSetup[0] = cooldownTime_skill1; 
        skillsCooldownSetup[1] = cooldownTime_skill2; 
        skillsCooldownSetup[2] = cooldownTime_skill3;
        return skillsCooldownSetup; 
    }

    IEnumerator shapeSkillsCooldown(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        // After the waiting
        canUseSkill1 = true;
        canUseSkill3 = true;
    }

    // Load the corresponding skill for each shape
    private void LoadSkill1()
    {
        // Debug.Log("Using Skill1 circle");
        if (canUseSkill1) 
        {
            Skill1Content();
            // wait for cooldown 
            canUseSkill1 = false;
            StartCoroutine(shapeSkillsCooldown(cooldownTime_skill1));
        }
    }

    private void Skill1Content()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Bullets");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject obj in objs)
        {
            float distance = Vector3.Distance(player.transform.position, obj.transform.position);
            // Debug.Log("distance is: " + distance);
            if (distance <= 2)
            {
                obj.SetActive(false);
            }
        }
    }

    void InitCircleArea()
    {
        circleAreaColor_0 = new Color();
        circleAreaColor_1 = new Color(0f, 0f, 0f, 0.8f);

        GameObject area = GameObject.FindGameObjectWithTag("CircleArea");

        // Only show when the level allow to change shape    
        if (enableShapeChanging)
        {
            area.SetActive(true);
        }
        else
        {
            area.SetActive(false);
        }
    }

    private void LoadSkill2()
    {
        Debug.Log("Using Skill2 triangle");

        if (dashCoolCounter <= 0 && dashCounter <= 0)
        {
            playerSpeed = dashSpeed;
            dashCounter = dashLength;
        }
    }

    private void LoadSkill3()
    {
        // Debug.Log("Using Skill3 square");
        if (canUseSkill3) 
        {
            ShootBrush();
            // wait for cooldown 
            canUseSkill3 = false;
            StartCoroutine(shapeSkillsCooldown(cooldownTime_skill3));
        }
    }

    private void ShootBrush()
    {
        Color playerColor = gameObject.GetComponent<SpriteRenderer>().color;

        float startAngle = 0f, endAngle = 360f;
        float angleStep = (endAngle - startAngle) / shootAmount;    // spread in range
        float angle = startAngle;  

        for (int i = 0; i < shootAmount; i++)
        {
            // Radian Angle = (Degree Angle) x pi/ 180
            float componentX = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
            float componentY = transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);

            Vector3 objMoveVector3 = new Vector3(componentX, componentY, 0f);
            Vector2 objDirection = (objMoveVector3 - transform.position).normalized;

            // Assign computation result to object
            GameObject obj = LV_ObjectPool2.poolInstance.GetObjectFromPool();
            obj.transform.position = transform.position;
            
            obj.transform.rotation = transform.rotation;
            // obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, objDirection);

            // Change sprite based on player's current color
            if (playerColor == colors[0])
            {
                Sprite redBrush = Resources.Load<Sprite>("Sprites/icons8-brush-64-red");  // Must exist in "Resources" folder
                obj.GetComponent<SpriteRenderer>().sprite = redBrush;     
            }
            else if (playerColor == colors[1])
            {
                Sprite yellowBrush = Resources.Load<Sprite>("Sprites/icons8-brush-64-yellow");  // Must exist in "Resources" folder
                obj.GetComponent<SpriteRenderer>().sprite = yellowBrush;   
            }
            else if (playerColor == colors[2]) 
            {
                Sprite blueBrush = Resources.Load<Sprite>("Sprites/icons8-brush-64-blue");  // Must exist in "Resources" folder
                obj.GetComponent<SpriteRenderer>().sprite = blueBrush;  
            }

            obj.SetActive(true);
            obj.GetComponent<LV_Brush>().SetMoveDirection(objDirection);
            angle += angleStep;
        }
    }

    public bool GetEnableShapeChanging() {
        return enableShapeChanging;
    }
} // class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LV_BulletPatrol : MonoBehaviour
{
    public float speed = 3f;
    private GameObject player = null;
    private Transform target;

    [SerializeField] private Color[] colors;  
    private Color currentColor;
    private Color playerColor;

    // Distance between bullet & player
    private float distance;
    public float detectRange = 4f;

    // Variables for patroling
    // public Transform[] waypoints;   // Given patroling waypoints
    public List<Transform> waypoints = new List<Transform>();   // Given patroling waypoints
    private int pointIndex = 0;
    private float waitTime = 8f;   
    public float startWaitTime = 5f;    // Wait 8 sec to start moving

    // BulletPatrol can talk
    [Header("Speech Bubble")]
    public GameObject speechBubblePrefab;
    private GameObject bubbleObj;
    
    // Start is called before the first frame update
    void Start()  
    {   
        // Set colors if no specific colors are assigned 
        if (colors.Length == 0)
        { 
            // black, Red, yellow, blue, ;
            Color[] defaultColors = {new Color32(0,0,0,255), new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};
            colors = defaultColors;
        }

        // Initial color setup 
        if (colors.Length < 1)
        {
            Debug.Log("Need to assign colors to BulletPatrol. (LV_BulletPatrol)");
        }
        else if (colors.Length == 1)
        {
            currentColor = colors[0];
        }
        else
        {
            currentColor =  colors[Random.Range(0, colors.Length)];
        }
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;

        // Initial waypoints setup
        if (waypoints.Count < 1) // waypoints list is empty
        {
            waypoints.Add(gameObject.transform);
            // Debug.Log("Need to assign waypoints to BulletPatrol. (LV_BulletPatrol)");
        }

        // Get player's transform and color
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
            // Debug.Log("playerColor = " + playerColor);
        }
        
        // Read SpeechBubble prefab
        GameObject obj = Instantiate(speechBubblePrefab);
        bubbleObj = obj;
        bubbleObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(target.position, transform.position);
        // Attack when player is within distance
        if (distance <= detectRange)
        {
            foundPlayer();

            // SpeechBubble appear.
            bubbleObj.SetActive(true); 
        }
        // Continue patrol
        else 
        {
            patroling();
            
            // SpeechBubble disappear.
            bubbleObj.SetActive(false); 
        }
      
    }

    private void patroling()
    {
        // Enemy will patrol when waypoints[] is given
        if (waypoints.Count >= 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[pointIndex].position, speed * Time.deltaTime);
            if (waitTime <= 0)
            {
                pointIndex++;
                if (pointIndex == waypoints.Count)
                {
                    pointIndex = 0;
                }
                waitTime = startWaitTime;   // reset
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void foundPlayer()
    {
        // detecting playerColor
        playerColor = player.GetComponent<SpriteRenderer>().color;
        

        // When playerColor is not the same as currentColor
        if (currentColor != playerColor)
        {
            // Face forward to player
            Vector2 direction = target.transform.position - gameObject.transform.position;
            direction = direction.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            // Move toward to player
            float towardStep = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, towardStep);

            // Show speech bubble
            bubbleObj.GetComponent<SpeechBubble>().SetSpeechText("Freeze!\n Don't move!");
            bubbleObj.GetComponent<SpeechBubble>().ChangeEmoji(Resources.Load<Sprite>("Sprites/Emoji_angry"));
            bubbleObj.transform.position = Vector2.MoveTowards(transform.position, target.position, towardStep); 
        }
        // When playerColor is the same as currentColor
        else 
        {
            // Face opposite to player's direction
            Vector2 direction = target.transform.position - gameObject.transform.position;
            direction = direction.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, -direction);

            // Run away from player
            float towardStep = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, -target.position, towardStep);

            // Show speech bubble
            bubbleObj.GetComponent<SpeechBubble>().SetSpeechText("Don't eat me!");
            bubbleObj.GetComponent<SpeechBubble>().ChangeEmoji(Resources.Load<Sprite>("Sprites/Emoji_scared"));
            bubbleObj.transform.position = Vector2.MoveTowards(transform.position, target.position, towardStep); 
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerColor = player.GetComponent<SpriteRenderer>().color;

        // Only destroy when being hitted by the same playerColor
        if (other.gameObject == player && currentColor == playerColor)
        {
            Destroy(gameObject);    // Delete BulletPatrol itself
            Destroy(bubbleObj);     // Delete speech bubble
        }
    }

}

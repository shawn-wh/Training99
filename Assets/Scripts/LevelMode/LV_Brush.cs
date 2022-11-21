using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_Brush : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float liveTime_Const = 10f;
    private Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        // Called when the gameobject is loaded.
        // Pull out from the object pool, and start count living time.

        // Decide the living time
        Invoke("DestroyBrush", liveTime_Const);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirection * Time.fixedDeltaTime * moveSpeed * 0.3f);

        // Vector2 norm_direction = moveDirection.normalized;
        // transform.rotation = Quaternion.FromToRotation(Vector3.up, norm_direction);
    }

    public void SetMoveDirection(Vector2 _direction)
    {
        moveDirection = _direction;
    }

    private void DestroyBrush()
    {
        // Put into the object pool
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // gameObject.SetActive(false);
        ProcessCollision(collision.gameObject);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject other)
    {
        // Get the current brush color
        Sprite brushType = gameObject.GetComponent<SpriteRenderer>().sprite;
        // Debug.Log("The current brush name = " + brushType );

        // Paint the collided bullets to the same color;
        if (other.CompareTag("Bullets"))
        {
            Debug.Log("Brush & bullets collision");
            // Find the corresponding color
            if (brushType.name == "icons8-brush-64-red")
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = colors[0];
                Debug.Log("Set collided bullet color to = " + other.GetComponent<SpriteRenderer>().color);
            }
            else if (brushType.name == "icons8-brush-64-yellow")
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = colors[1];
                Debug.Log("Set collided bullet color to = " + other.GetComponent<SpriteRenderer>().color);
            }
            else if (brushType.name == "icons8-brush-64-blue")
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = colors[2];
                Debug.Log("Set collided bullet color to = " + other.GetComponent<SpriteRenderer>().color);
            }
            else
            {
                Debug.Log("Error! Cannot not the matching paintBrush.");
            }
        }

        // PaintBrush already go out of the stage.
        if (other.CompareTag("BulletRecycler"))
        {
            gameObject.SetActive(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_BulletHell : MonoBehaviour
{

    // private Sprite ball;

    private float angle = 0f;  
    [SerializeField] private float angleStep = 20f;

    private Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue;
    private int colorIdx = 0;

    // private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        // Starting in 2 seconds, launched every 0.3 seconds
        float startTime = 5f;  
        float bulletGenerateInterval = 0.5f;
        InvokeRepeating("ShootPattern1", startTime, bulletGenerateInterval);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShootPattern1()
    {

        // Radian Angle = (Degree Angle) x pi/ 180
        float componentX = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
        float componentY = transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);
        // Debug.Log("x= " + componentX + ", y= " + componentY);
        
        Vector3 objMoveVector3 = new Vector3(componentX, componentY, 0f);
        Vector2 objDirection = (objMoveVector3 - transform.position).normalized;

        // Assign computation result to object
        GameObject obj = LV_ObjectPool3.poolInstance.GetObjectFromPool();
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation; 

        // Facing forward to move direction 
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, objDirection);   // Vector3.up = Vector3(0, 1, 0)

        // Give Rigidbody force
        // rb = obj.GetComponent<Rigidbody2D>();
        // rb.AddForce(new Vector2(25f * 9.8f , 25f * 9.8f)); 

        // Get actual bullet object
        obj.SetActive(true);
        obj.GetComponent<LV_BallBounce>().SetMoveDirection(objDirection);

        // Set color
        if (colorIdx == 0)
        {
            obj.GetComponent<SpriteRenderer>().color = colors[0];
        }
        else if (colorIdx == 1) 
        {
            obj.GetComponent<SpriteRenderer>().color = colors[1];
        }
        else if (colorIdx == 2)  
        {
            obj.GetComponent<SpriteRenderer>().color = colors[2];
        }        
        else
        {
            Debug.Log("Error! Something wrong with colorIdx");
        }

        // Increment Angle
        angle += angleStep;
        if (angle >= 180f)
        {
            angle = 0f;
            //change color after a loop
            colorIdx++; 
            if (colorIdx >= 3)
            {
                colorIdx = 0;
            }
        }

    }
}

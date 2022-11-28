using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_BulletHell_Endless : MonoBehaviour
{

    // private Sprite ball;

    private float angle = 0f;  
    [SerializeField] private float angleStep = 20f;
    [SerializeField] private int pattern1_or_2 = 1;
    [SerializeField] private float bulletGenerateInterval = 0.5f;
 

    private Color[] colors = { new Color32(220,38,127,255), new Color32(255,176,0,255), new Color32(100,143,255,255)};   // Red, Yellow, Blue;
    private int colorIdx = 0;

    // private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        // Starting in 2 seconds, launched every 0.3 seconds
        float startTime = 5f;  
        if (pattern1_or_2 == 1)
        {
            InvokeRepeating("ShootPattern1", startTime, bulletGenerateInterval);
        }
        else 
        {
            InvokeRepeating("ShootPattern2", startTime, bulletGenerateInterval); 
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShootPattern1()
    {
        // Unity Angle definition = clockwised
        // Radian Angle = (Degree Angle) x pi/ 180
        float componentX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f); 
        float componentY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
        // Debug.Log("x= " + componentX + ", y= " + componentY);
        

        Vector3 objMoveVector3 = new Vector3(componentX, componentY, 0f);
        Vector2 objDirection = (objMoveVector3 - transform.position).normalized;    // direction = destination - source

        // Assign computation result to object
        GameObject obj = LV_ObjectPool3.poolInstance.GetObjectFromPool();   // Pool3 for BallBounce, Pool2 for paint Brush
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation; 

        // Facing forward to move direction 
        obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, objDirection);   // Vector3.up = Vector3(0, 1, 0)

        // Get actual bullet object
        obj.SetActive(true);

        //*********  Endless  ***********
        obj.GetComponent<LV_BallBounce_Endless>().SetMoveDirection(objDirection); 
        //*********  Endless  ***********      

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

    private void ShootPattern2()
    {
        int shootAmount = (int)(360f / angleStep);
        for (int i = 0; i < shootAmount; i++)
        {
            // Radian Angle = (Degree Angle) x pi/ 180
            float componentX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f); // Unity angle is clockwised to 12.
            float componentY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 objMoveVector3 = new Vector3(componentX, componentY, 0f);
            Vector2 objDirection = (objMoveVector3 - transform.position).normalized;

            // Assign computation result to object
            GameObject obj = LV_ObjectPool3.poolInstance.GetObjectFromPool();  // Pool3 for BallBounce, Pool2 for paint Brush 
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation; 

            // Get actual bullet object
            obj.SetActive(true);

            //*********  Endless  ***********
            obj.GetComponent<LV_BallBounce_Endless>().SetMoveDirection(objDirection); 
            //*********  Endless  ***********  

            // Set color
            obj.GetComponent<SpriteRenderer>().color = colors[colorIdx];
            angle += angleStep;
        }

        colorIdx++; 
        if (colorIdx >= 3)
        {
            colorIdx = 0;
        }

    }
}

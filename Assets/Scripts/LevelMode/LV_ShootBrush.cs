using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_ShootBrush : MonoBehaviour
{
     [Header("For Square shape skill")]
    [SerializeField] private int paintBrushAmount = 12;

    private Vector2 objMoveDirection;
    [SerializeField] float repeatRate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Invokes the method in time seconds, then repeatedly every repeatRate seconds.
        InvokeRepeating("ShootBrush", 0f, repeatRate);    
    }

    private void ShootBrush()
    {
        float startAngle = 0f, endAngle = 360f;
        float angleStep = (endAngle - startAngle) / paintBrushAmount;    // spread in range
        float angle = startAngle;  

        for (int i = 0; i < paintBrushAmount; i++)
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

            obj.SetActive(true);
            obj.GetComponent<LV_Brush>().SetMoveDirection(objDirection);

            angle += angleStep;
        }
    }

}

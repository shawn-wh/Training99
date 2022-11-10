using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPropController : PropPrototype
{
    public float ShootInterval;
    
    private float nextShotTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        nextShotTime = Time.time + ShootInterval;
        transform.position = owner.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, target.transform.position - transform.position);
        
        if (nextShotTime < Time.time)
        {
            nextShotTime = Time.time + ShootInterval;
        }
        
    }
}

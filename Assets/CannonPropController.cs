using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPropController : PropPrototype
{
    [SerializeField] private float shootInterval;
    [SerializeField] private float bulletSpeed;
    
    private float nextShotTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        nextShotTime = Time.time + shootInterval;
        transform.position = owner.transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, target.transform.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, target.transform.position - transform.position);
        
        if (nextShotTime < Time.time)
        {
            VersusBullet bullet = manager.CreateBullet(transform.position);
            bullet.Color = owner.Color;
            bullet.TargetPlayer = target;
            bullet.Speed = bulletSpeed;
            nextShotTime = Time.time + shootInterval;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBulletPropController : PropPrototype
{
    // [SerializeField] private RandomBullet bulletTemplate;
    
    void Start()
    {
        Transform root = manager.bulletNode.gameObject.transform;
        foreach (Transform bulletTransform in root)
        {
            VersusBullet bullet = bulletTransform.GetComponent<VersusBullet>();
            if (bullet.Color == owner.OrignalColor)
            {
                bullet.IsRandomMoving = true;
            }
        }
        Destroy(gameObject, 5f);
    }

    void OnDestroy()
    {
        Transform root = manager.bulletNode.gameObject.transform;
        foreach (Transform bulletTransform in root)
        {
            VersusBullet bullet = bulletTransform.GetComponent<VersusBullet>();
            if (bullet.Color == owner.OrignalColor)
            {
                bullet.IsRandomMoving = false;
            }
        }
    }
}

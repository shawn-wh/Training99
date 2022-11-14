using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBulletPropController : PropPrototype
{   
    void Start()
    {
        Transform root = manager.bulletNode.gameObject.transform;
        foreach (Transform bulletTransform in root)
        {
            VersusBullet bullet = bulletTransform.GetComponent<VersusBullet>();
            if (bullet.TargetPlayer == target)
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
            if (bullet.TargetPlayer == target)
            {
                bullet.IsRandomMoving = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChangeColorPropController : PropPrototype
{
    // Start is called before the first frame update
    void Start()
    {
        Transform root = manager.bulletNode.gameObject.transform;
        foreach (Transform bulletTransform in root)
        {
            bulletTransform.GetComponent<VersusBullet>().Color = owner.OrignalColor;
        }
    }
}

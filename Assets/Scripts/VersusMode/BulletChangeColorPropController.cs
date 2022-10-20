using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChangeColorPropController : PropPrototype
{
    // Start is called before the first frame update
    void Start()
    {
        Color color = owner.gameObject.GetComponent<SpriteRenderer>().color;
        Transform root = manager.bulletNode.gameObject.transform;
        foreach (Transform bulletTransform in root)
        {
            bulletTransform.GetComponent<VersusBullet>().SetColor(color);
        }
    }
}

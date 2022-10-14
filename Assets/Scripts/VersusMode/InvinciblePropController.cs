using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvinciblePropController : PropPrototype
{
    void Start()
    {
        owner.isInvincible = true;
        
        // change owner's color to transparent
        Color color = owner.GetComponent<SpriteRenderer>().color;
        owner.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.5f);
        
        Destroy(gameObject, 3.0f);
    }
    
    void OnDestroy()
    {
        owner.RemoveProp();
        owner.isInvincible = false;
        Color color = owner.GetComponent<SpriteRenderer>().color;
        owner.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1.0f);
    }
}

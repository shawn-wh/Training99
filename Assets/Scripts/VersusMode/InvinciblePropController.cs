using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvinciblePropController : PropPrototype
{
    void Start()
    {
        owner.isInvincible = true;
        
        // change owner's color to half-transparent
        owner.Color = new Color(owner.OrignalColor.r, owner.OrignalColor.g, owner.OrignalColor.b, 0.5f);
        Destroy(gameObject, 3.0f);
    }
    
    void OnDestroy()
    {
        owner.RemoveProp();
        owner.isInvincible = false;
        owner.Color = new Color(owner.OrignalColor.r, owner.OrignalColor.g, owner.OrignalColor.b, 1.0f);
    }
}

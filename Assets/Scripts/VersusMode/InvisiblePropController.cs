using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisiblePropController : PropPrototype
{
    void Start()
    {
        Color color = owner.GetComponent<SpriteRenderer>().color;
        owner.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.5f);
        owner.m_collider.enabled = false;
        Destroy(gameObject, 3.0f);
    }
    
    void OnDestroy()
    {
        Color color = owner.GetComponent<SpriteRenderer>().color;
        owner.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1.0f);
        owner.m_collider.enabled = true;
    }
}

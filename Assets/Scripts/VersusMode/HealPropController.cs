using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPropController : PropPrototype
{
    // Start is called before the first frame update
    void Start()
    {
        print("heal!");
        owner.Heal(1);
    }
}

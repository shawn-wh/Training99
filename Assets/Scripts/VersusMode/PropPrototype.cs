using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropPrototype : MonoBehaviour
{
    [SerializeField]
    protected VersusPlayer owner;

    public void Awake()
    {
        enabled = false;
    }
    
    public void AssignAndEnable(VersusPlayer owner)
    {
        enabled = true;
        this.owner = owner;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropPrototype : MonoBehaviour
{
    public VersusPlayer owner { get; set; }

    public void Awake()
    {
        enabled = false;
    }
}
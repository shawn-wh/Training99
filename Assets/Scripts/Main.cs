using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player(100, 0, "Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public float speed = 5f;

    int health;
    int power;
    string name;

    public Player() { }

    public Player(int health, int power, string name)
    {
        this.health = health;
        this.power = power;
        this.name = name;
    }

    public void Info()
    {
        Debug.Log("Health is: " + health);
        Debug.Log("Power is: " + power);
        Debug.Log("Name is: " + name);
    }
} // class

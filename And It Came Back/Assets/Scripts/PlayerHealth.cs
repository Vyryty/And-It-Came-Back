using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth 
{
    private int health;

    public PlayerHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
    }
}

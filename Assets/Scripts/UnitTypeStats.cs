using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTypeStats 
{
    private int health, damge;
    private float damageDelay, range, speed;

    public UnitTypeStats()
    {
      
    }

    public UnitTypeStats(int health, int damge, float damageDelay, float range, float speed)
    {
        this.health = health;
        this.damge = damge;
        this.damageDelay= damageDelay;
        this.range = range;
        this.speed = speed;
    }

   
    public float Range { get => range; set => range = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Health { get => health; set => health = value; }
    public int Damge { get => damge; set => damge = value; }
    public float DamageDelay { get => damageDelay; set => damageDelay = value; }
 
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTypeStats
{
    private int health, minDamage, maxDamage, range, speed;
    private float damageDelay;

    public UnitTypeStats()
    {

    }

    public UnitTypeStats(int health, int minDamge, int maxDamage, float damageDelay, int range, int speed)
    {
        this.health = health;
        this.minDamage = minDamge;
        this.maxDamage = maxDamage;
        this.damageDelay = damageDelay;
        this.range = range;
        this.speed = speed;
    }
    public int getHealth()
    {
        return health;
    }
    public void setHealth(int health)
    {
        this.health = health;
    }
    public int getMinDamage()
    {
        return minDamage;
    }
    public void setMinDamage(int minDamage)
    {
        this.minDamage = minDamage;
    }
    public int getMaxDamage()
    {
        return maxDamage;
    }
    public void setMaxDamage(int maxDamage)
    {
        this.maxDamage = maxDamage;
    }
    public float getDamageDelay()
    {
        return damageDelay;
    }
    public void setDamageDelay(int damageDelay)
    {
        this.damageDelay = damageDelay;
    }
    public int getRange()
    {
        return range;
    }
    public void setRange(int range)
    {
        this.range = range;
    }
    public int getSpeed()
    {
        return speed;
    }
    public void setSpeed(int speed)
    {
        this.speed = speed;
    }
}

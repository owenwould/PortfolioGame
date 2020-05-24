using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    int enemiesKilled, goldSpent,playerUnitsSpawned;
 

    public void beginGame()
    {
        enemiesKilled = 0;
        goldSpent = 0;
        playerUnitsSpawned = 0;
    }


    public void increaseKillCount()
    {
        enemiesKilled++;
    }
    public void increaseGoldSpendure(int gold)
    {
        goldSpent += gold;
    }
    public void increaseSpawnCount()
    {
        playerUnitsSpawned++;
    }


    public int getEnemiesKilled()
    {
        return enemiesKilled;
    }
    public int getGoldSpent()
    {
        return goldSpent;
    }
    public int getPlayerUnitSpawned()
    {
        return playerUnitsSpawned;
    }



    
}

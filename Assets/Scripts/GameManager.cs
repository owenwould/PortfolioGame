using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]List<Unit> playerArmy;
    [SerializeField]List<Unit> enemyArmy;
    [SerializeField] UIManager uiManager;
    private int playerGold, enemyGold,unitCountPlayer,unitCountEnemy;

    void Start()
    {
        playerArmy = new List<Unit>(10);
        enemyArmy = new List<Unit>(10);
        playerGold = 0;
        enemyGold = 0;
        unitCountPlayer = 0;
        unitCountEnemy = 0;
    }

  

    public void addToPlayerArmy(Unit unit, bool isPlayer)
    {
        if (isPlayer)
            playerArmy.Add(unit);
        else
            enemyArmy.Add(unit);
   }
   public void IncreaseUnitCount(bool isPlayer)
   {
        if (isPlayer)
        {
            unitCountPlayer++;
            updateUnitCountUI(unitCountPlayer);
        }
        else
            unitCountEnemy++;
    }
    

   
    

    public void removeUnit(bool isPlayersUnit,int iEntityType)
    {
        if (isPlayersUnit)
        {
            //Enemy killed player unit
            unitCountPlayer--;
            int reward = enemyDeathReward(iEntityType);
            playerArmy.RemoveAt(0);
            updateUnitCountUI(unitCountPlayer);
            increaseGold(reward, false);
            
            
        }
        else
        {
            unitCountEnemy--;
            enemyArmy.RemoveAt(0);
            int reward = enemyDeathReward(iEntityType);
            increaseGold(reward,true);
        }
            
    }

    public void attackUnit(int damage, bool isPlayersUnit)
    {
        if (isPlayersUnit)
            enemyArmy[0].takeDamage(damage);
        else
            playerArmy[0].takeDamage(damage);
    }

    public int returnPlayerUnitCount()
    {
        return unitCountPlayer;
    }
    public int returnEnemyUnitCount()
    {
        return unitCountEnemy;
    }
    public int returnPlayerGold()
    {
        return playerGold;
    }
    public int returnEnemyGold()
    {
        return enemyGold;
    }


    public void increaseGold(int value,bool isPlayer)
    {
        if (isPlayer)
        {
            playerGold += value;
            updateGoldCountUI(playerGold);
        }
        else
        {
            enemyGold += value;
        }
        
    }
    public void decreaseGold(int value,bool isPlayer)
    {
        if (isPlayer)
        {
            playerGold -= value;
            updateGoldCountUI(playerGold);
        }
        else
        {
            enemyGold -= value;
        }
      
    }
   
    void updateUnitCountUI(int size)
    {
        uiManager.setUnitCount(size);
    }
    void updateGoldCountUI(int goldValue)
    {
        uiManager.setGoldValue(goldValue);
    }

    int enemyDeathReward(int iType)
    {
        int reward = 0;
        switch(iType)
        {
            case constants.LIGHT_UNIT_TYPE:
                reward = constants.LIGHT_UNIT_COST;
                break;
            case constants.RANGED_UNIT_TYPE:
                reward = constants.RANGE_UNIT_COST;
                break;
            case constants.MEDIUM_UNIT_TYPE:
                reward = constants.MEDIUM_UNIT_COST;
                break;
            case constants.HEAVY_UNIT_TYPE:
                reward = constants.HEAVY_UNIT_COST;
                break;
        }
        return reward;

    }

   

    
}

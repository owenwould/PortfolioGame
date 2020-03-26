using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]List<Unit> playerArmy;
    [SerializeField]List<Unit> enemyArmy;
    [SerializeField] UIManager uiManager;
    private int playerGold, enemyGold,unitCount;

    void Start()
    {
        playerArmy = new List<Unit>(10);
        enemyArmy = new List<Unit>(10);
        playerGold = 0;
        unitCount = 0;
    }

    private void Update()
    {
      
    }

    public void addToPlayerArmy(Unit unit)
   {
        playerArmy.Add(unit);
        //Note ui will be updated through spawn not here in this instance
   }
   public void IncreaseUnitCount()
   {
        unitCount++;
        updateUnitCountUI(unitCount);
    }
    

    void addToEnemyArmy(Unit unit)
    {
        enemyArmy.Add(unit);
    }
    

    public void removeUnit(bool isPlayersUnit,int iEntityType)
    {
        if (isPlayersUnit)
        {
            //Enemy killed player
            int reward = enemyDeathReward(iEntityType);
            playerArmy.RemoveAt(0);
            updateUnitCountUI(playerArmy.Count);
            enemyGold += reward;
            
        }
        else
        {
            enemyArmy.RemoveAt(0);
            int reward = enemyDeathReward(iEntityType);
            increasePlayerGold(reward);
         
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
        return unitCount;
    }
    public int returnEnemyUnitCount()
    {
        return enemyArmy.Count;
    }
    public int returnPlayerGold()
    {
        return playerGold;
    }

    public void increasePlayerGold(int value)
    {
        playerGold += value;
        updateGoldCountUI(playerGold);
    }
    public void decreasePlayerGold(int value)
    {
        playerGold -= value;
        updateGoldCountUI(playerGold);
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

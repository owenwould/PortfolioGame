using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Unit> playerArmy, enemyArmy;
    List<GameObject> playerArmyObj, enemyArmyObj;
    [SerializeField]UIManager uiManager;
    [SerializeField] Base playerBase, enemyBase;
    [SerializeField] GoldGenerator goldGenerator;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Upgrades upgradeScript;
    private int playerGold, enemyGold,unitCountPlayer,unitCountEnemy;
    public static bool gameover = false,mainMenuMode = true;

    void Start()
    {
        playerArmy = new List<Unit>(10);
        enemyArmy = new List<Unit>(10);
        playerArmyObj = new List<GameObject>(10);
        enemyArmyObj = new List<GameObject>(10);
       
    }

    private void setInitialValues()
    {
        playerGold = constants.startGoldPlayerEasy;
        enemyGold = 1200;
        unitCountPlayer = 0;
        unitCountEnemy = 0;
    }

    public void beginGame()
    {
        gameover = false;
        clearArmies();
        mainMenuMode = false;
        setInitialValues();
        uiManager.beginGame(playerGold);
        goldGenerator.startGoldGen();
        upgradeScript.begin();
        playerBase.startGame();
        enemyBase.startGame();
        enemyManager.begin();
    } 
    private void clearArmies()
    {
        playerArmy.Clear();
        enemyArmy.Clear();

        foreach (GameObject obj in playerArmyObj)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in enemyArmyObj)
        {
            Destroy(obj);
        }

        playerArmyObj.Clear();
        enemyArmyObj.Clear();

    }

  

    public void addToPlayerArmy(Unit unit, bool isPlayer,GameObject obj)
    {
        if (isPlayer)
        {
            playerArmy.Add(unit);
            playerArmyObj.Add(obj);
        }
        else
        {
            enemyArmy.Add(unit);
            enemyArmyObj.Add(obj);
        }
            
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
            playerArmyObj.RemoveAt(0);
            updateUnitCountUI(unitCountPlayer);
            increaseGold(reward, false);
            
            
        }
        else
        {
            unitCountEnemy--;
            enemyArmy.RemoveAt(0);
            enemyArmyObj.RemoveAt(0);
            int reward = enemyDeathReward(iEntityType);
           
            increaseGold(reward,true);
        }
            
    }

    public void attackUnit(int damage, bool isPlayersUnit,int currentType)
    {
        int fullDamage;

        if (isPlayersUnit)
        {
            if (enemyArmy.Count > 0)
            {
                fullDamage = damage + checkForCounter(currentType, enemyArmy[0].getUnitType());
                enemyArmy[0].takeDamage(fullDamage);
            }
               
        }
        else
        {
            if (playerArmy.Count > 0)
            {
                fullDamage = damage + checkForCounter(currentType, playerArmy[0].getUnitType());
                playerArmy[0].takeDamage(fullDamage);
            }
              
        }
           
    }

    private int checkForCounter(int damagingUnitType, int recievingUnitType) 
    {
        switch (damagingUnitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                if (recievingUnitType == constants.RANGED_UNIT_TYPE)
                    return criticalHit();
                break;
            case constants.MEDIUM_UNIT_TYPE:
                if (recievingUnitType == constants.HEAVY_UNIT_TYPE)
                    return criticalHit();
                break;
            case constants.RANGED_UNIT_TYPE:
                if (recievingUnitType == constants.MEDIUM_UNIT_TYPE)
                    return criticalHit();
                break;
            case constants.HEAVY_UNIT_TYPE:
                if (recievingUnitType == constants.LIGHT_UNIT_TYPE)
                    return criticalHit();
                break;
        }
        return 0;
    }

    private int criticalHit()
    {
        int AdditionalDamage = Random.Range(0, 11);
        return AdditionalDamage;
    }


    public int getPlayerUnitCount()
    {
        return unitCountPlayer;
    }
    public int getEnemyUnitCount()
    {
        return unitCountEnemy;
    }
    public int getPlayerGold()
    {
        return playerGold;
    }
    public int getEnemyGold()
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

    public void attackBase(bool isPlayer,int damage)
    {
        int baseHealth; 
        if (isPlayer)
           baseHealth = enemyBase.reduceHealth(damage);
        else
           baseHealth = playerBase.reduceHealth(damage);

        if (baseHealth < 1)
            gameoverState(isPlayer);
           
    }

    private void gameoverState(bool isPlayer)
    {
        enemyManager.StopAllCoroutines();

        foreach (Unit unit in enemyArmy)
        {
            unit.setIdle();
        }

        foreach (Unit unit in playerArmy)
        {
            unit.setIdle();
        }
        goldGenerator.stopGoldGen();
        gameover = true;
        uiManager.gameover(isPlayer);

    }

    public float returnLinearPosition()
    {
        return enemyArmy[0].getXPosition();
    }

    public bool returnIsAttacking()
    {
        return enemyArmy[0].getIsAttacking();
    }

   

   

    
}

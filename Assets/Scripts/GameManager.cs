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
        playerGold = 1200;
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
        goldGenerator.stopGoldGen();
        gameover = true;
        uiManager.gameover(isPlayer);

    }

   

   

    
}

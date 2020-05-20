﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    
    [SerializeField] GameObject obj;
    [SerializeField] Transform playerSpawnPointTran, enemySpawnPointTran;
    [SerializeField] GameManager gameManager;
    [SerializeField] Upgrades upgradesScript; 
    Vector3 playerSpawnPostion, enemySpawnPosition;
    List<int> playerEntityQueue, enemyEntityQueue;
    static bool bPlayerCoroutineRunning,bEnemyCorountineRunning;
    [SerializeField] UIManager uiManager;
    void Start()
    {
        
        playerSpawnPostion = playerSpawnPointTran.position;
        enemySpawnPosition = enemySpawnPointTran.position;
        bPlayerCoroutineRunning = false;
        bEnemyCorountineRunning = false;
        playerEntityQueue = new List<int>(constants.MAX_UNIT_COUNT);
        enemyEntityQueue = new List<int>(constants.MAX_UNIT_COUNT);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Alpha1))
            checkSpawnEntity(constants.LIGHT_UNIT_TYPE,true);
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            checkSpawnEntity(constants.RANGED_UNIT_TYPE, true);
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            checkSpawnEntity(constants.MEDIUM_UNIT_TYPE, true);
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            checkSpawnEntity(constants.HEAVY_UNIT_TYPE, true);
    }

    public void spawnUnit(int unitType,bool isPlayer)
    {
        checkSpawnEntity(unitType, isPlayer);
    }
    public void GameOver()
    {
        if (GameManager.gameover)
        {
            StopAllCoroutines();
            playerEntityQueue.Clear();
            enemyEntityQueue.Clear();
        }
      
    }


    void checkSpawnEntity(int iEntityType, bool isPlayer)
    {
        int iUnitCount = 0;
        if (isPlayer)
            iUnitCount = gameManager.getPlayerUnitCount();
        else
            iUnitCount = gameManager.getEnemyUnitCount();

        if (iUnitCount < constants.MAX_UNIT_COUNT)
        {
            
            if (checkPlayerGold(iEntityType,isPlayer))
            {
                gameManager.IncreaseUnitCount(isPlayer);

                if (isPlayer)
                {
                    uiManager.setTimer(iEntityType);
                    playerEntityQueue.Add(iEntityType);
                    if (!bPlayerCoroutineRunning)
                    {
                        bPlayerCoroutineRunning = true;
                        StartCoroutine(PlayerQueueCoroutine());
                    }
                }
                else
                {
                    enemyEntityQueue.Add(iEntityType);
                    if (!bEnemyCorountineRunning)
                    {
                        bEnemyCorountineRunning = true;
                        StartCoroutine(EnemyQueueCoroutine());
                    }
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

    }

    


    

    void spawnPlayerEntity(int iEntityType,bool isPlayer)
    {
        Vector3 vec;
        float targetPos;
        if (isPlayer)
        {
            vec = playerSpawnPostion;
            targetPos = enemySpawnPosition.x;
        }
        else
        {
            vec = enemySpawnPosition;
            targetPos = playerSpawnPostion.x;
        }
        GameObject entity = Instantiate(obj, vec, Quaternion.identity);
        Unit unitScript = entity.AddComponent<Unit>();
        unitScript.setUnit(targetPos, isPlayer,iEntityType);

        UnitTypeStats unitStats = upgradesScript.unitValues(iEntityType,isPlayer);
        unitScript.setUnitAttributes(unitStats.getHealth(), unitStats.getMinDamage(),unitStats.getMaxDamage(), unitStats.getDamageDelay(), unitStats.getRange(), unitStats.getSpeed());
        gameManager.addToPlayerArmy(unitScript,isPlayer,entity);
    }
    IEnumerator PlayerQueueCoroutine()
    {

       
        float fWaitDuration = returnWaitTime(playerEntityQueue);
        yield return new WaitForSeconds(fWaitDuration);
        spawnPlayerEntity(playerEntityQueue[0],true);
        playerEntityQueue.RemoveAt(0);
      
        if (playerEntityQueue.Count > 0)
            StartCoroutine(PlayerQueueCoroutine());
        else
            bPlayerCoroutineRunning = false;

    }
    IEnumerator EnemyQueueCoroutine()
    {
        float fWaitDuration = returnWaitTime(enemyEntityQueue);
        yield return new WaitForSeconds(fWaitDuration);
        spawnPlayerEntity(enemyEntityQueue[0], false);
        enemyEntityQueue.RemoveAt(0);

        if (enemyEntityQueue.Count > 0)
            StartCoroutine(EnemyQueueCoroutine());
        else
            bEnemyCorountineRunning = false;
    }

    float returnWaitTime(List<int> list)
    {
        float fWaitDuration = 0;
        switch (list[0])
        {
            case constants.LIGHT_UNIT_TYPE:
                fWaitDuration = constants.LIGHT_UNIT_WAIT;
                break;
            case constants.RANGED_UNIT_TYPE:
                fWaitDuration = constants.RANGE_UNIT_WAIT;
                break;
            case constants.MEDIUM_UNIT_TYPE:
                fWaitDuration = constants.MEDIUM_UNIT_WAIT;
                break;
            case constants.HEAVY_UNIT_TYPE:
                fWaitDuration = constants.HEAVY_UNIT_WAIT;
                break;
        }

        return fWaitDuration;
    }



    bool checkPlayerGold(int iType, bool isPlayer)
    {
        int gold = 0;
        if (isPlayer)
            gold = gameManager.getPlayerGold();
        else
            gold = gameManager.getEnemyGold();

        bool canAfford = false;
        switch (iType)
        {
            case constants.LIGHT_UNIT_TYPE:
                if (gold >= constants.LIGHT_UNIT_COST) { 
                    gameManager.decreaseGold(constants.LIGHT_UNIT_COST,isPlayer);
                    canAfford = true;
                }
                break;
            case constants.RANGED_UNIT_TYPE:
                if (gold >= constants.RANGE_UNIT_COST) {
                    gameManager.decreaseGold(constants.RANGE_UNIT_COST,isPlayer);
                    canAfford = true;
                }
                break;
            case constants.MEDIUM_UNIT_TYPE:
                if (gold >= constants.MEDIUM_UNIT_COST) {
                    gameManager.decreaseGold(constants.MEDIUM_UNIT_COST,isPlayer);
                    canAfford = true;
                }
                break;
            case constants.HEAVY_UNIT_TYPE:
                if (gold >= constants.HEAVY_UNIT_COST) {
                    gameManager.decreaseGold(constants.HEAVY_UNIT_COST,isPlayer);
                    canAfford = true;
                }
                break;
        }
        return canAfford;
    }


    

    

    
}

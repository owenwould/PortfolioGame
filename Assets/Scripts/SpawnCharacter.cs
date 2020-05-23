using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    
    [SerializeField] GameObject[] unitObjs;
    [SerializeField] Transform playerSpawnPointTran, enemySpawnPointTran;
    [SerializeField] GameManager gameManager;
    [SerializeField] Upgrades upgradesScript; 

    List<int> playerEntityQueue, enemyEntityQueue;
    static bool bPlayerCoroutineRunning,bEnemyCorountineRunning;
    [SerializeField] UIManager uiManager;
    long unitIDPlayer, unitIDEnemy;

    void Start()
    {
       
        bPlayerCoroutineRunning = false;
        bEnemyCorountineRunning = false;
        playerEntityQueue = new List<int>(constants.MAX_UNIT_COUNT);
        enemyEntityQueue = new List<int>(constants.MAX_UNIT_COUNT);

        unitIDEnemy = 1000;
        unitIDPlayer = 2000;


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


        if (Input.GetKeyUp(KeyCode.B))
        {
            checkSpawnEntity(constants.LIGHT_UNIT_TYPE, false);
        }
        else if (Input.GetKeyUp(KeyCode.C)) {
            checkSpawnEntity(constants.RANGED_UNIT_TYPE, false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            checkSpawnEntity(constants.MEDIUM_UNIT_TYPE, false);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            checkSpawnEntity(constants.HEAVY_UNIT_TYPE, false);
        }

    }

    public void spawnUnit(int unitType,bool isPlayer)
    {
        checkSpawnEntity(unitType, isPlayer);
    }
    public void GameOver()
    {
        unitIDEnemy = 1000;
        unitIDPlayer = 2000;

        StopAllCoroutines();
        playerEntityQueue.Clear();
        enemyEntityQueue.Clear();


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
     
        Vector3 pos;
        float targetPos;
        if (isPlayer)
        {
       
            pos = playerSpawnPointTran.position;
            targetPos = enemySpawnPointTran.position.x;
        }
        else 
        {
            pos= enemySpawnPointTran.position ;
            targetPos = playerSpawnPointTran.position.x;
        }
       
        int unitIndex = getUnitIndex(iEntityType);
        GameObject entity = Instantiate(unitObjs[unitIndex], pos, Quaternion.identity);
        Unit unitScript = entity.AddComponent<Unit>();
        UnitTypeStats unitTypeStats = upgradesScript.returnUnitStats(isPlayer, iEntityType);
        unitScript.setUnitAttributes(unitTypeStats.getHealth(), unitTypeStats.getMinDamage(), unitTypeStats.getMaxDamage(), unitTypeStats.getDamageDelay(), unitTypeStats.getRange(), unitTypeStats.getSpeed());
        unitScript.setUnit(targetPos, isPlayer,iEntityType,getID(isPlayer));
        gameManager.addToPlayerArmy(unitScript,isPlayer,entity);
    }
    IEnumerator PlayerQueueCoroutine()
    {
        float fWaitDuration = returnWaitTime(playerEntityQueue[0]);
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
        float fWaitDuration = returnWaitTime(enemyEntityQueue[0]);
        yield return new WaitForSeconds(fWaitDuration);
        spawnPlayerEntity(enemyEntityQueue[0], false);
        enemyEntityQueue.RemoveAt(0);

        if (enemyEntityQueue.Count > 0)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(EnemyQueueCoroutine());
        }
        else
            bEnemyCorountineRunning = false;
    }

    float returnWaitTime(int iEntityType)
    {
        float fWaitDuration = 0;
        switch (iEntityType)
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

            default:
                print("Error");
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
            default:
                print("Error");
                    break;
        }
        return canAfford;
    }

    private int getUnitIndex(int unitType)
    {
        string unitTag = "";
        switch (unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                unitTag = constants.lightTag;
                break;
            case constants.RANGED_UNIT_TYPE:
                unitTag = constants.rangeTag;
                break;
            case constants.MEDIUM_UNIT_TYPE:
                unitTag = constants.mediumTag;
                break;
            case constants.HEAVY_UNIT_TYPE:
                unitTag = constants.heavyTag;
                break;
            default:
                print("Error");
                break;
        }
        for (int i=0; i < unitObjs.Length; i++)
        {
            if (unitObjs[i].CompareTag(unitTag))
                return i;
        }

      

        return 0;
    }


    public long getID(bool isPlayer)
    {
        if (isPlayer)
        {
            unitIDPlayer++;
            return unitIDPlayer;
        }
        else
        {
            unitIDEnemy++;
            return unitIDEnemy;
        }
           
    }


    

    

    
}

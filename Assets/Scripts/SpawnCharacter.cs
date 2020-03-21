using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] GameObject enemyObj;
    [SerializeField] GameObject obj;
    [SerializeField] Transform playerSpawnPointTran, enemySpawnPointTran;
    [SerializeField] GameManager gameManager;
    Vector3 playerSpawnPostion, enemySpawnPosition;
    List<int> playerEntityQueue, enemyEntityQueue;
    static bool bPlayerCoroutineRunning;
    void Start()
    {
        playerSpawnPostion = playerSpawnPointTran.position;
        enemySpawnPosition = enemySpawnPointTran.position;
        bPlayerCoroutineRunning = false;
        playerEntityQueue = new List<int>(10);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            checkSpawnEntity(1);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            //spawnEntity(3);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
           
        }
    }
    void checkSpawnEntity(int iEntityType)
    {
        int iUnitCount = gameManager.returnPlayerUnitCount();
        if (iUnitCount < constants.MAX_UNIT_COUNT)
        {
            if (checkPlayerGold(iEntityType))
            {
                playerEntityQueue.Add(iEntityType);
                gameManager.IncreaseUnitCount();
                if (!bPlayerCoroutineRunning)
                {
                    bPlayerCoroutineRunning = true;
                    StartCoroutine(playerQueueCoroutine());
                }
            }
            else
            {
                //Can't Afford
            }
        }
        else
        {
            //Can't spawn
        }

    }


    

    void spawnPlayerEntity(int iEntityType)
    {
        GameObject entity = Instantiate(obj, playerSpawnPostion, Quaternion.identity);
        Unit unitScript = entity.AddComponent<Unit>();
        unitScript.setUnit(enemySpawnPosition.x, true,iEntityType);
        gameManager.addToPlayerArmy(unitScript);
    }
    IEnumerator playerQueueCoroutine()
    {
        float fWaitDuration =0;
        switch (playerEntityQueue[0])
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
        yield return new WaitForSeconds(fWaitDuration);

        spawnPlayerEntity(playerEntityQueue[0]);
        playerEntityQueue.RemoveAt(0);
        

        if (playerEntityQueue.Count > 0)
            StartCoroutine(playerQueueCoroutine());
        else
            bPlayerCoroutineRunning = false;

    }

    bool checkPlayerGold(int iType)
    {
        int gold = gameManager.returnPlayerGold();
        bool canAfford = false;
        switch (iType)
        {
            case constants.LIGHT_UNIT_TYPE:
                if (gold >= constants.LIGHT_UNIT_COST) { 
                    gameManager.decreasePlayerGold(constants.LIGHT_UNIT_COST);
                    canAfford = true;
                }
                break;
            case constants.RANGED_UNIT_TYPE:
                if (gold >= constants.RANGE_UNIT_COST) {
                    gameManager.decreasePlayerGold(constants.RANGE_UNIT_COST);
                    canAfford = true;
                }
                break;
            case constants.MEDIUM_UNIT_TYPE:
                if (gold >= constants.MEDIUM_UNIT_COST) {
                    gameManager.decreasePlayerGold(constants.MEDIUM_UNIT_COST);
                    canAfford = true;
                }
                break;
            case constants.HEAVY_UNIT_TYPE:
                if (gold >= constants.HEAVY_UNIT_COST) {
                    gameManager.decreasePlayerGold(constants.HEAVY_UNIT_COST);
                    canAfford = true;
                }
                break;
        }
        return canAfford;
    }

    

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] GameObject enemyObj;
    [SerializeField] Transform leftSpawnPointTran, rightSpawnPointTran;
    [SerializeField] UIManager uiManager;
    Vector3 leftSpawnPostion, rightSpawnPosition;
    List<int> playerEntityQueue, enemyEntityQueue;
    static bool bPlayerCoroutineRunning;
    void Start()
    {
        leftSpawnPostion = leftSpawnPointTran.position;
        rightSpawnPosition = rightSpawnPointTran.position;
        bPlayerCoroutineRunning = false;
        playerEntityQueue = new List<int>(10);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            spawnEntity(1);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            spawnEntity(3);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            cheat();
        }
    }
    void spawnEntity(int iEntityType)
    {
        if (UIManager.unitCount < 10)
        {
            if (UIManager.goldCount >= UIManager.LIGHT_UNIT_COST)
            {
                playerEntityQueue.Add(iEntityType);
                uiManager.increaseUnitCount();
                uiManager.decreaseGoldValue(UIManager.LIGHT_UNIT_COST);
                if (!bPlayerCoroutineRunning)
                {
                    bPlayerCoroutineRunning = true;
                    StartCoroutine(playerQueueCoroutine());
                }
              
            }
            else
            {
                print("Not enough Gold");
            }
            
        } 
        
    }
    void cheat()
    {
        uiManager.increaseGoldValue(1000);
    }


    void x()
    {
        
        GameObject obj = Instantiate(enemyObj, rightSpawnPosition, Quaternion.identity);
    }


    IEnumerator playerQueueCoroutine()
    {
        float fWaitDuration =0;
        switch (playerEntityQueue[0])
        {
            case 1:
                print(1);
                fWaitDuration = 1;
                break;
            case 2:
                fWaitDuration = 2;
                break;
            case 3:
                print(3);
                fWaitDuration = 3;
                break;
        }
        yield return new WaitForSeconds(fWaitDuration);

        playerEntityQueue.RemoveAt(0);
        x();

        if (playerEntityQueue.Count > 0)
        {
            StartCoroutine(playerQueueCoroutine());
        }
        else { bPlayerCoroutineRunning = false; }

    }


    
}

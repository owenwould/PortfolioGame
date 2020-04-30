using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] GameManager manager;
    [SerializeField] SpawnCharacter spawner;
    int waitTime = 3;
    bool canSpawnAll;
    public bool enemyActive;

    private void Start()
    {
        canSpawnAll = false;
       
    }


    public void begin()
    {
        if (enemyActive)
        {
            canSpawnAll = false;
            StartCoroutine(timeCanSpawnAllUnits());
            StartCoroutine(aiReponseDelay());
        }
    }

    //Check money
    //Spawn
    //Repeat 

    private int returnGoldQuantity()
    {
        return manager.returnEnemyGold();
    }
    private int returnUnitCount()
    {
        return manager.returnEnemyUnitCount();
    }

    private bool canBuyUnit(int unitCount,int gold)
    {
      
        if (unitCount < constants.MAX_UNIT_COUNT && gold >= constants.LIGHT_UNIT_COST)
            return true;
        else
            return false;
    }
    


    private void takeAction()
    {
        print("X");
        int unitCount = returnUnitCount();
        int gold = returnGoldQuantity();

        if (!canBuyUnit(unitCount,gold))
            return;


       
        if (unitCount < 2)
            chooseUnit(gold);
        else
        {

        }
        
        /*
         * else check if upgrade is worth saving for or buy more units based on position
         * 
         */
    }

    private void chooseUnit(int gold)
    {
        int unitRange = 0;
        if (!canSpawnAll)
            unitRange = unitPoolRange(gold, false);
        else
            unitRange = unitPoolRange(gold, true);


        int unitType = returnUnitType(unitRange);

        spawner.spawnUnit(1, false);
        

    }

    IEnumerator aiReponseDelay()
    {
        yield return new WaitForSeconds(3);
        takeAction();
        StartCoroutine(aiReponseDelay());

    }

    private IEnumerator timeCanSpawnAllUnits()
    {
        yield return new WaitForSeconds(10);
        canSpawnAll = true;
        

    }

    private int returnUnitType(int unitRange)
    {
        int max = -1;
        switch (unitRange)
        {
            case 1:
                max = constants.LIGHT_RANDOM_MAX;
                break;
            case 2:
                max = constants.RANGED_RANDOM_MAX;
                break;
            case 3:
                max = constants.MEDIUM_RANDOM_MAX;
                break;
            case 4:
                max = constants.HEAVY_RANDOM_MAX;
                break;
        }

        return decideUnit(max);
    }







    int decideUnit(int max)
    {
        int random = Random.Range(0, max);
        int unitType = 1;

        if (random >= 0 && random < constants.LIGHT_RANDOM_MAX)
        {
            unitType = constants.LIGHT_UNIT_TYPE;
        }
        else if (random >= constants.LIGHT_RANDOM_MAX && random < constants.RANGED_RANDOM_MAX)
        {
            unitType = constants.RANGED_UNIT_TYPE;
        }
        else if (random >= constants.RANGED_RANDOM_MAX && random < constants.MEDIUM_UNIT_COST)
        {
            unitType = constants.MEDIUM_UNIT_TYPE;
        }
        else if (random >= constants.MEDIUM_RANDOM_MAX && random < constants.HEAVY_RANDOM_MAX)
        {
            unitType = constants.HEAVY_UNIT_TYPE;
        }
        return unitType;
    }


    int unitPoolRange(int gold, bool canSpawnAll)
    {
        int unitsAvailiableToSpawn = 0;
        if (gold >= constants.LIGHT_UNIT_COST && gold <= constants.RANGE_UNIT_COST)
        {
            unitsAvailiableToSpawn = 1;
        }
        else if (gold >= constants.RANGE_UNIT_COST && gold <= constants.MEDIUM_UNIT_COST)
        {
            unitsAvailiableToSpawn = 2;
        }
        else if (gold >= constants.MEDIUM_UNIT_COST && gold <= constants.HEAVY_UNIT_COST)
        {
            if (canSpawnAll)
                unitsAvailiableToSpawn = 3;
            else
                unitsAvailiableToSpawn = 2;
        }
        else if (gold >= constants.HEAVY_UNIT_COST)
        {
            if (canSpawnAll)
                unitsAvailiableToSpawn = 4;
            else
                unitsAvailiableToSpawn = 2;
        }

        return unitsAvailiableToSpawn;
    }










}

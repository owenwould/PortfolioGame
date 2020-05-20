using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] GameManager manager;
    [SerializeField] SpawnCharacter spawner;
    [SerializeField] Transform spawnTran;
    [SerializeField] Base playerBaseScript;
    [SerializeField] Upgrades upgradeScript;
    float spawnXPos;
    int waitTime = 3;
    bool canSpawnAll;
    public bool enemyActive;

    private void Start()
    {
        canSpawnAll = false;
        spawnXPos = spawnTran.position.x;
       
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
        return manager.getEnemyGold();
    }
    private int returnUnitCount()
    {
        return manager.getEnemyUnitCount();
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

        int unitCount = returnUnitCount();
        int gold = returnGoldQuantity();

        if (!canBuyUnit(unitCount, gold))
            return;

        if (unitCount < 3)
            chooseUnit(gold);
        else
        {
            makeEducatedDecision(gold);
        }
    }



    void makeEducatedDecision(int gold)
    {
        int distanceAdditive = considerDistance();
        int baseAdditive = considerPlayerHealth();
        finalDecision(baseAdditive, distanceAdditive, gold);
    }



    private int considerDistance()
    {
        int distance = returnDistance(manager.returnLinearPosition());
        bool isAttacking = returnIsAttacking();
       
        int action = 0;
        if (isAttacking)
        {
            if (distance > 0 && distance < 20)
                action = 40;
            else if (distance > 20 && distance < 40)
                action = 30;
            else if (distance > 40 && distance < 60)
                action = 20;
            else if (distance > 60)
                action = 30;
        }
        else
        {
            if (distance > 0 && distance < 20)
                action = 10;
            else if (distance > 20 && distance < 40)
                action = 20;
            else if (distance > 40 && distance < 60)
                action = 20;
            else if (distance > 60)
                action = 40;
        }
        print("distance " + action);
        return action;
    }


    private int considerPlayerHealth()
    {
        int playerHealth = returnPlayerBaseHealth();
        print(playerHealth);
        int action = 0;
        if (playerHealth > 0 && playerHealth < 31)
            action = 40;
        else if (playerHealth > 30 && playerHealth < 51)
            action = 20;
        else if (playerHealth > 50 && playerHealth < 81)
            action = 20;
        else if (playerHealth > 80 && playerHealth < 101)
           action =30;
       
        return action;
    }
    private void finalDecision(int healthAspect, int distanceAspect,int gold)
    {
        
        int action = healthAspect + distanceAspect;

        if (action > 0 && action < 51)
        {
          
            return;
        }
        else if (action > 50 && action < 101)
        {
            print("Spawn");
            chooseUnit(gold);
        }
      
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
        yield return new WaitForSeconds(4);
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

    private int returnDistance(float unitXValue)
    {
        int distance = (int)(spawnXPos - unitXValue);
        return distance;
    }
    private bool returnIsAttacking()
    {
        return manager.returnIsAttacking();
    }
    private int returnPlayerBaseHealth()
    {
        return playerBaseScript.getHealth();
    }




    private void upgrade()
    {

    }
}







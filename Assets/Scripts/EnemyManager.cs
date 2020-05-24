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
    int waitTime =4;
    bool canSpawnAll;
    public bool enemyActive,upgradesComplete;
    List<int> upgradeListOrder;
    bool upgrade;

    private void Start()
    {
        upgrade = false;
        canSpawnAll = false;
        spawnXPos = spawnTran.position.x;
        upgradesComplete = false;
    }


    public void begin()
    {

        if (enemyActive)
        {
            canSpawnAll = false;
            StartCoroutine(timeCanSpawnAllUnits());
            StartCoroutine(aiReponseDelay());

            upgradeListOrder = new List<int>();
            setUpgradeList();
            upgradesComplete = false;

        }
    }

  

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
      
        if (unitCount < 4 && gold >= constants.LIGHT_UNIT_COST)
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
        
        if (!upgradesComplete)
        {
            if (unitCount < 3)
                chooseUnit(gold);
            else
            {
              makeEducatedDecision(gold,unitCount);
            }
        }
        else
            chooseUnit(gold);
    }



    void makeEducatedDecision(int gold,int unitCount)
    {
        int distanceAdditive = considerDistance();
        int baseAdditive = considerPlayerHealth();
        int unitCountSubtractive = considerUnitCount(unitCount);
        distanceAdditive -= unitCountSubtractive;
        finalDecision(baseAdditive, distanceAdditive, gold);
    }



    private int considerDistance()
    {
        int distance = returnDistance(manager.returnLinearPosition());
        bool isAttacking = returnIsAttacking();

       
        int action = 0;
        if (isAttacking)
        {
            if (distance > 0 && distance < 21)
                action = 40;
            else if (distance > 20 && distance < 41)
                action = 30;
            else if (distance > 40 && distance < 61)
                action = 20;
            else if (distance > 60)
                action = 30;
            else
            {
                print(distance);
                print("Errir");
            }
        }
        else
        {
            if (distance > 0 && distance < 21)
                action = 10;
            else if (distance > 20 && distance < 41)
                action = 20;
            else if (distance > 40 && distance < 61)
                action = 20;
            else if (distance > 60)
                action = 40;
            else
            {
                print(distance);
                print("Error");
            }
        }
        
      
        return action;
    }

    private int considerUnitCount(int unitCount)
    {
        if (unitCount > 3 && unitCount < 5)
            return 0;
        else if (unitCount > 4 && unitCount < 6)
            return 5;
        else
            return 10;
    }
    

    private int considerPlayerHealth()
    {
        int playerHealth = returnPlayerBaseHealth();
        int playerHealthPercentage = (playerHealth / constants.BASE_HEALTH) * 100;

        int action = 0;
        if (playerHealthPercentage > 0 && playerHealthPercentage < 31)
            action = 40;
        else if (playerHealthPercentage > 30 && playerHealthPercentage < 51)
            action = 20;
        else if (playerHealthPercentage > 50 && playerHealthPercentage < 81)
            action = 20;
        else if (playerHealthPercentage > 80 && playerHealthPercentage < 101)
           action = 30;
        else
        {
            print(playerHealthPercentage);
            print("Error");
        }

      
       
        return action;
    }
    private void finalDecision(int healthAspect, int distanceAspect, int gold)
    {
      
        int action = healthAspect + distanceAspect;

        //print(action);
        if (action > 0 && action < 46)
        {
            if (gold < 500)
                return;
           
            if (upgrade)
            {
                upgrade = false;
                return;
            }
            print("up");
            upgradeAction(gold);
            return;
        }
        else if (action > 45 && action < 101)
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
        spawner.spawnUnit(unitType, false);
    }

    IEnumerator aiReponseDelay()
    {
        yield return new WaitForSeconds(waitTime);
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
            default:
                print("Error");
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
            if ((random >= constants.LIGHT_RANDOM_MAX && random < constants.LIGHT_RANDOM_MAX + 25))
                unitType = constants.LIGHT_UNIT_TYPE;
            else
                unitType = constants.RANGED_UNIT_TYPE;
        }
        else if (random >= constants.RANGED_RANDOM_MAX  && random < constants.MEDIUM_RANDOM_MAX)
        {
            if ((random >= constants.RANGED_RANDOM_MAX && random < constants.RANGED_RANDOM_MAX + 25))
                unitType = constants.RANGED_UNIT_TYPE;
            else
                unitType = constants.MEDIUM_UNIT_TYPE;
        }
        else if (random >= constants.MEDIUM_RANDOM_MAX && random < constants.HEAVY_RANDOM_MAX)
        {
            if ((random >= constants.MEDIUM_RANDOM_MAX && random < constants.MEDIUM_RANDOM_MAX + 25))
                unitType = constants.MEDIUM_UNIT_TYPE;
            else

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
        else
        {
            print("Error");
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

    private void upgradeAction(int gold)
    {
        decideUpgrade(gold);
    }

   private void decideUpgrade(int gold)
   {

        if (gold < constants.firstUpgradeCost)
            return;

        if (upgradeStage(constants.initialStage, constants.firstUpgradeCost))
        {
            StartCoroutine(upgradeDelay());
            upgrade = true;
            return;
        }
          
      
        if (gold < constants.secondUpgradeCost)
            return;

        if (upgradeStage(constants.stage_one, constants.secondUpgradeCost))
        {
            StartCoroutine(upgradeDelay());
            upgrade = true;
            return;
        }
            

        if (gold < constants.finalUpgradeCost)
            return;

        if (upgradeStage(constants.stage_two, constants.finalUpgradeCost))
        {
            StartCoroutine(upgradeDelay());
            upgrade = true;
            return;
        }
        else
            upgradesComplete = true;
   }

    private bool upgradeStage(int stage, int cost)
    {
        if (upgradeScript.checkEntireUpgradeState(stage))
        {
            for (int j = 0; j < upgradeListOrder.Count; j++)
            {
                //Check for first upgrades 
                if (upgradeScript.checkEnemyUpgradeState(stage, upgradeListOrder[j]))
                {
                    int[] upgrade = constants.retrurnUpgradeKeyComponent(upgradeListOrder[j]);
                    int unitType = upgrade[0];
                    int attributeType = upgrade[1];
                    upgradeScript.enemyUpgrade(upgradeListOrder[j], attributeType, unitType, stage, cost);
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

  
    private void setUpgradeList()
    {
       
        upgradeListOrder.Add(constants.lightHealthKey);
        upgradeListOrder.Add(constants.lightDamageKey);
        upgradeListOrder.Add(constants.mediumSpeedKey);
        upgradeListOrder.Add(constants.mediumDamageKey);
        upgradeListOrder.Add(constants.rangeDamageKey);
        upgradeListOrder.Add(constants.rangeRangeKey);
        upgradeListOrder.Add(constants.heavyHealthKey);
        upgradeListOrder.Add(constants.heavyDamageKey);
        //Randomise order each game
        upgradeListOrder.Shuffle();
    }



    private IEnumerator upgradeDelay()
    {
        yield return new WaitForSeconds(6);
        upgrade = false;
    }
}

public static class IListExtensions
{
    //Code Taken from https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}







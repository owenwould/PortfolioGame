﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private Dictionary<int,UnitTypeStats> playerUnitStats;
    private Dictionary<int, UnitTypeStats> enemyUnitStats;
    [SerializeField] GameManager managerScript;
    Dictionary<int, int> EnemyUpgradeState;
    Dictionary<int, int> PlayerUpgradeState;
    
    public void begin()
    {
        playerUnitStats = new Dictionary<int, UnitTypeStats>();
        enemyUnitStats = new Dictionary<int, UnitTypeStats>();
        EnemyUpgradeState = new Dictionary<int, int>();
        PlayerUpgradeState = new Dictionary<int, int>();
        setUnitStats();
    }


    private void setUnitStats()
    {
        setLightUnit();
        setRangedUnit();
        setMediumUnit();
        setHeavyUnit();
        setEnemyUpgrades();
        setPlayerUpgrades();
    }
    private void setLightUnit()
    {
        //Light - damage/health 
        UnitTypeStats light = new UnitTypeStats(constants.LIGHT_HEALTH,
            constants.LIGHT_MIN_DAMAGE,constants.LIGHT_MAX_DAMAGE, constants.LIGHT_DAMAGE_DELAY,
            constants.LIGHT_RANGE, constants.LIGHT_SPEED);

        UnitTypeStats lightEnemy = new UnitTypeStats(constants.LIGHT_HEALTH,
            constants.LIGHT_MIN_DAMAGE, constants.LIGHT_MAX_DAMAGE, constants.LIGHT_DAMAGE_DELAY,
            constants.LIGHT_RANGE, constants.LIGHT_SPEED);

        playerUnitStats.Add(constants.LIGHT_UNIT_TYPE, light);
        enemyUnitStats.Add(constants.LIGHT_UNIT_TYPE, lightEnemy);
    } 

    private void setRangedUnit()
    {
        UnitTypeStats range = new UnitTypeStats(constants.RANGED_HEALTH,constants.RANGED_MIN_DAMAGE,
            constants.RANGED_MAX_DAMAGE,constants.RANGED_DAMAGE_DELAY,constants.RANGED_RANGE,constants.RANGED_SPEED);

        UnitTypeStats rangeEnemy = new UnitTypeStats(constants.RANGED_HEALTH, constants.RANGED_MIN_DAMAGE,
            constants.RANGED_MAX_DAMAGE, constants.RANGED_DAMAGE_DELAY, constants.RANGED_RANGE, constants.RANGED_SPEED);
        playerUnitStats.Add(constants.RANGED_UNIT_TYPE, range);
        enemyUnitStats.Add(constants.RANGED_UNIT_TYPE, rangeEnemy);
    }

    private void setMediumUnit()
    {
        UnitTypeStats medium = new UnitTypeStats(constants.MEDIUM_HEALTH, constants.MEDIUM_MIN_DAMAGE,
           constants.MEDIUM_MAX_DAMAGE, constants.MEDIUM_DAMAGE_DELAY, constants.MEDIUM_RANGE, constants.MEDIUM_SPEED);

        UnitTypeStats mediumEnemy = new UnitTypeStats(constants.MEDIUM_HEALTH, constants.MEDIUM_MIN_DAMAGE,
           constants.MEDIUM_MAX_DAMAGE, constants.MEDIUM_DAMAGE_DELAY, constants.MEDIUM_RANGE, constants.MEDIUM_SPEED);
        playerUnitStats.Add(constants.MEDIUM_UNIT_TYPE, medium);
        enemyUnitStats.Add(constants.MEDIUM_UNIT_TYPE, mediumEnemy);
    }
    private void setHeavyUnit()
    {
        //Never use the same object in both dictionaries - they share the same memory address and will both update together
        UnitTypeStats heavy = new UnitTypeStats(constants.HEAVY_HEALTH, constants.HEAVY_MIN_DAMAGE,
              constants.HEAVY_MAX_DAMAGE, constants.HEAVY_DAMAGE_DELAY, constants.HEAVY_RANGE, constants.HEAVY_SPEED);

        UnitTypeStats heavyEnemy = new UnitTypeStats(constants.HEAVY_HEALTH, constants.HEAVY_MIN_DAMAGE,
              constants.HEAVY_MAX_DAMAGE, constants.HEAVY_DAMAGE_DELAY, constants.HEAVY_RANGE, constants.HEAVY_SPEED);
        playerUnitStats.Add(constants.HEAVY_UNIT_TYPE, heavy);
       enemyUnitStats.Add(constants.HEAVY_UNIT_TYPE, heavyEnemy);
    }

    public UnitTypeStats returnUnitStats(bool isPlayer,int unitType)
    {
        if (isPlayer)
        {
            if (playerUnitStats.ContainsKey(unitType))
                return playerUnitStats[unitType];
        }
        else
        {
            if (enemyUnitStats.ContainsKey(unitType))
                return enemyUnitStats[unitType];
        }
        return null;
    }

    public int returnCurrentProgress(int upgradeKey)
    {
        int stage = getPlayerUpgradeStage(upgradeKey);
        return stage;
    }

    public bool upgradePlayerAttribute(int upgradeKey,int unitType,int attributeType)
    {
        int currentStage = getPlayerUpgradeStage(upgradeKey);
        if (!(currentStage == constants.stage_final))
        {
            int cost = constants.returnUpgradeCost(currentStage);
            bool result = upgradePlayerUnit(cost, attributeType, unitType, currentStage,upgradeKey);
            return result;
        }
        return false;
    }

    private bool upgradePlayerUnit(int cost,int attributeType,int unitType,int currentStage,int upgradeKey)
    {
        int currentGold = managerScript.getPlayerGold();
        if (currentGold < cost)
            return false;

        int initialValue = constants.returnInitialValue(unitType, attributeType);
        int newValue = constants.returnNewValue(initialValue, currentStage);
        managerScript.decreaseGold(cost, true);
      
        upgradeUnitStats(playerUnitStats, attributeType, unitType, newValue, currentStage);
        setUpgradeState(PlayerUpgradeState, upgradeKey, currentStage);
        return true;
    }

    private void upgradeUnitStats(Dictionary<int,UnitTypeStats> unitList,int attributeType,int unitType, int newValue, int stage)
    {
        if (unitList.ContainsKey(unitType))
        {
            switch (attributeType)
            {
                case constants.attribute_tpye_health:
                    unitList[unitType].setHealth(newValue);
                    break;
                case constants.attribute_type_damage:
                    unitList[unitType].setMinDamage(newValue);
                    int maxDamage = constants.returnNewValue(constants.returnInitialMaxDamage(unitType), stage);
                    unitList[unitType].setMaxDamage(maxDamage);
                    break;
                case constants.attribute_type_range:
                    unitList[unitType].setRange(newValue);
                    break;
                case constants.attribute_type_speed:
                    unitList[unitType].setSpeed(newValue);
                    break;
                default:
                    print("Error");
                    break;
            }
        }
        else
        {
            print("Error");
        }
    }

    public void enemyUpgrade(int upgradeKey, int attributeType,int unitType,int stage,int cost)
    {
        int gold = managerScript.getEnemyGold();
        if (gold < cost)
            return;

        int initialValue = constants.returnInitialValue(unitType, attributeType);
        int newValue = constants.returnNewValue(initialValue, stage);
        managerScript.decreaseGold(cost, false);
        upgradeUnitStats(enemyUnitStats, attributeType, unitType, newValue, stage);

        //Update Upgrade Dictionary
        setUpgradeState(EnemyUpgradeState, upgradeKey, stage);
    }

    private void setEnemyUpgrades()
    {
        int initialState = constants.initialStage;
        EnemyUpgradeState.Add(constants.lightHealthKey,initialState);
        EnemyUpgradeState.Add(constants.lightDamageKey, initialState);
        EnemyUpgradeState.Add(constants.mediumSpeedKey, initialState);
        EnemyUpgradeState.Add(constants.mediumDamageKey, initialState);
        EnemyUpgradeState.Add(constants.rangeDamageKey, initialState);
        EnemyUpgradeState.Add(constants.rangeRangeKey, initialState);
        EnemyUpgradeState.Add(constants.heavyHealthKey, initialState);
        EnemyUpgradeState.Add(constants.heavyDamageKey, initialState);
    }

    private void setEnemyUpgradeState(int upgradeKey)
    {
        if (EnemyUpgradeState.ContainsKey(upgradeKey))
        {
            int currentStage = EnemyUpgradeState[upgradeKey];
            switch (currentStage)
            {
                case constants.initialStage:
                    EnemyUpgradeState[upgradeKey] = constants.stage_one;
                    break;
                case constants.stage_one:
                    EnemyUpgradeState[upgradeKey] = constants.stage_two;
                    break;
                case constants.stage_two:
                    EnemyUpgradeState[upgradeKey] = constants.stage_final;
                    break;
            }
        }
        else
            print("Dictionary Key error");
    }

    private void setPlayerUpgradeState(int upgradeKey,int currentStage)
    {
        if (PlayerUpgradeState.ContainsKey(upgradeKey))
        {
          
            switch (currentStage)
            {
                case constants.initialStage:
                    PlayerUpgradeState[upgradeKey] = constants.stage_one;
                    break;
                case constants.stage_one:
                    PlayerUpgradeState[upgradeKey] = constants.stage_two;
                    break;
                case constants.stage_two:
                    PlayerUpgradeState[upgradeKey] = constants.stage_final;
                    break;
            }
        }
        else
            print("Dictionary Key error");
    }

    private void setUpgradeState(Dictionary<int,int> upgradeDict,int upgradeKey,int currentStage)
    {
        if (upgradeDict.ContainsKey(upgradeKey))
        {
            switch (currentStage)
            {
                case constants.initialStage:
                    upgradeDict[upgradeKey] = constants.stage_one;
                    break;
                case constants.stage_one:
                    upgradeDict[upgradeKey] = constants.stage_two;
                    break;
                case constants.stage_two:
                    upgradeDict[upgradeKey] = constants.stage_final;
                    break;
            }
        }
        else
            print("Dictionary Key error");
    }


    public bool checkEnemyUpgradeState(int state, int upgradeKey)
    {
        if (EnemyUpgradeState.ContainsKey(upgradeKey))
        {
            if (EnemyUpgradeState[upgradeKey] == state)
                return true;
            else
                return false;
        }
        else
        {
            print("Error");
        }
        return false;
    }

    public bool checkEntireUpgradeState(int state)
    {
        if (EnemyUpgradeState.ContainsValue(state))
            return true;
        else
            return false;
    }


    private void setPlayerUpgrades()
    {
        int initialState = constants.initialStage;
        PlayerUpgradeState.Add(constants.lightHealthKey, initialState);
        PlayerUpgradeState.Add(constants.lightDamageKey, initialState);
        PlayerUpgradeState.Add(constants.mediumSpeedKey, initialState);
        PlayerUpgradeState.Add(constants.mediumDamageKey, initialState);
        PlayerUpgradeState.Add(constants.rangeDamageKey, initialState);
        PlayerUpgradeState.Add(constants.rangeRangeKey, initialState);
        PlayerUpgradeState.Add(constants.heavyHealthKey, initialState);
        PlayerUpgradeState.Add(constants.heavyDamageKey, initialState);
    }


    private int getPlayerUpgradeStage(int upgradeKey)
    {
        if (PlayerUpgradeState.ContainsKey(upgradeKey))
        {
            return PlayerUpgradeState[upgradeKey];
        }
        else
        {
            print("Error Missing Key ");
            return 0;
        }
    }

 







}

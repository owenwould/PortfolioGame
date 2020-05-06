using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private Dictionary<int,UnitTypeStats> playerUnitStats;
    private Dictionary<int, UnitTypeStats> enemyUnitStats;

    private Dictionary<int, UnitAttributes> unitAttributes;
    [SerializeField] GameManager managerScript;
    
    public void begin()
    {
        playerUnitStats = new Dictionary<int, UnitTypeStats>();
        enemyUnitStats = new Dictionary<int, UnitTypeStats>();
        unitAttributes = new Dictionary<int, UnitAttributes>();
        setUnitStats();
    }


    private void setUnitStats()
    {
        setLightUnit();
        setRangedUnit();
        setMediumUnit();
        setHeavyUnit();
    }


    private void setLightUnit()
    {
        //Light - damage/health 
        UnitTypeStats light = new UnitTypeStats(constants.LIGHT_HEALTH,
            constants.LIGHT_MIN_DAMAGE,constants.LIGHT_MAX_DAMAGE, constants.LIGHT_DAMAGE_DELAY,
            constants.LIGHT_RANGE, constants.LIGHT_SPEED);
        playerUnitStats.Add(constants.LIGHT_UNIT_TYPE, light);
        enemyUnitStats.Add(constants.LIGHT_UNIT_TYPE, light);
    } 

    private void setRangedUnit()
    {
        UnitTypeStats range = new UnitTypeStats(constants.RANGED_HEALTH,constants.RANGED_MIN_DAMAGE,
            constants.RANGED_MAX_DAMAGE,constants.RANGED_DAMAGE_DELAY,constants.RANGED_RANGE,constants.RANGED_SPEED);
        playerUnitStats.Add(constants.RANGED_UNIT_TYPE, range);
        enemyUnitStats.Add(constants.RANGED_UNIT_TYPE, range);
    }

    private void setMediumUnit()
    {
        UnitTypeStats medium = new UnitTypeStats(constants.MEDIUM_HEALTH, constants.MEDIUM_MIN_DAMAGE,
           constants.MEDIUM_MAX_DAMAGE, constants.MEDIUM_DAMAGE_DELAY, constants.MEDIUM_RANGE, constants.MEDIUM_SPEED);
        playerUnitStats.Add(constants.MEDIUM_UNIT_TYPE, medium);
        enemyUnitStats.Add(constants.MEDIUM_UNIT_TYPE, medium);
    }
    private void setHeavyUnit()
    {
        UnitTypeStats heavy = new UnitTypeStats(constants.HEAVY_HEALTH, constants.HEAVY_MIN_DAMAGE,
              constants.HEAVY_MAX_DAMAGE, constants.HEAVY_DAMAGE_DELAY, constants.HEAVY_RANGE, constants.HEAVY_SPEED);
        playerUnitStats.Add(constants.HEAVY_UNIT_TYPE, heavy);
        enemyUnitStats.Add(constants.HEAVY_UNIT_TYPE, heavy);
    }



    public UnitTypeStats unitValues(int iEntityType, bool isPlayer)
    {
        if (isPlayer)
        {
            if (playerUnitStats.ContainsKey(iEntityType))
                return playerUnitStats[iEntityType];
        }
        else
        {
            if (enemyUnitStats.ContainsKey(iEntityType))
                return enemyUnitStats[iEntityType];
        }

        return null;
    }






  
    public void upgradeAttribute(bool isPlayer, int unitType, int attributeType)
    {
        int currentValue = returnCurrentAttributeValue(isPlayer, unitType, attributeType);
        int currentStage = constants.returnUpgradeStage(currentValue, attributeType, unitType);
        print(currentStage);


        
        switch (currentStage)
        {
            case constants.initialStage:
                upgrade(constants.firstUpgradeCost, isPlayer, attributeType, unitType,currentStage);
                break;
            case constants.stage_one:
                upgrade(constants.secondUpgradeCost, isPlayer, attributeType, unitType,currentStage);
                break;
            case constants.stage_two:
                upgrade(constants.finalUpgradeCost, isPlayer, attributeType, unitType,currentStage);
                break;
            case constants.stage_final:
                return;      
        }
    }



    private void upgrade(int cost, bool isPlayer,int attributeType, int unitType,int currentStage)
    {
        int currentGold;
        if (isPlayer)
            currentGold = managerScript.getPlayerGold();
        else
            currentGold = managerScript.getEnemyGold();

        int initialAttributeVal = constants.returnInitialValue(unitType, attributeType);
        int newValue = constants.returnNewValue(initialAttributeVal, currentStage);

        if (currentGold < cost)
            return;
        else
        {
           
            if (isPlayer)
            {
                upgradeStat(playerUnitStats, attributeType, unitType, newValue,currentStage);
                managerScript.decreaseGold(cost, true);
            }
            else
            {
                upgradeStat(enemyUnitStats, attributeType, unitType, newValue,currentStage);
                managerScript.decreaseGold(cost, false);
            }
        } 
    }

    private void upgradeStat(Dictionary<int,UnitTypeStats> unitList,int attributeType,int unitType, int newValue,int stage)
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
            }

        }
        else
            print("Error");
    }



    private int returnCurrentAttributeValue(bool isPlayer, int unitType,int attributeType)
    {
        if (isPlayer)
        {
            return getAttributeValue(playerUnitStats, attributeType, unitType);
        }
        else
        {
            return getAttributeValue(enemyUnitStats, attributeType, unitType);
        }
    }


    private int getAttributeValue(Dictionary<int,UnitTypeStats> unitList, int attributeType, int unitType)
    {
        if (unitList.ContainsKey(unitType)) {

            if (attributeType == constants.attribute_tpye_health)
            {
                return unitList[unitType].getHealth();
            }
            else if (attributeType == constants.attribute_type_damage)
            {
                return unitList[unitType].getMinDamage();
            }
            else if (attributeType == constants.attribute_type_range)
            {
                return unitList[unitType].getRange();
            }
            else if (attributeType == constants.attribute_type_speed)
            {
                return unitList[unitType].getSpeed();
            }
        }
        else
        {
            print("Error");
        }
        return 0;
    }


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    private Dictionary<int,UnitTypeStats> playerUnitStats;
    private Dictionary<int, UnitTypeStats> enemyUnitStats;
   
    
    public void begin()
    {
        playerUnitStats = new Dictionary<int, UnitTypeStats>();
        enemyUnitStats = new Dictionary<int, UnitTypeStats>();
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
        playerUnitStats.Add(constants.HEAVY_UNIT_COST, heavy);
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


    
}

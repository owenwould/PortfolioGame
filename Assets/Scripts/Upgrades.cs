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

        


        UnitTypeStats medium = new UnitTypeStats();
        
        UnitTypeStats heavy = new UnitTypeStats();




    }


    private void setLightUnit()
    {
        //Light - damage/health 
        UnitTypeStats light = new UnitTypeStats(constants.LIGHT_HEALTH,
            constants.LIGHT_DAMAGE, constants.LIGHT_DAMAGE_DELAY,
            constants.LIGHT_RANGE, constants.LIGHT_SPEED);
        playerUnitStats.Add(constants.LIGHT_UNIT_TYPE, light);
        enemyUnitStats.Add(constants.LIGHT_UNIT_TYPE, light);
    } 

    private void setRangedUnit()
    {
        UnitTypeStats range = new UnitTypeStats();




        playerUnitStats.Add(constants.RANGED_UNIT_TYPE, range);
        enemyUnitStats.Add(constants.RANGED_UNIT_TYPE, range);

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

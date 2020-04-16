using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Dictionary<int,UnitTypeStats> playerUnitStats;
    private Dictionary<int, UnitTypeStats> enemyUnitStats;
    void Start()
    {

        setUnitStats();
    }

    private void setUnitStats()
    {
        playerUnitStats = new Dictionary<int, UnitTypeStats>();
        enemyUnitStats = new Dictionary<int, UnitTypeStats>();

        UnitTypeStats light = new UnitTypeStats(constants.LIGHT_DEFUALT_HEALTH,
            constants.LIGHT_DEFAULT_DAMAGE,constants.LIGHT_DEFAULT_DAMAGERATE,
            constants.LIGHT_DEFAULT_RANGE,constants.LIGHT_DEFAULT_SPEED);
        playerUnitStats.Add(constants.LIGHT_UNIT_TYPE,light);
        enemyUnitStats.Add(constants.LIGHT_UNIT_TYPE, light);

        UnitTypeStats medium = new UnitTypeStats();
        UnitTypeStats range = new UnitTypeStats();
        UnitTypeStats heavy = new UnitTypeStats();
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

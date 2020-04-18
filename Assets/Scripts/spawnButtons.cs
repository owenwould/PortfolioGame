using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpawnCharacter spawnScript;
    public void spawnLightUnit()
   {
        spawnScript.spawnUnit(constants.LIGHT_UNIT_TYPE, true);
   }
    public void spawnMediumUnit()
    {
        spawnScript.spawnUnit(constants.MEDIUM_UNIT_TYPE, true);
    }
    public void spawnRangeUnit()
    {
        spawnScript.spawnUnit(constants.RANGED_UNIT_TYPE, true);
    }
    public void spawnHeavyUnit()
    {
        spawnScript.spawnUnit(constants.HEAVY_UNIT_TYPE, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constants : MonoBehaviour
{
    public const string enemyMaskName = "EnemyArmyLayer", playerMaskName = "PlayerArmyLayer";
    public const string enemyTag = "Enemy", playerTag = "Player", baseTag = "Base";
    public const int MAX_UNIT_COUNT = 10;
    public const int LIGHT_UNIT_TYPE = 1, RANGED_UNIT_TYPE = 2,MEDIUM_UNIT_TYPE = 3 ,HEAVY_UNIT_TYPE = 4;
    public const int LIGHT_UNIT_COST = 100, RANGE_UNIT_COST = 125, MEDIUM_UNIT_COST = 200,HEAVY_UNIT_COST = 400;
    public const float LIGHT_UNIT_WAIT = 1f, RANGE_UNIT_WAIT = 2f, MEDIUM_UNIT_WAIT = 3f, HEAVY_UNIT_WAIT = 4f;
}

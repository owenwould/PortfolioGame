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
    public const float LIGHT_UNIT_WAIT = 1f, RANGE_UNIT_WAIT = 1.5f, MEDIUM_UNIT_WAIT = 2.5f, HEAVY_UNIT_WAIT = 4f;

    public const string isRunning = "isRunning", isAttacking = "isAttacking";

    public const int LIGHT_RANDOM_MAX = 100, RANGED_RANDOM_MAX = 200, MEDIUM_RANDOM_MAX = 300, HEAVY_RANDOM_MAX = 400;


    
    public const int startGoldPlayerEasy = 1200;

    //Unit stats - light
    public const int LIGHT_HEALTH = 140,  LIGHT_MIN_DAMAGE = 25, LIGHT_MAX_DAMAGE = 35;
    public const float LIGHT_DAMAGE_DELAY = 0.67f, LIGHT_SPEED = 4.28f, LIGHT_RANGE = 1f;

    //Unit stats - medium 
    public const int MEDIUM_HEALTH = 120, MEDIUM_MIN_DAMAGE = 30, MEDIUM_MAX_DAMAGE = 35;
    public const float MEDIUM_DAMAGE_DELAY = 0.94f, MEDIUM_SPEED = 3.40f, MEDIUM_RANGE = 1f;

    //Unit Stats - Ranged
    public const int RANGED_HEALTH = 100, RANGED_MIN_DAMAGE = 15, RANGED_MAX_DAMAGE = 35;
    public const float RANGED_DAMAGE_DELAY = 0.51f, RANGED_SPEED = 5.93f, RANGED_RANGE = 6f;

    //Unit Stats - Heavy
    public const int HEAVY_HEALTH = 400, HEAVY_MIN_DAMAGE = 30, HEAVY_MAX_DAMAGE = 50;
    public const float HEAVY_DAMAGE_DELAY = 0.85f, HEAVY_SPEED = 2.43f, HEAVY_RANGE = 1f;









}

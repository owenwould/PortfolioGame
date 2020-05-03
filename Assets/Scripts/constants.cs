﻿using System.Collections;
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

    public const string isRunning = "isRunning", isAttacking = "isAttacking";

    public const int LIGHT_RANDOM_MAX = 100, RANGED_RANDOM_MAX = 200, MEDIUM_RANDOM_MAX = 300, HEAVY_RANDOM_MAX = 400;


    //
    public const int startGoldPlayerEasy = 1200;

    //Unit stats - light
    public const int LIGHT_HEALTH = 100,  LIGHT_DAMAGE = 30;
    public const float LIGHT_DAMAGE_DELAY = 0.67f, LIGHT_SPEED = 4.28f, LIGHT_RANGE = 1f;

    //Unit stats - medium 
    public const int MEDIUM_HEALTH = 120, MEDIUM_DEFAULT_DAMAGE = 30;
    public const float MEDIUM_DAMAGE_RATE = 1f, MEDIUM_SPEED = 2.5f, MEDIUM_RANGE = 1f;

    //Unit Stats - light
    public const int RANGED_HEALTH = 75, RANGED_DAMAGE = 30;






}

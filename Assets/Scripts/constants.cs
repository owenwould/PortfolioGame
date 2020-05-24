using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constants : MonoBehaviour
{
    public const string enemyMaskName = "EnemyArmyLayer", playerMaskName = "PlayerArmyLayer";
    public const string enemyTag = "Enemy", playerTag = "Player", baseTag = "Base";
    public const string lightTag = "Light", rangeTag = "Range", mediumTag = "Medium", heavyTag = "Heavy";
    public const string healthBarTag = "HealthBar";

    public const int MAX_UNIT_COUNT = 10;
    public const int LIGHT_UNIT_TYPE = 1, RANGED_UNIT_TYPE = 2,MEDIUM_UNIT_TYPE = 3 ,HEAVY_UNIT_TYPE = 4;
    public const int LIGHT_UNIT_COST = 100, RANGE_UNIT_COST = 125, MEDIUM_UNIT_COST = 200,HEAVY_UNIT_COST = 400;
    public const float LIGHT_UNIT_WAIT = 1f, RANGE_UNIT_WAIT = 1.5f, MEDIUM_UNIT_WAIT = 2.5f, HEAVY_UNIT_WAIT = 4f;
    public const float healthBarConstant = 2f;
    public const string isRunning = "isRunning", isAttacking = "isAttacking";

    public const int LIGHT_RANDOM_MAX = 100, RANGED_RANDOM_MAX = 200, MEDIUM_RANDOM_MAX = 300, HEAVY_RANDOM_MAX = 400;
    public const int BASE_HEALTH = 800;


    //Gameover text 
    public const string goldSpentText = "Amount of gold spent:",enemiesKilledText = "Number enemies killed:",friendlySpawnText = "Number of friendly units spawned:",timeTakenText = "Game Time: ";
    public const string secondsText = "seconds";


    public const int startGold = 3000,enemyStartGold = 4500;


    public const int firstUpgradeCost = 300, secondUpgradeCost = 500, finalUpgradeCost = 1000;


    public const int attribute_tpye_health = 20, attribute_type_damage = 21, attribute_type_range = 22, attribute_type_speed = 23;


    public const float upgradeModifierFirst = 0.2f, upgradeModifierSecond = 0.5f, upgradeModiferFinal = 1f;

    public const int initialStage = 0, stage_one = 1, stage_two = 2, stage_final = 3;
    public const int firendlyDetectionRange = 2;

    //Unit stats - light
    public const int LIGHT_HEALTH = 200,  LIGHT_MIN_DAMAGE = 20, LIGHT_MAX_DAMAGE = 35,LIGHT_SPEED = 6, LIGHT_RANGE = 1;
    public const float LIGHT_DAMAGE_DELAY = 1.0f;

    //Unit stats - medium 
    public const int MEDIUM_HEALTH = 300, MEDIUM_MIN_DAMAGE = 35, MEDIUM_MAX_DAMAGE = 40, MEDIUM_SPEED = 4, MEDIUM_RANGE = 1;
    public const float MEDIUM_DAMAGE_DELAY = 1.2f;

    //Unit Stats - Ranged
    public const int RANGED_HEALTH = 150, RANGED_MIN_DAMAGE = 15, RANGED_MAX_DAMAGE = 30, RANGED_SPEED = 5, RANGED_RANGE = 5;
    public const float RANGED_DAMAGE_DELAY = 1.3f; 

    //Unit Stats - Heavy
    public const int HEAVY_HEALTH = 400, HEAVY_MIN_DAMAGE = 40, HEAVY_MAX_DAMAGE = 50, HEAVY_SPEED = 3, HEAVY_RANGE = 1;
    public const float HEAVY_DAMAGE_DELAY = 1.5f;


    public const string HEALTH_UPGRADE_TEXT = "Upgrade Health", DAMAGE_UPGRADE_TEXT = "Upgrade Damage", SPEED_UPGRADE_TEXT = "Upgrade Speed", RANGE_UPGRADE_TEXT = "Upgrade Range",UPGRADE_COMPLETE = "Complete";
    //Upgrades 

    public const int lightHealthKey = 12, lightDamageKey = 13, mediumSpeedKey = 14, mediumDamageKey = 15,rangeRangeKey =16,rangeDamageKey=17,heavyHealthKey = 18,heavyDamageKey=19;

    private static int getStageValue(int initialValue, float modifier)
    {
        float additional = (float) (initialValue * modifier);
        int convertedVal = Mathf.CeilToInt(additional); 
        return convertedVal + initialValue;

    }
    public static int returnNewValue(int originalValue, int stage)
    {
        switch(stage)
        {
            case initialStage:
                return getStageValue(originalValue, upgradeModifierFirst);
            case stage_one:
                return getStageValue(originalValue, upgradeModifierSecond);
            case stage_two:
                return getStageValue(originalValue, upgradeModiferFinal);

            default:
                print("Error");
                break;
        }
       
        return 1000;
    }
    public static int returnInitialValue(int unitType,int attributeType)
    {
        switch (unitType)
        {
            case LIGHT_UNIT_TYPE:
                if (attributeType == attribute_type_damage)
                    return LIGHT_MIN_DAMAGE;
                else
                    return LIGHT_HEALTH;
            case MEDIUM_UNIT_TYPE:
                if (attributeType == attribute_type_damage)
                    return MEDIUM_MIN_DAMAGE;
                else
                    return MEDIUM_SPEED;
            case RANGED_UNIT_TYPE:
                if (attributeType == attribute_type_damage)
                    return RANGED_MIN_DAMAGE;
                else
                    return RANGED_RANGE;
            case HEAVY_UNIT_TYPE:
                if (attributeType == attribute_type_damage)
                    return HEAVY_MIN_DAMAGE;
                else
                    return HEAVY_HEALTH;

            default:
                print("Error");
                break;
        }
       
        return 0;
    }

    public static int returnInitialMaxDamage(int unitType)
    {
        switch (unitType)
        {
            case LIGHT_UNIT_TYPE:
                return LIGHT_MAX_DAMAGE;
            case MEDIUM_UNIT_TYPE:
                return MEDIUM_MAX_DAMAGE;
            case RANGED_UNIT_TYPE:
                return RANGED_MAX_DAMAGE;
            case HEAVY_UNIT_TYPE:
                return HEAVY_MAX_DAMAGE;
        }
        print("Error");
        return 0;

    }
    public static string returnUpgradeText(int attributeType)
    {
        switch (attributeType)
        {
            case attribute_tpye_health:
                return HEALTH_UPGRADE_TEXT;
            case attribute_type_damage:
                return DAMAGE_UPGRADE_TEXT;
            case attribute_type_speed:
                return SPEED_UPGRADE_TEXT;
            case attribute_type_range:
                return RANGE_UPGRADE_TEXT;
            default:
                print("error");
                break;
        }

        return "";
    }

    public static int returnUpgradeCost(int currentStage)
    {

        switch (currentStage)
        {
            case initialStage:
                return firstUpgradeCost;

            case stage_one:
                return secondUpgradeCost;

            case stage_two:
                return finalUpgradeCost;
            

            default:
                print("Error");
                print(currentStage);
                return 0; 
        }
    }


    public static int getUpgradeKey(int unitType,int attributeType)
    {
        int upgradeKey = 0;
        if (unitType == LIGHT_UNIT_TYPE)
        {
            if (attributeType == attribute_tpye_health)
                upgradeKey = lightHealthKey;
            else if (attributeType == attribute_type_damage)
                upgradeKey = lightDamageKey;
            else
            {
                print("error");
            }
        }
        else if (unitType == MEDIUM_UNIT_TYPE)
        {
            if (attributeType == attribute_type_speed)
                upgradeKey = mediumSpeedKey;
            else if (attributeType == attribute_type_damage)
                upgradeKey = mediumDamageKey;
            else
            {
                print("error");
            }
        }
        else if (unitType == RANGED_UNIT_TYPE)
        {
            if (attributeType == attribute_type_range)
                upgradeKey = rangeRangeKey;
            else if (attributeType == attribute_type_damage)
                upgradeKey = rangeDamageKey;
            else
            {
                print("error");
            }
        }
        else if (unitType == HEAVY_UNIT_TYPE)
        {
            if (attributeType == attribute_tpye_health)
                upgradeKey = heavyHealthKey;
            else if (attributeType == attribute_type_damage)
                upgradeKey = heavyDamageKey;
            else
            {
                print("error");
            }
        }
        else
        {
            print("Error");
        }
        return upgradeKey;
    }


    public static int[] retrurnUpgradeKeyComponent(int upgradeKey)
    {
        int[] upgrade = new int[2];


        //First element is unit type
        //Second element is attribute type
        switch (upgradeKey)
        {
            case lightHealthKey:
                upgrade[0] = LIGHT_UNIT_TYPE;
                upgrade[1] = attribute_tpye_health;
                break;

            case lightDamageKey:
                upgrade[0] = LIGHT_UNIT_TYPE;
                upgrade[1] = attribute_type_damage;
                break;

            case mediumSpeedKey:
                upgrade[0] = MEDIUM_UNIT_TYPE;
                upgrade[1] = attribute_type_speed;
                break;

            case mediumDamageKey:
                upgrade[0] = MEDIUM_UNIT_TYPE;
                upgrade[1] = attribute_type_damage;
                break;

            case rangeRangeKey:
                upgrade[0] = RANGED_UNIT_TYPE;
                upgrade[1] = attribute_type_range;
                break;

            case rangeDamageKey:
                upgrade[0] = RANGED_UNIT_TYPE;
                upgrade[1] = attribute_type_damage;
                break;

            case heavyHealthKey:
                upgrade[0] = HEAVY_UNIT_TYPE;
                upgrade[1] = attribute_tpye_health;
                break;

            case heavyDamageKey:
                upgrade[0] = HEAVY_UNIT_TYPE;
                upgrade[1] = attribute_type_damage;
                break;

            default:
                print(upgradeKey);
                print("Error");
                break;
        }


        return upgrade;
    }

  









}

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


    
    public const int startGoldPlayerEasy = 60000;

    public const int firstUpgradeCost = 300, secondUpgradeCost = 2000, finalUpgradeCost = 3000;


    public const int attribute_tpye_health = 10, attribute_type_damage = 11, attribute_type_range = 12, attribute_type_speed = 13;


    public const float upgradeModifierFirst = 0.2f, upgradeModifierSecond = 0.5f, upgradeModiferFinal = 1f;

    public const int initialStage = 0, stage_one = 1, stage_two = 2, stage_final = 3;
    

    //Unit stats - light
    public const int LIGHT_HEALTH = 140,  LIGHT_MIN_DAMAGE = 25, LIGHT_MAX_DAMAGE = 35,LIGHT_SPEED = 5, LIGHT_RANGE = 1;
    public const float LIGHT_DAMAGE_DELAY = 0.70f;

    //Unit stats - medium 
    public const int MEDIUM_HEALTH = 200, MEDIUM_MIN_DAMAGE = 30, MEDIUM_MAX_DAMAGE = 35, MEDIUM_SPEED = 4, MEDIUM_RANGE = 1;
    public const float MEDIUM_DAMAGE_DELAY = 0.95f;

    //Unit Stats - Ranged
    public const int RANGED_HEALTH = 100, RANGED_MIN_DAMAGE = 15, RANGED_MAX_DAMAGE = 35, RANGED_SPEED = 6, RANGED_RANGE = 6;
    public const float RANGED_DAMAGE_DELAY = 0.50f; 

    //Unit Stats - Heavy
    public const int HEAVY_HEALTH = 400, HEAVY_MIN_DAMAGE = 30, HEAVY_MAX_DAMAGE = 50, HEAVY_SPEED = 3, HEAVY_RANGE = 1;
    public const float HEAVY_DAMAGE_DELAY = 0.80f;


    public const string HEALTH_UPGRADE_TEXT = "Upgrade Health", DAMAGE_UPGRADE_TEXT = "Upgrade Damage", SPEED_UPGRADE_TEXT = "Upgrade Speed", RANGE_UPGRADE_TEXT = "Upgrade Range",UPGRADE_COMPLETE = "Complete";
    //Upgrades 
    
    public static int returnUpgradeStage(int currentValue, int attributeType, int unitType)
    {
        switch (unitType)
        {
            case LIGHT_UNIT_TYPE:
                return lightUnitAttributes(attributeType,currentValue);
            case MEDIUM_UNIT_TYPE:
                return mediumUnitAttributes(attributeType, currentValue);
            case RANGED_UNIT_TYPE:
                return rangedUnitAttributes(attributeType, currentValue);
            case HEAVY_UNIT_TYPE:
                return heavyUnitAttributes(attributeType, currentValue);

        }
        return stage_final;
    }



    private static int lightUnitAttributes(int attributeType, int currentValue)
    {
        //Damage & Health
        int initialValue;
        if (attributeType == attribute_type_damage)
            initialValue = LIGHT_MIN_DAMAGE;
        else
            initialValue = LIGHT_HEALTH;

        int stageOne = getStageValue(initialValue, upgradeModifierFirst);
        int stageTwo = getStageValue(initialValue, upgradeModifierSecond);

        return returnCurrentStage(currentValue, initialValue, stageOne, stageTwo);
    }

    private static int mediumUnitAttributes(int attributeType, int currentValue)
    {
        //Damage & Speed
        int initialValue;
        if (attributeType == attribute_type_damage)
            initialValue = MEDIUM_MIN_DAMAGE;
        else
            initialValue = MEDIUM_SPEED;

        int stageOne = getStageValue(initialValue, upgradeModifierFirst);
        int stageTwo = getStageValue(initialValue, upgradeModifierSecond);

        return returnCurrentStage(currentValue, initialValue, stageOne, stageTwo);
    }

    private static int rangedUnitAttributes(int attributeType, int currentValue)
    {
        //Damage & Ranged
        int initialValue;
        if (attributeType == attribute_type_damage)

            initialValue = RANGED_MIN_DAMAGE;
        else
            initialValue =  RANGED_RANGE;

        int stageOne = getStageValue(initialValue, upgradeModifierFirst);
        int stageTwo = getStageValue(initialValue, upgradeModifierSecond);

        return returnCurrentStage(currentValue, initialValue, stageOne, stageTwo);
    }
    private static int heavyUnitAttributes(int attributeType, int currentValue)
    {
        //Damage & Health
        int initialValue;
        if (attributeType == attribute_type_damage)

            initialValue = HEAVY_MIN_DAMAGE;
        else
            initialValue =  HEAVY_HEALTH;

        int stageOne = getStageValue(initialValue, upgradeModifierFirst);
        int stageTwo = getStageValue(initialValue, upgradeModifierSecond);

        return returnCurrentStage(currentValue, initialValue, stageOne, stageTwo);
    }


    private static int getStageValue(int initialValue, float modifier)
    {
        float additional = (float) (initialValue * modifier);
        int convertedVal = Mathf.CeilToInt(additional); 
        return convertedVal + initialValue;

    }


    public static int returnCurrentStage(int currentValue, int initial, int stageOne, int stageTwo)
    {
        if (currentValue == initial)
            return initialStage;
        else if (currentValue == stageOne)
            return stage_one;
        else if (currentValue == stageTwo)
            return stage_two;
        else
            return stage_final;
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
        }
        print("Error");
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
        }
        print("Error");
        return 1000;
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
        return 1000;

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
        }
        print("error");
        return "";
    }

  









}

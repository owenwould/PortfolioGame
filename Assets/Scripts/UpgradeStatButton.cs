using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStatButton : MonoBehaviour
{
    [SerializeField] Upgrades upgradeScript;
    [SerializeField] UpgradesUI upgradesUiScript;
    
    bool attributeComplete;
    private void Start()
    {
        attributeComplete = false;
        

       
    }

   
    public void callUpgradeLightHealth()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health, true,gameObject);
    }

    public void callUpgradeLightDamage()
    {
        bool UpgradeSuccess =  upgradeScript.upgradeAttribute(true, constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage, false,gameObject);
    }

    public void callUpgradeMediumDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage, false,gameObject);
    }
    public void callUpgradeMediumSpeed()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed, true,gameObject)  ;
    }

    public void callUpgradeRangedDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.RANGED_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_damage, false,gameObject);
    }
    public void callUpgradeRangedRange()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.RANGED_UNIT_TYPE, constants.attribute_type_range);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_range, true,gameObject);
    }

    public void callUpgradeHeavyHealth()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health, true,gameObject);
    }
    public void callUpgradeHeavyDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradeAttribute(true, constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage, false,gameObject);
    }

    




}

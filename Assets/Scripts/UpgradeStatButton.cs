using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatButton : MonoBehaviour
{
    [SerializeField] Upgrades upgradeScript;
    bool attributeComplete;
    private void Start()
    {
        attributeComplete = false;
       
    }

    //Light Unit Upgrade
    public void callUpgradeLightHealth()
    {
        upgradeScript.upgradeAttribute(true, constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health);
    }

    public void callUpgradeLightDamage()
    {
        upgradeScript.upgradeAttribute(true, constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage);
    }

    public void callUpgradeMediumDamage()
    {
        upgradeScript.upgradeAttribute(true, constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage);
    }
    public void callUpgradeMediumSpeed()
    {
        upgradeScript.upgradeAttribute(true, constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed);
    }

    public void callUpgradeRangedDamage()
    {
        upgradeScript.upgradeAttribute(true, constants.RANGED_UNIT_TYPE, constants.attribute_type_damage);
    }
    public void callUpgradeRangedRange()
    {
        upgradeScript.upgradeAttribute(true, constants.RANGED_UNIT_TYPE, constants.attribute_type_range);
    }

    public void callUpgradeHeavyHealth()
    {
        upgradeScript.upgradeAttribute(true, constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health);
    }
    public void callUpgradeHeavyDamage()
    {
        upgradeScript.upgradeAttribute(true, constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage);
    }

    




}

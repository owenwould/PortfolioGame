
using UnityEngine;
using TMPro;

public class UpgradeStatButton : MonoBehaviour
{
    [SerializeField] Upgrades upgradeScript;
    [SerializeField] UpgradesUI upgradesUiScript;
    TextMeshProUGUI buttonText;
    
  
    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>(); 
    }

 
    public void callUpgradeLightHealth()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.lightHealthKey, constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health, true,gameObject,buttonText);
        
    }

    public void callUpgradeLightDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.lightDamageKey, constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage, false,gameObject,buttonText);
    }

    public void callUpgradeMediumDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.mediumDamageKey, constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage, false,gameObject,buttonText);
    }
    public void callUpgradeMediumSpeed()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.mediumSpeedKey, constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed, true,gameObject,buttonText)  ;
    }

    public void callUpgradeRangedDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.rangeDamageKey, constants.RANGED_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_damage, false,gameObject,buttonText);
    }
    public void callUpgradeRangedRange()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.rangeRangeKey, constants.RANGED_UNIT_TYPE, constants.attribute_type_range);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_range, true,gameObject,buttonText);
    }

    public void callUpgradeHeavyHealth()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.heavyHealthKey, constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health, true,gameObject,buttonText);
    }
    public void callUpgradeHeavyDamage()
    {
        bool UpgradeSuccess = upgradeScript.upgradePlayerAttribute(constants.heavyDamageKey, constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage);
        if (UpgradeSuccess)
            upgradesUiScript.setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage, false,gameObject,buttonText);
    }

   

    




}

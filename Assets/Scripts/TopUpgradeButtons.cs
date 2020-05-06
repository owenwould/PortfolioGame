using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUpgradeButtons : MonoBehaviour
{
    [SerializeField] UpgradesUI upgradesUI;
    
   public void callDisplayLightUpgrades()
   {
        
        upgradesUI.setUpgradeButtons(constants.LIGHT_UNIT_TYPE);
   }
   public void callDisplayMediumUpgrades()
   {
        upgradesUI.setUpgradeButtons(constants.MEDIUM_UNIT_TYPE);
   }
   public void callDisplayRangedUpgrades()
   {
        upgradesUI.setUpgradeButtons(constants.RANGED_UNIT_TYPE);
   }
   public void callDisplayHeavyUpgrades()
   {
        upgradesUI.setUpgradeButtons(constants.HEAVY_UNIT_TYPE);
   }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] UpgradesUI upgradeUIScript;
   public void callUpgradeUI()
    {
        upgradeUIScript.changeUpgradeCanvasState();
    }
}

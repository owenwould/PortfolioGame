using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject lightUpgradeButtons, mediumUpgradeButtons, rangedUpgradeButtons, heavyUpgradeButtons;
    [SerializeField] Image lightButtonImage, mediumButtonImage, rangedButtonImage, heavyButtonImage;
    [SerializeField] Color blue, red;
    [SerializeField] Slider topSlider, bottomSlider;
    [SerializeField] Upgrades upgradeScript;
    [SerializeField] TextMeshProUGUI topUpgradeText, bottomUpgradeText;
    [SerializeField] GameObject lightHealthButton, lightDamageButton, mediumSpeedButton, mediumDamageButton, rangedRangeButton, rangedDamageButton, heavyHealthButton, heavyDamageButton;

   



    public void changeUpgradeCanvasState()
    {
        if (GameManager.gameover || GameManager.mainMenuMode)
            return;

        if (upgradeCanvas.activeSelf)
            upgradeCanvas.SetActive(false);
        else
        {
            upgradeCanvas.SetActive(true);
            setUpgradeButtons(constants.LIGHT_UNIT_TYPE);
        }
            
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            changeUpgradeCanvasState();
        }
    }


    public void setUpgradeButtons(int unitType)
    {
        hideAllbuttons();
        switch (unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                displayButtons(lightUpgradeButtons,constants.LIGHT_UNIT_TYPE);
                setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health, true,lightHealthButton);
                setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage, false,lightDamageButton);
                break;
            case constants.MEDIUM_UNIT_TYPE:
                displayButtons(mediumUpgradeButtons,constants.MEDIUM_UNIT_TYPE);
                setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed, true,mediumSpeedButton);
                setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage, false,mediumDamageButton);
                break;
            case constants.RANGED_UNIT_TYPE:
                displayButtons(rangedUpgradeButtons,constants.RANGED_UNIT_TYPE);
                setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_range, true,rangedRangeButton);
                setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_damage, false,rangedDamageButton);
                break;
            case constants.HEAVY_UNIT_TYPE:
                displayButtons(heavyUpgradeButtons,constants.HEAVY_UNIT_TYPE);
                setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health, true,heavyHealthButton);
                setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage, false,heavyDamageButton);
                break;
        }
    }


    private void hideAllbuttons()
    {
        if (lightUpgradeButtons.activeSelf)
        {
            lightUpgradeButtons.SetActive(false);
            lightButtonImage.color = blue;
        }
        if (mediumUpgradeButtons.activeSelf)
        {
            mediumUpgradeButtons.SetActive(false);
            mediumButtonImage.color = blue;
        }
        if (rangedUpgradeButtons.activeSelf)
        {
            rangedUpgradeButtons.SetActive(false);
            rangedButtonImage.color = blue;
        }
        if (heavyUpgradeButtons.activeSelf)
        {
            heavyUpgradeButtons.SetActive(false);
            heavyButtonImage.color = blue;
        }
          

    }

    private void displayButtons(GameObject buttons, int unitType)
    {
        buttons.SetActive(true);
        switch(unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                lightButtonImage.color = red;
                break;
            case constants.MEDIUM_UNIT_TYPE:
                mediumButtonImage.color = red;
                break;
            case constants.RANGED_UNIT_TYPE:
                rangedButtonImage.color = red;
                break;
            case constants.HEAVY_UNIT_TYPE:
                heavyButtonImage.color = red;
                break;
        }
    }

    public void setCurrentProgress(int unitType,int attributeType,bool isTop,GameObject ButtonObj)
    {
        int currentStage = upgradeScript.returnCurrentProgress(unitType,attributeType);
        string upgradeName;
        int progress;
        if (currentStage == constants.stage_final)
        {
            progress = 100;
            upgradeName = constants.UPGRADE_COMPLETE;

            if (ButtonObj.activeSelf)
               ButtonObj.SetActive(false);

        }
        else
        {


            upgradeName = constants.returnUpgradeText(attributeType);
            progress = currentStage * 33;
        }

        if (isTop)
        {
            topSlider.value = progress;
            topUpgradeText.SetText(upgradeName);
        }
        else
        {
            bottomSlider.value = progress;
            bottomUpgradeText.SetText(upgradeName);
        }
            
    }




}

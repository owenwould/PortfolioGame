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
    TextMeshProUGUI lightHealthText, lightDamageText, mediumSpeedText, mediumDamageText, rangedRangeText, rangedDamageText, heavyHealthText, heavyDamageText;

    private void Start()
    {
        lightHealthText = lightHealthButton.GetComponentInChildren<TextMeshProUGUI>();
        lightDamageText = lightDamageButton.GetComponentInChildren<TextMeshProUGUI>();
        
        mediumSpeedText = mediumSpeedButton.GetComponentInChildren<TextMeshProUGUI>(); 
        mediumDamageText = mediumDamageButton.GetComponentInChildren<TextMeshProUGUI>(); 

        rangedRangeText = rangedRangeButton.GetComponentInChildren<TextMeshProUGUI>();
        rangedDamageText = rangedDamageButton.GetComponentInChildren<TextMeshProUGUI>();
        
        heavyHealthText = heavyHealthButton.GetComponentInChildren<TextMeshProUGUI>();
        heavyDamageText = heavyDamageButton.GetComponentInChildren<TextMeshProUGUI>();

    }


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
                setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_tpye_health, true,lightHealthButton,lightHealthText);
                setCurrentProgress(constants.LIGHT_UNIT_TYPE, constants.attribute_type_damage, false,lightDamageButton,lightDamageText);
                break;
            case constants.MEDIUM_UNIT_TYPE:
                displayButtons(mediumUpgradeButtons,constants.MEDIUM_UNIT_TYPE);
                setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_speed, true,mediumSpeedButton,mediumSpeedText);
                setCurrentProgress(constants.MEDIUM_UNIT_TYPE, constants.attribute_type_damage, false,mediumDamageButton,mediumDamageText);
                break;
            case constants.RANGED_UNIT_TYPE:
                displayButtons(rangedUpgradeButtons,constants.RANGED_UNIT_TYPE);
                setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_range, true,rangedRangeButton,rangedRangeText);
                setCurrentProgress(constants.RANGED_UNIT_TYPE, constants.attribute_type_damage, false,rangedDamageButton,rangedDamageText);
                break;
            case constants.HEAVY_UNIT_TYPE:
                displayButtons(heavyUpgradeButtons,constants.HEAVY_UNIT_TYPE);
                setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_tpye_health, true,heavyHealthButton,heavyHealthText);
                setCurrentProgress(constants.HEAVY_UNIT_TYPE, constants.attribute_type_damage, false,heavyDamageButton,heavyDamageText);
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

    public void setCurrentProgress(int unitType,int attributeType,bool isTop,GameObject ButtonObj,TextMeshProUGUI buttonText)
    {

        int upgradeKey = constants.getUpgradeKey(unitType, attributeType);
        int currentStage = upgradeScript.returnCurrentProgress(upgradeKey);
        string upgradeName;
        int progress;

        setButtonText(buttonText, currentStage);

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
    private void setButtonText(TextMeshProUGUI buttonText,int currentStage)
    {
        if (currentStage == constants.stage_final)
            return;
        string cost = constants.returnUpgradeCost(currentStage).ToString();
        buttonText.SetText(cost);
    }




}

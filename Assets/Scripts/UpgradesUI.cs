using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject lightUpgradeButtons, mediumUpgradeButtons, rangedUpgradeButtons, heavyUpgradeButtons;
    [SerializeField] Image lightButtonImage, mediumButtonImage, rangedButtonImage, heavyButtonImage;
    [SerializeField] Color blue, red;

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
                break;
            case constants.MEDIUM_UNIT_TYPE:
                displayButtons(mediumUpgradeButtons,constants.MEDIUM_UNIT_TYPE);
                break;
            case constants.RANGED_UNIT_TYPE:
                displayButtons(rangedUpgradeButtons,constants.RANGED_UNIT_TYPE);
                break;
            case constants.HEAVY_UNIT_TYPE:
                displayButtons(heavyUpgradeButtons,constants.HEAVY_UNIT_TYPE);
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




}

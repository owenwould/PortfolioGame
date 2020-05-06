using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldTextVal,unitTextVal;
    [SerializeField] GameManager gameManager;
    [SerializeField] MainMenuMode mainMenuScript;
    [SerializeField] SpawnCharacter spawnCharacterScript;
    [SerializeField] TextMeshProUGUI gameoverText;
    [SerializeField] GameObject gameoverUI,upgradesUI;
 
    const string sUnitDefault = "/10";
    public const int LIGHT_UNIT_COST = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        goldTextVal.SetText("0");
        unitTextVal.SetText("0");
        mainMenuScript.setPlayingUI(false);
        setCameraFocus(false);
    }

    // Update is called once per frame
    

    public void setUnitCount(int iSize)
    {
        unitTextVal.SetText(iSize + sUnitDefault);
    }
    public void setGoldValue(int value)
    {
        goldTextVal.SetText(value.ToString());
    }

    public void beginGame(int playerGold)
    {
        mainMenuScript.setPlayingUI(true);
        setCameraFocus(false);
        setGoldValue(playerGold);
        setUnitCount(0);
    }
    public void setCameraFocus(bool isLocked)
    {
        if (isLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
    public void gameover(bool isPlayer)
    {
        upgradesUI.SetActive(false);
        spawnCharacterScript.GameOver();
        gameoverUI.SetActive(true);

        if (isPlayer)
            gameoverText.SetText("Gameover \n\n Player Won");
        else
            gameoverText.SetText("Gameover \n\n Enemy Won");


    }

    public void displayMainMenu()
    {

        mainMenuScript.setPlayingUI(false);
        gameoverUI.SetActive(false);
    }




}

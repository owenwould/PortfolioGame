using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldTextVal,unitTextVal;
    [SerializeField] GameManager gameManager;
    [SerializeField] MainMenuMode mainMenuScript;
    [SerializeField] SpawnCharacter spawnCharacterScript;
    [SerializeField] TextMeshProUGUI gameoverText;
    [SerializeField] GameObject gameoverUI,upgradesUI,playingUI;
    bool lightDelay, mediumDelay, rangeDelay, heavyDelay;
    [SerializeField] Image progressLight, progressRange, progressMedium, progressHeavy;
    [SerializeField] TextMeshProUGUI lightSpawnCount, rangeSpawnCount, mediumSpawnCount, heavySpawnCount;
    List<int> lightQueue, rangedQueue, mediumQueue, heavyQueue;
    [SerializeField] muteButton muteButtonScript;

 
    const string sUnitDefault = "/10";
    public const int LIGHT_UNIT_COST = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        goldTextVal.SetText("0");
        unitTextVal.SetText("0");
        mainMenuScript.setPlayingUI(false);
        setCameraFocus(false);
        lightDelay = false;
        mediumDelay = false;
        rangeDelay = false;
        heavyDelay = false;

        lightQueue = new List<int>();
        rangedQueue = new List<int>();
        mediumQueue = new List<int>();
        heavyQueue = new List<int>();
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
        muteButtonScript.setMutedButton();
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
        playingUI.SetActive(false);
        upgradesUI.SetActive(false);
        spawnCharacterScript.GameOver();
        gameoverUI.SetActive(true);
       
        

        if (isPlayer)
            gameoverText.SetText("Gameover Player Won");
        else
            gameoverText.SetText("Gameover Enemy Won");


    }

    public void displayMainMenu()
    {
        gameManager.stopMusic();
        mainMenuScript.setPlayingUI(false);
        gameoverUI.SetActive(false);
    }


    public void setTimer(int unitType)
    {
        switch (unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                if (!lightDelay)
                {
                    setDelayState(unitType, true);
                    StartCoroutine(delayProgress(constants.LIGHT_UNIT_WAIT, progressLight,false,lightSpawnCount,unitType));
                }
                else
                {
                    lightQueue.Add(0);
                    updateSpawnCountText(lightQueue, lightSpawnCount);
                }
                break;
            case constants.RANGED_UNIT_TYPE:
                if (!rangeDelay)
                {
                    setDelayState(unitType, true);
                    StartCoroutine(delayProgress(constants.RANGE_UNIT_WAIT, progressRange, false, rangeSpawnCount, unitType));
                }
                else
                {
                    rangedQueue.Add(0);
                    updateSpawnCountText(rangedQueue, rangeSpawnCount);
                }
                break;
            case constants.MEDIUM_UNIT_TYPE:
                if (!mediumDelay)
                {
                    setDelayState(unitType, true);
                    StartCoroutine(delayProgress(constants.MEDIUM_UNIT_WAIT, progressMedium, false, mediumSpawnCount, unitType));
                }
                else
                {
                    mediumQueue.Add(0);
                    updateSpawnCountText(mediumQueue, mediumSpawnCount);
                }
                break;
            case constants.HEAVY_UNIT_TYPE:
                if (!heavyDelay)
                {
                    setDelayState(unitType, true);
                    StartCoroutine(delayProgress(constants.HEAVY_UNIT_WAIT, progressHeavy, false, heavySpawnCount, unitType));
                }
                else
                {
                    heavyQueue.Add(0);
                    updateSpawnCountText(heavyQueue, heavySpawnCount);
                }
                break;
        }
    }


    private IEnumerator delayProgress(float delay,Image image,bool multiple,TextMeshProUGUI unitSpawnCount,int unitType)
    {
        if (multiple)
        {
            string countText = "";
            int count = returnUnitSpawnCount(unitType);
            if (count > 0)
                countText = count.ToString();
            unitSpawnCount.SetText(countText);
        }

        image.fillAmount = 0;
        float tenPercent =  delay / 10f;

        float correctedDelay = (delay /tenPercent ) + 1;

       
        image.enabled = true;
        for (float i=1; i < correctedDelay; i++)
        {
            image.enabled = true;
            yield return new WaitForSeconds(tenPercent);
            image.fillAmount = (i * tenPercent)/delay;
        
        }

       
        if (multiple)
            removeListElement(unitType);

        if (returnUnitSpawnCount(unitType) > 0)
            StartCoroutine(delayProgress(delay, image, true, unitSpawnCount, unitType));
        else
        {
            unitSpawnCount.SetText("");
            image.enabled = false;
            setDelayState(unitType, false);
        }
           
    }

    private void setDelayState(int unitType,bool state)
    {
        switch (unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                lightDelay = state;
                break;
            case constants.RANGED_UNIT_TYPE:
                rangeDelay =state;
                break;
            case constants.MEDIUM_UNIT_TYPE:
                mediumDelay = state;
                break;
            case constants.HEAVY_UNIT_TYPE:
                heavyDelay = state;
                break;
        }

    }

    private int returnUnitSpawnCount(int unitType)
    {
        switch (unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
               return lightQueue.Count;
            case constants.RANGED_UNIT_TYPE:
                return rangedQueue.Count;
            case constants.MEDIUM_UNIT_TYPE:
                return mediumQueue.Count;
            case constants.HEAVY_UNIT_TYPE:
                return heavyQueue.Count;
            default:
                return 0;
        }
    }
    private void removeListElement(int unitType)
    {
        switch (unitType)
        {
            case constants.LIGHT_UNIT_TYPE:
                lightQueue.RemoveAt(0);
                break;
            case constants.RANGED_UNIT_TYPE:
                rangedQueue.RemoveAt(0);
                break;
            case constants.MEDIUM_UNIT_TYPE:
                mediumQueue.RemoveAt(0);
                break;
            case constants.HEAVY_UNIT_TYPE:
                heavyQueue.RemoveAt(0);
                break;
        }
    }

    private void updateSpawnCountText(List<int> queueList,TextMeshProUGUI spawnCount)
    {
        string count = queueList.Count.ToString();
        spawnCount.SetText(count);
    }

    










}

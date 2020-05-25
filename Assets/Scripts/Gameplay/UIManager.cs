using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldTextVal,unitTextVal;
    [SerializeField] GameManager gameManager;
    [SerializeField] SpawnCharacter spawnCharacterScript;
    [SerializeField] TextMeshProUGUI gameoverText,goldSpentText,enemiesKilledText,unitSpawnedText,timeTakenText;
    [SerializeField] GameObject gameoverUI,upgradesUI,playingUI,helpMenuUI,mainMenuUI;
    bool lightDelay, mediumDelay, rangeDelay, heavyDelay;
    [SerializeField] Image progressLight, progressRange, progressMedium, progressHeavy;
    [SerializeField] TextMeshProUGUI lightSpawnCount, rangeSpawnCount, mediumSpawnCount, heavySpawnCount;
    List<int> lightQueue, rangedQueue, mediumQueue, heavyQueue;
    [SerializeField] muteButton muteButtonScript;
    [SerializeField] StatsTracker statsTracker;
    [SerializeField] Transform defualtCamPos, helpScreenCameraPos;
    [SerializeField] CameraMovement cameraMovement;
 
    private const string sUnitDefault = "/10";
    
    
    void Start()
    {
        setValues();
    }
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
        setValues();
        mainMenuUI.SetActive(false);
        playingUI.SetActive(true);
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
    public void gameover(bool isPlayer,string time)
    {
        playingUI.SetActive(false);
        upgradesUI.SetActive(false);
        spawnCharacterScript.GameOver();
        gameoverUI.SetActive(true);

        setCameraPosition(helpScreenCameraPos.position);

        if (isPlayer)
            gameoverText.SetText("Gameover Player Won");
        else
            gameoverText.SetText("Gameover Enemy Won");

        gameOverStats(time);
    }

    private void gameOverStats(string time)
    {
        timeTakenText.SetText(constants.timeTakenText + " " +  time);
        goldSpentText.SetText(constants.goldSpentText + "  " + statsTracker.getGoldSpent());
        enemiesKilledText.SetText(constants.enemiesKilledText + "  " + statsTracker.getEnemiesKilled());
        unitSpawnedText.SetText(constants.friendlySpawnText + "  " + statsTracker.getPlayerUnitSpawned());
    }

      
    public void disableAudio()
    {
        gameManager.stopMusic();
    }

    public void displayMainMenu()
    {
        if (helpMenuUI.activeSelf)
            helpMenuUI.SetActive(false);

        setCameraPosition(defualtCamPos.position);
        mainMenuUI.SetActive(true);
        playingUI.SetActive(false);
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

    private void setValues()
    {

        goldTextVal.SetText("0");
        unitTextVal.SetText("0");
        displayMainMenu();
        setCameraFocus(false);
        lightDelay = false;
        mediumDelay = false;
        rangeDelay = false;
        heavyDelay = false;

        lightQueue = new List<int>();
        rangedQueue = new List<int>();
        mediumQueue = new List<int>();
        heavyQueue = new List<int>();


        gameoverUI.SetActive(true);
        timeTakenText.SetText("");
        goldSpentText.SetText("");
        enemiesKilledText.SetText("");
        unitSpawnedText.SetText("");
        gameoverUI.SetActive(false);
    }

    private void setCameraPosition(Vector3 destination)
    {
        cameraMovement.setCameraPos(destination);
    }

    public void returnCameraPosToDefault()
    {
        setCameraPosition(defualtCamPos.position);
    }
    public void displayHelpMenuUI()
    {
        mainMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);
        setCameraPosition(helpScreenCameraPos.position);
    }
}

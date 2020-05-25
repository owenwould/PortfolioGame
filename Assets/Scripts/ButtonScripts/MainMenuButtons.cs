using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    public void displayHelpUI()
    {
        uiManager.displayHelpMenuUI();
    }
    public void returnToMainMenu()
    {
        uiManager.displayMainMenu();
    }
    public void quitGame()
    {
        Application.Quit();
    }
}

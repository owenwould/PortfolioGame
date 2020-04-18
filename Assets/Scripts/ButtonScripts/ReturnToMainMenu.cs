using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    public void returnToMainMenu()
    {
        uiManager.displayMainMenu();
    }
}

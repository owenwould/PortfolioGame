using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
   
    
    public void startGame()
    {
        gameManager.beginGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMode : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI, PlayingUI;
    void Start()
    {
        
    }

    // Update is called once per frame
   public void setPlayingUI(bool isPlaying)
    {
        if (isPlaying)
        {
            MainMenuUI.SetActive(false);
            PlayingUI.SetActive(true);
        }
        else
        {
            MainMenuUI.SetActive(true);
            PlayingUI.SetActive(false);
        }

    }
}

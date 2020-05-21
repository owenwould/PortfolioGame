using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class muteButton : MonoBehaviour
{
    [SerializeField] GameObject muted, mute;
    [SerializeField] GameManager gameManager;

    bool isMuted;
    void Start()
    {
        isMuted = false;
    }

   
    public void setMutedButton()
    {
        isMuted = false;
        mute.SetActive(true);
        muted.SetActive(false);
    }

    public void changeMusicState()
    {
        if (GameManager.gameover)
            return;

        if (isMuted)
        {
            //Un Mute 
            mute.SetActive(true);
            muted.SetActive(false);
            isMuted = false;
            gameManager.startMusic();

        }
        else
        {
            mute.SetActive(false);
            muted.SetActive(true);
            isMuted = true;
            gameManager.stopMusic();

        }

    }
}

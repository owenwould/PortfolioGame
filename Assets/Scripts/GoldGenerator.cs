using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGenerator : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    int GOLD_VALUE = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGoldGen()
    {
        StartCoroutine(goldGenCoroutine());
    }
    public void stopGoldGen()
    {
        StopAllCoroutines();
    }


    IEnumerator goldGenCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameManager.increaseGold(GOLD_VALUE, true); //For Player
        gameManager.increaseGold(GOLD_VALUE*2, false); //For Enemy
        StartCoroutine(goldGenCoroutine());
    }
}

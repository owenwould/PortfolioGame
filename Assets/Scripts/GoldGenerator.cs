using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGenerator : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    int GOLD_VALUE = 10;
    void Start()
    {
        StartCoroutine(goldGenCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator goldGenCoroutine()
    {
        yield return new WaitForSeconds(1f);
        uiManager.increaseGoldValue(GOLD_VALUE);
        StartCoroutine(goldGenCoroutine());
    }
}

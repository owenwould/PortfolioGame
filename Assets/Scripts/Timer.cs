using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    float seconds;
    public void startTimer()
    {
        seconds = 0f;
        StartCoroutine(waitASecond());
    }

    private IEnumerator waitASecond()
    {
        yield return new WaitForSeconds(1);
        seconds++;
        StartCoroutine(waitASecond());
    }


    public void stopTimer()
    {
        StopAllCoroutines();
    }

    public float getSeconds()
    {
        return seconds;
    }
}

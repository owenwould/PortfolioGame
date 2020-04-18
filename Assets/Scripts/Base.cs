using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    int baseHealth;
    [SerializeField] Slider healthSlider;
    // Start is called before the first frame update
   
    public void startGame()
    {
        baseHealth = 1000;
        healthSlider.maxValue = baseHealth;
        healthSlider.value = baseHealth;
    }
    // Update is called once per frame
    



   


    public int reduceHealth(int damage)
    {
        baseHealth -= damage;
        healthSlider.value = baseHealth;
        return baseHealth;
        
    }




}

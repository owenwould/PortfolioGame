using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    int baseHealth;
    [SerializeField] Slider healthSlider;
    public void startGame()
    {
        baseHealth = constants.BASE_HEALTH;
        healthSlider.maxValue = baseHealth;
        healthSlider.value = baseHealth;
    }
    public int reduceHealth(int damage)
    {
        baseHealth -= damage;
        healthSlider.value = baseHealth;
        return baseHealth;
        
    }
    public int getHealth()
    {
        return baseHealth;
    }
}

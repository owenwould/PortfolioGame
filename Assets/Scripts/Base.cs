using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    int baseHealth;
    [SerializeField] Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = 1000;
        healthSlider.maxValue = baseHealth;
        healthSlider.value = baseHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }



   


    public void reduceHealth(int damage)
    {
        baseHealth -= damage;
        healthSlider.value = baseHealth;
        
    }
}

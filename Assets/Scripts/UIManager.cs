using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]  TextMeshProUGUI goldTextVal,unitTextVal;
    [SerializeField] GameManager gameManager;
 
    const string sUnitDefault = "/10";
    public const int LIGHT_UNIT_COST = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        goldTextVal.SetText("0");
        unitTextVal.SetText("0");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUnitCount(int iSize)
    {
        unitTextVal.SetText(iSize + sUnitDefault);
    }
    public void setGoldValue(int value)
    {
        goldTextVal.SetText(value.ToString());
    }
    
  
}

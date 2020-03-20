using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]  TextMeshProUGUI goldTextVal,unitTextVal;
    public static int goldCount, unitCount;
    const string sUnitDefault = "/10";
    public const int LIGHT_UNIT_COST = 100;
    // Start is called before the first frame update
    void Start()
    {
        goldCount = 0;
        unitCount = 0;

        goldTextVal.SetText(goldCount.ToString());
        unitTextVal.SetText(unitCount.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseUnitCount()
    {
        unitCount++;
        if (unitCount > 10)
        {
            unitCount = 10;
        }
        unitTextVal.SetText(unitCount.ToString() + sUnitDefault);

    }

    public void increaseGoldValue(int value)
    {
        goldCount += value;
        goldTextVal.SetText(goldCount.ToString());
    }
    public void decreaseGoldValue(int value)
    {
        goldCount -= value;
        goldTextVal.text = goldCount.ToString();

    }
  



  
}

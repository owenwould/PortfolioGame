using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttributes : MonoBehaviour
{
    List<int> upgradableAttributes;
    int initialValue;
    int firstUpgradeValue, secondUpgradeValue, finalUpgradeValue;

    public void setAttributeList(List<int> attributeList, int initialVal, int firstVal, int secondVal, int finalVal)
    {
        upgradableAttributes = attributeList;
    }

    public bool containsAttribute(int type)
    {
        for (int i = 0; i < upgradableAttributes.Count; i++)
        {
            if (upgradableAttributes[i] == type)
                return true;

        }
        return false;
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRange : MonoBehaviour
{

    string type;
    private void Start()
    {
        detectType();
    }

    private void detectType()
    {
        //If On Players side then update enemy range 

        if (transform.CompareTag(constants.playerTag))
            type = constants.enemyMaskName;
        else
            type = constants.playerMaskName;
    }


    private void OnTriggerStay(Collider other)
    {
       if ((other.gameObject.layer == LayerMask.NameToLayer(type) ))
       {
            other.GetComponent<Unit>().updateRange();
       }
    }


}

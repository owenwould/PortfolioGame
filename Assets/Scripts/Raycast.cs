using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private const string enemyMaskName = "EnemyArmyLayer", playerMaskName = "PlayerArmyLayer";
    private const string enemyTag = "Enemy", playerTag = "Player",baseTag = "Base";
    LayerMask friendlyMask, enemyMask;
    bool attackRunning = false;

    void Start()
    {
        if (transform.CompareTag(playerTag))
        {        
            friendlyMask = LayerMask.GetMask(playerMaskName);
            enemyMask = LayerMask.GetMask(enemyMaskName);
            
        }
        else if (transform.CompareTag(enemyTag))
        {
            friendlyMask = LayerMask.GetMask(enemyMaskName);
            enemyMask = LayerMask.GetMask(playerMaskName);
          
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        raymeme();
    }

    void raymeme()
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.right;
        Debug.DrawRay(origin, transform.right * 10,Color.red);

        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray,out RaycastHit hit, 5,enemyMask))
        {
            if (hit.collider.CompareTag(baseTag))
            {
                if (!attackRunning)
                {
                    StartCoroutine(attack());
                    hit.collider.gameObject.GetComponent<Base>().reduceHealth(10);
                }
             
            }
        }
    }

    IEnumerator attack()
    {
        attackRunning = true;
        yield return new WaitForSeconds(2);
        attackRunning = false;
    }
}

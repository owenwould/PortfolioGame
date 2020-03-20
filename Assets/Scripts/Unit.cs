using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Transform targetTran;
    Vector3 targetVec;
    
    bool bPlayerArmy,attackRunning = false;
    LayerMask friendlyMask, enemyMask;
    public bool enabledUnit = true;
    
    int health, speed;

    void Start()
    {
        setType();
        speed = 5;
        health = 100;
        targetVec = new Vector3(targetTran.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (enabledUnit)
        {
            takeAction();
        }
        
    }

    void takeAction()
    {
        if (detectEnemy())
        {

        }
        else if (detectFriendly())
        {

        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetVec, step);
        }

    }

    private bool detectEnemy()
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.right;
        Debug.DrawRay(origin, transform.right * 10, Color.red);

        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, 1, enemyMask))
        {
            if (hit.collider.CompareTag(constants.baseTag))
            {
                return true;
            }
            else
            {
                if (!attackRunning)
                {
                    StartCoroutine(attackAction());
                }

                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private bool detectFriendly()
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.right;
        Debug.DrawRay(origin, transform.right * 10, Color.red);

        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, 2, friendlyMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void setType()
    {
        if (transform.CompareTag(constants.playerTag))
        {
            friendlyMask = LayerMask.GetMask(constants.playerMaskName);
            enemyMask = LayerMask.GetMask(constants.enemyMaskName);
            bPlayerArmy = true;

        }
        else if (transform.CompareTag(constants.enemyTag))
        {
            friendlyMask = LayerMask.GetMask(constants.enemyMaskName);
            enemyMask = LayerMask.GetMask(constants.playerMaskName);
            bPlayerArmy = false;

        }
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health < 1)
        {
            Singleton.instance.removeUnit(bPlayerArmy);
            destroyUnit();
        }
    }


    private IEnumerator attackAction()
    {
        attackRunning = true;
        yield return new WaitForSeconds(1);
        if (bPlayerArmy)
        {
            Singleton.instance.attackUnit(10, true);
        }
        else
        {
            Singleton.instance.attackUnit(10,false);
        }
        yield return new WaitForSeconds(0.1f);
        //Give time to prevent an attack on dead enemy
        attackRunning = false;
    } 


    public void destroyUnit()
    {
        enabledUnit = false;
        StopAllCoroutines();
        attackRunning = false;
        Destroy(gameObject);
    }
}

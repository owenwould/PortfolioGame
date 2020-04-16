using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    private Vector3 targetVec;
    private bool bPlayerArmy,attackRunning = false;
    private LayerMask friendlyMask, enemyMask;
    public bool enabledUnit = false; 
    private bool isAttacking = false;
    private int health,damage;
    private float damageDelay, range, speed;
    private int entityType;
    private Animator anim;
   

  
    public void setUnit(float targetX,bool isPlayer,int type)
    {
        setType(isPlayer);
        bPlayerArmy = isPlayer;
        entityType = type;
        targetVec = new Vector3(targetX, transform.position.y, transform.position.z);
        enabledUnit = true;
       // anim = GetComponent<Animator>();
       // anim.SetBool(constants.isAttacking, false);
    }
    public void setUnitAttributes(int health, int damage, float damageDelay, float range, float speed)
    {
        this.health = health;
        this.damage = damage;
        this.damageDelay = damageDelay;
        this.range = range;
        this.speed = speed;
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
            //anim.SetBool(constants.isAttacking, true);
            return;
        }
        else if (detectFriendly())
        {

            //anim.SetBool(constants.isRunning, false);

        }
        else
        {
            //anim.SetBool(constants.isRunning, true);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetVec, step);
        }

    }

    private bool detectEnemy()
    {
        
        Vector3 origin = transform.position;
        //Note that player is rotated for animation therefore both raycasts are -right
        Vector3 dir = -transform.right; 
       


        Debug.DrawRay(origin, dir * 2, Color.red);

        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, range, enemyMask))
        {
            isAttacking = true;
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
            if (isAttacking)
                // anim.SetBool(constants.isAttacking, false);
                isAttacking = false;
            return false;
        }
    }

    private bool detectFriendly()
    {
        Vector3 origin = transform.position;
        Vector3 dir = -transform.right;
        

        //Debug.DrawRay(origin, dir * 2, Color.red);
        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, 2, friendlyMask))
        {
            if (!hit.collider.CompareTag(constants.baseTag))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    void setType(bool isPlayer)
    {
     
        if (isPlayer)
        {
            friendlyMask = LayerMask.GetMask(constants.playerMaskName);
            enemyMask = LayerMask.GetMask(constants.enemyMaskName);
            gameObject.layer = LayerMask.NameToLayer(constants.playerMaskName);
            transform.localEulerAngles = new Vector3(0, -180, 0);
        }
        else 
        {
            friendlyMask = LayerMask.GetMask(constants.enemyMaskName);
            enemyMask = LayerMask.GetMask(constants.playerMaskName);
            gameObject.layer = LayerMask.NameToLayer(constants.enemyMaskName);

        }
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health < 1)
        {
            Singleton.instance.removeUnit(bPlayerArmy,entityType);
            destroyUnit();
        }
    }


    private IEnumerator attackAction()
    {
        attackRunning = true;
        yield return new WaitForSeconds(damageDelay);
        Singleton.instance.attackUnit(returnDamage(), bPlayerArmy);
        yield return new WaitForSeconds(0.1f);
        //Give time to prevent an attack on dead enemy
        attackRunning = false;
    } 

    private int returnDamage()
    {
        float rng = damage/4;
        int min = (int)(damage - rng);
        int max = (int)(damage + rng);
        int generateDamage = Random.Range(min, max + 1);
        print(generateDamage);
        return generateDamage;
    }


    public void destroyUnit()
    {
        enabledUnit = false;
        StopAllCoroutines();
        attackRunning = false;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    private Vector3 targetVec;
    private bool isPlayer,attackRunning = false;
    private LayerMask friendlyMask, enemyMask;
    public bool enabledUnit = false; 
    private bool isAttacking = false;
    private int health,minDamage,maxDamage,range,speed;
    private float damageDelay;
    private int friendlyRaycastRange;
    Transform unitTransform;
  
    private int entityType;
    private Animator anim;
    private bool isRangedUnit,friendlyInFront,enemyInFront;
    private Vector3 origin, dir;
    private bool canMove;
    private long unitID;
    Vector3 healthBarVec;
    Transform healthBar;
    float onePercent;



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        origin = new Vector3(0, 0, 0);
        dir = new Vector3(0, 0, 0);
        unitTransform = GetComponent<Transform>();
        setHealthBar();
       
    }

    public void setUnit(float targetX,bool isPlayer,int type,long unitID)
    {
        enabledUnit = true;
        setType(isPlayer);
        this.isPlayer = isPlayer;
        entityType = type;
        targetVec = new Vector3(targetX, transform.position.y, transform.position.z);
        anim.SetBool(constants.isAttacking, false);

        if (entityType == constants.RANGED_UNIT_TYPE)
            isRangedUnit = true;
        else
            isRangedUnit = false;

        this.unitID = unitID;
        StartCoroutine(wait());
        healthBar.localScale = healthBarVec;

    }
    public void setUnitAttributes(int health, int minDamage,int maxDamage ,float damageDelay, int range, int speed)
    {
        this.health = health;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.damageDelay = damageDelay;
        this.range = range;
        this.speed = speed;
        friendlyRaycastRange = constants.firendlyDetectionRange;
        canMove = false;

        onePercent = (float)(constants.healthBarConstant / (float)health);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameover)
        {
            if (enabledUnit)
                takeAction();
        }
    }

    void takeAction()
    {
        if (detectEnemy())
        {
            anim.SetBool(constants.isAttacking, true);
            if (isRangedUnit)
            {
                //Prevents  other units being help up when archer gets into range of base to attack 
                if (!canMove)
                    return;


                enemyInFront = false;
                friendlyInFront = false;
                //Check if friendly is in front & check if enemy is in front 
                origin = unitTransform.position;
                dir = -unitTransform.right;

                Ray ray = new Ray(origin, dir);
                if (Physics.Raycast(ray, out RaycastHit hitE, friendlyRaycastRange, enemyMask))
                {
                    enemyInFront = true;
                }

                if (Physics.Raycast(ray, out RaycastHit hitF, friendlyRaycastRange, friendlyMask))
                {
                    if (!hitF.collider.CompareTag(constants.baseTag))

                        friendlyInFront = true;
                }

                if (!friendlyInFront && !enemyInFront)
                {
                    anim.SetBool(constants.isRunning, true);
                    float step = (float)speed * Time.deltaTime;
                    unitTransform.position = Vector3.MoveTowards(transform.position, targetVec, step);
                    return;
                }
            }

         
            return;
        }
        else if  (detectFriendly())
        {
            anim.SetBool(constants.isRunning, false);
            
        }
        else
        {
            if (!canMove)
                return;

            anim.SetBool(constants.isRunning, true);
            float step = (float) speed * Time.deltaTime;
            unitTransform.position = Vector3.MoveTowards(transform.position, targetVec, step);
        }






    }

    private bool detectEnemy()
    {

        origin = unitTransform.position;
        //Note that player is rotated for animation therefore both raycasts are -right
        dir = -unitTransform.right; 
       

        Debug.DrawRay(origin, dir * 2, Color.red);

        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, range, enemyMask))
        {
            isAttacking = true;
            if (!hit.collider.CompareTag(constants.baseTag))
            {
                if (!attackRunning)
                    StartCoroutine(attackAction());
                return true;
            }
            else
            {
                if (!attackRunning)
                    StartCoroutine(attackAction(isPlayer));
                return true;
            }
        }
        else
        {
            if (isAttacking)
                anim.SetBool(constants.isAttacking, false);
                isAttacking = false;
            return false;
        }
    }

    private bool detectFriendly()
    {
        origin = unitTransform.position;
        dir = -unitTransform.right;
        Ray ray = new Ray(origin, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, friendlyRaycastRange, friendlyMask))
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
            Singleton.instance.removeUnit(isPlayer, entityType);
            destroyUnit();
        }
        else
            alterHealthBar();
    }
    private void alterHealthBar()
    {
        healthBarVec.x = (float)health * onePercent;
        healthBar.localScale = healthBarVec;
    }

    private IEnumerator attackAction()
    {
        attackRunning = true;
        yield return new WaitForSeconds(damageDelay);

        if (health > 1)
        {
            Singleton.instance.attackUnit(returnDamage(), isPlayer,entityType);
            yield return new WaitForSeconds(0.1f);
        }
        //Give time to prevent an attack on dead enemy
        attackRunning = false;
    } 
    private IEnumerator attackAction(bool isPlayer)
    {
        attackRunning = true;
        yield return new WaitForSeconds(damageDelay);
        if (health > 0)
        {
            Singleton.instance.attackBase(isPlayer, returnDamage());
            yield return new WaitForSeconds(0.1f);
        }
        //Give time to prevent an attack on dead enemy
        attackRunning = false;
    }

    private int returnDamage()
    {
        int generateDamage = Random.Range(minDamage, maxDamage + 1);
        return generateDamage;
    }

    public void destroyUnit()
    {
        enabledUnit = false;
        StopAllCoroutines();
        attackRunning = false;
        Destroy(gameObject);
    }

    public float getXPosition()
    {
        return unitTransform.position.x;
    }
    public bool getIsAttacking()
    {
        return isAttacking;
    }
    public int getUnitType()
    {
        return entityType;
    }

    public void setIdle()
    {
        anim.SetBool(constants.isAttacking, false);
        anim.SetBool(constants.isRunning, false);
    }

    IEnumerator wait()
    {
     //Prevents Unit getting such on each other when spawned in quick sucession and when the enemy has pressed the 
     //Player to their base (can still attack just cant move), prevents them going in front of units that were spawned before
        yield return new WaitForSeconds(0.1f);

        bool tempCanMove = Singleton.instance.canUnitMove(isPlayer, unitID);
        if (tempCanMove)
            canMove = true;
        else
            StartCoroutine(wait());
    }


    public long getUnitID()
    {
        return unitID;
    }

    private void setHealthBar()
    {
        foreach (Transform child  in transform)
        {
            if (child.gameObject.CompareTag(constants.healthBarTag))
            {
                healthBar = child;
                healthBarVec = healthBar.localScale;
            }
        }
    }
}

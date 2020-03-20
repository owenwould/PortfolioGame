using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]List<Unit> playerArmy;
    [SerializeField] List<Unit> enemyArmy;
    void Start()
    {
        //playerArmy = new List<Unit>(10);
        //enemyArmy = new List<Unit>(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    void addToPlayerArmy(Unit unit)
    {
        playerArmy.Add(unit);
    }

    void addToEnemyArmy(Unit unit)
    {
        enemyArmy.Add(unit);
    }
    

    public void removeUnit(bool isPlayersUnit)
    {
        if (isPlayersUnit)
        {
           
            playerArmy.RemoveAt(0);
            
        }
        else
        {
            enemyArmy.RemoveAt(0);
        }
    }

    public void attackUnit(int damage, bool isPlayersUnit)
    {
        if (isPlayersUnit)
        {
            enemyArmy[0].takeDamage(damage);
        }
        else
        {
            playerArmy[0].takeDamage(damage);
            
        }
    }

    
}

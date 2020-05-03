using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;
    [SerializeField] GameManager gamemanger;
   
    private void Awake()
    {
        instance = this;
    }
    public void attackUnit(int damage,bool isPlayers,int unitType)
    {
        gamemanger.attackUnit(damage, isPlayers,unitType);
    }
    public void removeUnit(bool isPlayers,int iEntityType)
    {
        gamemanger.removeUnit(isPlayers,iEntityType);
    }
    public void attackBase(bool isPlayer,int damage)
    {
        gamemanger.attackBase(isPlayer, damage);
    }



}

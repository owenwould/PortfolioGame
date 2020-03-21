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
    public void attackUnit(int damage,bool isPlayers)
    {
        gamemanger.attackUnit(damage, isPlayers);
    }
    public void removeUnit(bool isPlayers,int iEntityType)
    {
        gamemanger.removeUnit(isPlayers,iEntityType);
    }

}

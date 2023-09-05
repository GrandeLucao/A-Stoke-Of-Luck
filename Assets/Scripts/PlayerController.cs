using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerController : ScriptableObject
{   
    [SerializeField] 
    private int _mapNum;
    public int MapNum
    {
        get { return _mapNum; }
        set { _mapNum = value; }
    }
    
    [SerializeField] 
    private int _PlayaLife;
    public int PlayaLife
    {
        get { return _PlayaLife; }
        set { _PlayaLife = value; }
    }

    [SerializeField] 
    private int _PlayaDamage;
    public int PlayaDamage
    {
        get { return _PlayaDamage; }
        set { _PlayaDamage = value; }
    }

    [SerializeField]
    private int _PlayaLevel;
    public int PlayaLevel
    {
        get { return _PlayaLevel; }
        set { _PlayaLevel = value; }
    }

    [SerializeField]
    private int _PlayaDefense;
    public int PlayaDefense
    {
        get { return _PlayaDefense; }
        set { _PlayaDefense = value; }
    }

    [SerializeField]
    private int _PlayaLuck;
    public int PlayaLuck
    {
        get { return _PlayaLuck; }
        set { _PlayaLuck = value; }
    }

    [SerializeField]
    private int _PlayaDex;
    public int PlayaDex
    {
        get { return _PlayaDex; }
        set { _PlayaDex = value; }
    }
    
    [SerializeField]
    private int _PlayaCurrentHp;
    public int PlayaCurrentHp
    {
        get { return _PlayaCurrentHp; }
        set { _PlayaCurrentHp = value; }
    }

    [SerializeField]
    private int _Potions;
    public int Potions
    {
        get { return _Potions; }
        set { _Potions = value; }
    }
    
    
    
    
    
    
    
    


}

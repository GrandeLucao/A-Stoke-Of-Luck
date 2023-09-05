using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random= UnityEngine.Random;

public class CharUnit : MonoBehaviour
{
    [SerializeField]
    private PlayerController PlayerStats;
    private Animator anim;


    public int playaDamage, playaDex, playaLuck, playaDefense,playaCurrentHP,playaMaxHP,playaLevel;
    public string playaName;

    void Start()
    {
        playaDamage=PlayerStats.PlayaDamage;
        playaLevel=PlayerStats.PlayaLevel;
        playaMaxHP=PlayerStats.PlayaLife;
        playaCurrentHP=PlayerStats.PlayaCurrentHp;
        playaDefense=PlayerStats.PlayaDefense;
        playaLuck=PlayerStats.PlayaLuck;
        playaDex=PlayerStats.PlayaDex;
        anim=GetComponent<Animator>();
    }

    public bool TakeDamage(int dmg)
    {
        PlayerStats.PlayaCurrentHp-=dmg;

        if(PlayerStats.PlayaCurrentHp<=0)
            return true;
        else    
            return false;
    }

    public void AtkAnim()
    {
        anim.SetTrigger("atk");
    }

    public void DamAnim()
    {
        anim.SetTrigger("hit");
    }
}

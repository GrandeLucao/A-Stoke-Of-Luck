using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random= UnityEngine.Random;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{    
    [SerializeField]
    private PlayerController PlayerStats;
    private Animator anim;

    public string unitName;
    public int UnitHP;
    public int UnitCurrentHP;
    public int UnitLevel;
    public int UnitDamage;
    public int UnitDex;

    

    void Start() 
    {
        EnemyHP();
        anim=GetComponent<Animator>();
    }

    public bool TakeDamage(int dmg)
    {
        UnitCurrentHP-=dmg;

        if(UnitCurrentHP<=0)
            return true;
        else    
            return false;
    }

    public void BeGone()
    {
        Destroy(this.gameObject);
    }

    public void AtkAnim()
    {
        anim.SetTrigger("atk");
    }

    public void DamAnim()
    {
        anim.SetTrigger("hit");
    }

    void EnemyHP()
    {
        if(unitName=="Watcher")
        {
            UnitHP=50;
            UnitDamage=20;
            UnitDex=20;
            UnitLevel=15;
            UnitCurrentHP=UnitHP;
        }else if(unitName=="Golem")
        {
            UnitHP=100;
            UnitDamage=25;
            UnitDex=100;
            UnitLevel=30;
            UnitCurrentHP=UnitHP;
        }else{
        switch(PlayerStats.MapNum)
        {
            case 1:
                UnitHP=10;
                UnitDamage=1;
                UnitDex=20;
                UnitLevel=1;
                UnitCurrentHP=UnitHP;
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                UnitHP=Random.Range(10,20);
                UnitDamage=Random.Range(5,10);
                UnitDex=Random.Range(0,10);
                UnitLevel=Random.Range(1,5);
                UnitCurrentHP=UnitHP;
                break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                UnitHP=Random.Range(15,30);
                UnitDamage=Random.Range(10,20);
                UnitDex=Random.Range(0,10);
                UnitLevel=Random.Range(6,10);
                UnitCurrentHP=UnitHP;
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                UnitHP=Random.Range(20,40);
                UnitDamage=Random.Range(15,25);
                UnitDex=Random.Range(0,10);
                UnitLevel=Random.Range(11,14);
                UnitCurrentHP=UnitHP;
                break;
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
                UnitHP=Random.Range(40,60);
                UnitDamage=Random.Range(20,30);
                UnitDex=Random.Range(0,10);
                UnitLevel=Random.Range(16,20);
                UnitCurrentHP=UnitHP;
                break;
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
                UnitHP=Random.Range(50,100);
                UnitDamage=Random.Range(25,35);
                UnitDex=Random.Range(0,20);
                UnitLevel=Random.Range(21,25);
                UnitCurrentHP=UnitHP;
                break;
            case 26:
            case 27:
            case 28:
            case 29:
                UnitHP=Random.Range(60,100);
                UnitDamage=Random.Range(30,40);
                UnitDex=Random.Range(0,20);
                UnitLevel=Random.Range(26,29);
                UnitCurrentHP=UnitHP;
                break;
            default:
                break;
        }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random= UnityEngine.Random;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    CharUnit playerUnit;
    Unit enemyUnit;
    public Text dialogueText;
    public BattleHud playerHUD;
    public BattleHud enemyHUD;
    private BattleState state;    
    private string Lore;
    private int FirstDamage, FirstLuck,FirstMaxHP,FirstDefense,FirstDex;
    private int DiceNum;    
    int oldChoose;
    public Text HPStat, STRStat, DEFStat, DEXStat, LUCKStat;

    
    [SerializeField]
    private PlayerController PlayerStats;
    [SerializeField]
    public List<GameObject> enemyPrefab=new List<GameObject>();

    void Awake()
    {
        if(PlayerStats.MapNum==1)
        {
            FirstRoll();
        }
        if(PlayerStats.MapNum==31)
        {
            SceneManager.LoadScene(2);
        }
    }

    void Start()
    {
        state=BattleState.START;
        StartCoroutine(SetupBattle());
    }
    
    IEnumerator SetupBattle()
    {
        GameObject playerGo=Instantiate(playerPrefab, playerBattleStation);
        playerUnit=playerGo.GetComponent<CharUnit>();

        StatsScroll();

        GameObject enemyGO;
        switch(PlayerStats.MapNum)
        {
            case 15:
                enemyGO= Instantiate(enemyPrefab[5], enemyBattleStation);
                enemyUnit=enemyGO.GetComponent<Unit>();
                break;
            case 30:
                enemyGO= Instantiate(enemyPrefab[6], enemyBattleStation);
                enemyUnit=enemyGO.GetComponent<Unit>();
                break;
            default:
                enemyGO= Instantiate(enemyPrefab[Random.Range(0,5)], enemyBattleStation);
                enemyUnit=enemyGO.GetComponent<Unit>();
                break;
        }


        dialogueText.text="A "+enemyUnit.unitName+" approaches.";
        playerHUD.SetPlayerHUD(playerUnit);
        yield return new WaitForSeconds(2f);
        enemyHUD.SetHUD(enemyUnit);



        state=BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerNormalAttack()
    {        
        DiceNum=Random.Range(0,100);
        if(DiceNum<20)
        {
            state=BattleState.ENEMYTURN;
            playerUnit.AtkAnim();      
            FindObjectOfType<AudioManager>().Play("Atk"); 
            yield return new WaitForSeconds(1f);
            dialogueText.text="Attack Missed.";     
            yield return new WaitForSeconds(3f);
            StartCoroutine(EnemyTurn());
        }else if(DiceNum>80)
        {            
            bool isDead=enemyUnit.TakeDamage((PlayerStats.PlayaDamage)*2);
            enemyHUD.SetHP(enemyUnit.UnitCurrentHP);
            playerUnit.AtkAnim();
            FindObjectOfType<AudioManager>().Play("Atk");
            enemyUnit.DamAnim();
            yield return new WaitForSeconds(1f);
            dialogueText.text= "Critical Attack.";
            yield return new WaitForSeconds(1f);
            if(isDead)
            {
                state=BattleState.WON;
                EndBattle();
            }else
            {
                state=BattleState.ENEMYTURN;            
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }
        }else
        {
            bool isDead=enemyUnit.TakeDamage(PlayerStats.PlayaDamage);
            enemyHUD.SetHP(enemyUnit.UnitCurrentHP);
            playerUnit.AtkAnim();
            FindObjectOfType<AudioManager>().Play("Atk");
            enemyUnit.DamAnim();
            yield return new WaitForSeconds(1f);
            dialogueText.text= "Attack Sucessuful.";
            yield return new WaitForSeconds(1f);
            if(isDead)
            {
                state=BattleState.WON;
                EndBattle();
            }else
            {
                state=BattleState.ENEMYTURN;            
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator PlayerHeavyAttack()
    {        
        DiceNum=Random.Range(0,100);
        if(DiceNum<75)
        {
            state=BattleState.ENEMYTURN;
            playerUnit.AtkAnim();            
            FindObjectOfType<AudioManager>().Play("Atk");
            yield return new WaitForSeconds(1f);
            dialogueText.text="Attack Missed.";
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }else if(DiceNum>95)
        {            
            bool isDead=enemyUnit.TakeDamage(((PlayerStats.PlayaDamage)*2)*2);
            enemyHUD.SetHP(enemyUnit.UnitCurrentHP);
            playerUnit.AtkAnim();
            FindObjectOfType<AudioManager>().Play("Atk");
            enemyUnit.DamAnim();
            yield return new WaitForSeconds(1f);
            dialogueText.text= "Critical Attack.";
            if(isDead)
            {
                state=BattleState.WON;
                EndBattle();
            }else
            {
                state=BattleState.ENEMYTURN;            
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }
        }else
        {
            bool isDead=enemyUnit.TakeDamage((PlayerStats.PlayaDamage)*2);
            enemyHUD.SetHP(enemyUnit.UnitCurrentHP);
            playerUnit.AtkAnim();
            FindObjectOfType<AudioManager>().Play("Atk");
            enemyUnit.DamAnim();
            yield return new WaitForSeconds(1f);
            dialogueText.text= "Attack Sucessuful.";
            yield return new WaitForSeconds(1f);
            if(isDead)
            {
                state=BattleState.WON;
                EndBattle();
            }else
            {
                state=BattleState.ENEMYTURN;            
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator PlayerHeal()
        {
            if(PlayerStats.Potions>0)
            {
                dialogueText.text="Healed 35 HP.";
                PlayerStats.Potions-=1;
                PlayerStats.PlayaCurrentHp+=35; 
                if(PlayerStats.PlayaCurrentHp>PlayerStats.PlayaLife){PlayerStats.PlayaCurrentHp=PlayerStats.PlayaLife;}
                playerHUD.SetPotion(PlayerStats.Potions);           
                playerHUD.SetHP(PlayerStats.PlayaCurrentHp);
                state=BattleState.ENEMYTURN;            
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }else
            {                
                dialogueText.text="No potion available.";
                state=BattleState.ENEMYTURN;            
                yield return new WaitForSeconds(4f);
                StartCoroutine(EnemyTurn());
            }
        }
    

    IEnumerator PlayerRun()
    {
        int DiceRunP=Random.Range(0,20);
        int DiceRunE=Random.Range(0,20);
        DiceRunP+=PlayerStats.PlayaDex;
        DiceRunE+=enemyUnit.UnitDex;
        if(DiceRunP>DiceRunE)
        {
            state=BattleState.WON;
            dialogueText.text="You ran from this battle.";
            FindObjectOfType<AudioManager>().Play("Dash");
            yield return new WaitForSeconds(3f);
            StartCoroutine(WaitTime());
        }else
        {
            dialogueText.text="Run attempt failed.";
            state=BattleState.ENEMYTURN;            
            yield return new WaitForSeconds(3f);
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text= enemyUnit.unitName+" attacks.";
        yield return new WaitForSeconds(2f);
        DiceNum=Random.Range(0,100);
        if(DiceNum<20)
        {
            enemyUnit.AtkAnim();
            dialogueText.text="Enemy Missed.";    
            yield return new WaitForSeconds(3f);
            PlayerTurn();
            state=BattleState.PLAYERTURN;   
        }else if(DiceNum>80)
        {
            enemyUnit.AtkAnim();
            FindObjectOfType<AudioManager>().Play("Atk");
            playerUnit.DamAnim();
            dialogueText.text="Enemy critic attack.";
            yield return new WaitForSeconds(1f);
            bool isDead=playerUnit.TakeDamage(((enemyUnit.UnitDamage*2)-(PlayerStats.PlayaDefense/2)));
            playerHUD.SetHP(PlayerStats.PlayaCurrentHp);
            yield return new WaitForSeconds(2f);
            if(isDead)
            {
                state=BattleState.LOST;
                EndBattle();
            }else
            {
                PlayerTurn();
                state=BattleState.PLAYERTURN;
            }
        }else
        {
            enemyUnit.AtkAnim();
            FindObjectOfType<AudioManager>().Play("Atk");
            playerUnit.DamAnim();
            dialogueText.text="Enemy attacked.";
            yield return new WaitForSeconds(1f);
            bool isDead=playerUnit.TakeDamage(enemyUnit.UnitDamage-(PlayerStats.PlayaDefense/2));
            playerHUD.SetHP(PlayerStats.PlayaCurrentHp);
            yield return new WaitForSeconds(2f);
            if(isDead)
            {
                state=BattleState.LOST;
                EndBattle();
            }else
            {
                PlayerTurn();
                state=BattleState.PLAYERTURN;
            }
        }
        
    }

    IEnumerator LuckShot()
    {
        int D20=Random.Range(0,20);
        int Chance=Random.Range(0,100);
        int LuckRoll=PlayerStats.PlayaLuck;
        if((D20+LuckRoll)>=Chance)
        {
            dialogueText.text="The monster dropped a potion.";
            PlayerStats.Potions+=1;
            playerHUD.SetPotion(PlayerStats.Potions);  
        }
        yield return new WaitForSeconds(0.1f);            
        
    }

    public void StatsScroll()
    {
        HPStat.text="Vitality: "+PlayerStats.PlayaLife.ToString();
        STRStat.text="Strength: "+PlayerStats.PlayaDamage.ToString(); 
        DEFStat.text="Resistance: "+PlayerStats.PlayaDefense.ToString(); 
        DEXStat.text="Dexterity: "+PlayerStats.PlayaDex.ToString();  
        LUCKStat.text="Luck: "+PlayerStats.PlayaLuck.ToString(); 
    }

    void PlayerTurn()
    {
        dialogueText.text="Choose an Action: ";
    }

    public void OnAttackButton()
    {
        if(state!= BattleState.PLAYERTURN)
            return;
        else{
            StartCoroutine(PlayerNormalAttack());
        }
    }

    public void OnHeavyButton()
    {
        if(state!= BattleState.PLAYERTURN)
            return;
        else{
            StartCoroutine(PlayerHeavyAttack());
        }
    }

    public void OnRunButton()
    {
        if(state!= BattleState.PLAYERTURN)
            return;
        else{
            StartCoroutine(PlayerRun());
        }
    }

    public void OnHealButton()
    {
        if(state!= BattleState.PLAYERTURN)
            return;
        else{
            StartCoroutine(PlayerHeal());
        }
        
    }

    IEnumerator WaitTime()
    {        
        PlayerStats.MapNum+=1;   
        if (state== BattleState.LOST) 
        {                     
            dialogueText.text="You lost the battle.";  
            FindObjectOfType<AudioManager>().Play("Death");
            yield return new WaitForSeconds(3f); 
        }
        yield return new WaitForSeconds(0.1f);  
        if (state== BattleState.LOST) 
        {            
            SceneManager.LoadScene(0);
        }else{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void EndBattle()
    {
        if(state== BattleState.WON) 
        {
            PlayerStats.PlayaLevel+=1;
            LvlUp();       
            StartCoroutine(LoreKeeper());
        }else if (state== BattleState.LOST)
        {          
            StartCoroutine(WaitTime());
        }
    }

    public void FirstRoll()
    {
        FirstDamage=Random.Range(5, 20);
        PlayerStats.PlayaDamage=FirstDamage;

        FirstLuck=Random.Range(1,20);
        PlayerStats.PlayaLuck=FirstLuck;

        FirstMaxHP=Random.Range(10, 100);
        PlayerStats.PlayaLife=FirstMaxHP;
        PlayerStats.PlayaCurrentHp=FirstMaxHP;

        FirstDefense=Random.Range(1,10);
        PlayerStats.PlayaDefense=FirstDefense;  

        FirstDex=Random.Range(0,12);
        PlayerStats.PlayaDex=FirstDex;

    }

    public void LvlUp()
    {
        for(int i=0;i<2; i++)
        {
            int choose=Random.Range(0,5);
            if(oldChoose==choose)
            {
                choose=Random.Range(0,5);
            }
            oldChoose=choose;
            switch (choose)
            {
                case 0:
                    PlayerStats.PlayaDamage+=2;
                    break;
                case 1:
                    PlayerStats.PlayaLuck+=1;
                    break;
                case 2:
                    PlayerStats.PlayaLife+=3;
                    PlayerStats.PlayaCurrentHp+=3;
                    break;
                case 3:
                    PlayerStats.PlayaDefense+=1;
                    break;
                case 4:
                    PlayerStats.PlayaDex+=1;
                    break;
                default:
                    break;
            }
        }
        StatsScroll();
    }

    
    IEnumerator LoreKeeper()
    {
        enemyUnit.BeGone();
        dialogueText.text="You won the battle.";
        yield return new WaitForSeconds(1f);        
        StartCoroutine(LuckShot());
        yield return new WaitForSeconds(1.5f);        
        int LoreRandom=Random.Range(1,29);    
        int LoreChance=Random.Range(1,101);
        if(PlayerStats.MapNum==31)
        {
            Lore="The prophecy was fulfilled after all.";
        }else{
            if(LoreChance>=75)
            {
                switch(LoreRandom)
                {
                    case 1:
                        Lore="The Celestials wage war against evil on behalf of humanity.";
                        break;
                    case 2:
                        Lore="The Celestials would defeat and save humanity from chaos.";
                        break;
                    case 3:
                        Lore="The Celestial Beings made a deal with humans.";
                        break;
                    case 4:
                        Lore="The humans, desperately, had to resort to the Celestials.";
                        break;                
                    case 5:
                        Lore="The Reign was in piece, until something profane awakened powers the humans had forgotten.";                
                        break;                
                    case 6:
                        Lore="An ancient Golem rose from the depths of the world, and with it came the monsters.";
                        break;                
                    case 7:
                        Lore="The cities were under attack and the Reign went to war.";
                        break;                
                    case 8:
                        Lore="The hordes of monsters destroy everything in its path.";
                        break;                
                    case 9:
                        Lore="The Reign went into chaos, the people frightened and hope was fading away.";
                        break;                
                    case 10:
                        Lore="No warrior could stop the monsters attack.";
                        break;                
                    case 11:
                        Lore="Luck didn't help any hero to save the Reign.";
                        break;                
                    case 12:
                        Lore="The monsters were numerous but the power of the Light Beigns was overwhelming.";
                        break;                
                    case 13:
                        Lore="The battlefields were shining with the power from these Beigns.";
                        break;                
                    case 14:
                        Lore="But the price the humans paid was high.";
                        break;                
                    case 15:
                        Lore="They now owe the Celestials eternal devotion for banishing evil.";
                        break;                
                    case 16:
                        Lore="No human was allowed to enter the place where the evil that plagued the world was imprisoned.";
                        break;                
                    case 17:
                        Lore="If evil strikes again, the Celestials would lose the devotion of men.";
                        break;                
                    case 18:
                        Lore="And then a legend arose, as if it were a prophecy for humans.";
                        break;                
                    case 19:
                        Lore="The legend of a hero who would be embraced by fate and defeat monsters in the name of humanity.";
                        break;                
                    case 20:
                        Lore="The Celestials didn't like the prophecy and so a warrior was left to protect the place.";
                        break;                
                    case 21:
                        Lore="Humans couldn't find out the truth.";
                        break;                
                    case 22:
                        Lore="The Celestials didn't save humanity.";
                        break;                
                    case 23:
                        Lore="The evil that came to plague people was actually the result of a mistake by the Celestials.";
                        break;                
                    case 24:
                        Lore="The mistake of trying to revive dead warriors and create life in creatures that shouldn't live.";
                        break;                
                    case 25:
                        Lore="If humans discovered the truth, the celestials would lose their powers and their kingdom in the heavens would fall.";
                        break;                
                    case 26:
                        Lore="So they tried to cover up the truth and exile evil within this dungeon.";
                        break;                
                    case 27:
                        Lore="But the hero of the legend would defeat the reanimated evil and free mankind from the lies of the Celestials.";
                        break;                
                    case 28:
                        Lore="Luck would embrace a hero and once again peace would reign in the human world.";
                        break;        
                    case 29:
                        Lore="They banish evil to a secret prison within a mountain.";
                        break;    
                }                
                dialogueText.text=Lore;
                yield return new WaitForSeconds(6);   
            }
        }     
        StartCoroutine(WaitTime());
    }

}

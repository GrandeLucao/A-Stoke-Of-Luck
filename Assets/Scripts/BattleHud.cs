using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text PotionText, MapText;
    public Slider hpSlider;
    
    [SerializeField]
    private PlayerController PlayerStats;

    public void SetHUD(Unit unit)
    {
        nameText.text=unit.unitName;
        levelText.text="Llv "+unit.UnitLevel;
        hpSlider.maxValue=unit.UnitHP;
        hpSlider.value=unit.UnitCurrentHP;
    }

    public void SetPlayerHUD(CharUnit unit)
    {
        nameText.text=unit.playaName;
        levelText.text="Llv "+PlayerStats.PlayaLevel;
        hpSlider.maxValue=PlayerStats.PlayaLife;
        hpSlider.value=PlayerStats.PlayaCurrentHp;
        PotionText.text="x" + PlayerStats.Potions.ToString();
        MapText.text="Room: " + PlayerStats.MapNum.ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.value=hp;
    }

    public void SetPotion(int pot)
    {
        PotionText.text="x" + pot.ToString();
    }


}

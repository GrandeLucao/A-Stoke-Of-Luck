using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerController PlayerStats;

    public void BeginGame()
    {        
        PlayerStats.MapNum=1;
        PlayerStats.PlayaDamage=0;
        PlayerStats.PlayaLevel=1;
        PlayerStats.PlayaLife=0;
        PlayerStats.PlayaDefense=0;
        PlayerStats.PlayaLuck=0;
        PlayerStats.PlayaDex=0;
        PlayerStats.Potions=3;
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class KDA_Data : MonoBehaviour
{   //Marcus
    [Header("KILLS & Deaths")]
    public int Kills;
    public int SaveKills;
    public int Deaths;

    [Header("Shown Values (On Scoreboard)")]
    public Text KillsText;
    public Text DeathsText;
    public void Start()
    {
        PlayerPrefs.SetInt("kills", Kills);
    }

    public void SetData()
    {
        
        SaveKills = PlayerPrefs.GetInt("kills");
        KillsText.text = SaveKills.ToString();

    }


    public void AddKD()
    {
        SaveKills++;
    }





}

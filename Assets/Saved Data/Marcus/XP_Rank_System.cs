using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class XP_Rank_System : MonoBehaviour
{
    public int currentlevel = 0;
    public string RankSave;
    public Text RankText;
    public string saveName;
   
    public string[] Ranks= 
    { 
        "Rookie", "Corpal",
        "Sergeant", "Lieutenant",
        "Captain", "Major", 
        "General", "Commander",
        "Prestige", "Kungen" 
    };

    public void Start()
    {
        PlayerPrefs.SetString("none", RankSave);
    }

    public void RankSystem()
    {
            currentlevel++;
            for (int i = 0; i <= currentlevel; i++)
            {
                Debug.Log("Youre rank is:" + Ranks[i]);
                RankSave = Ranks[i];
                saveName = PlayerPrefs.GetString("Rank", "none");
                RankText.text = saveName;
            }

        PlayerPrefs.SetString("Rank", RankSave);

    }
}

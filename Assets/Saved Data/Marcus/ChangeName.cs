using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    public string NameOfPlayer;
    public string saveName;

    public Text inputText;
    public Text loadedName; // Där det står

    void Update()
    {
        NameOfPlayer = PlayerPrefs.GetString("name", "none");
        loadedName.text = NameOfPlayer;
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
    }

}

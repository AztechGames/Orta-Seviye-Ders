using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefSave : MonoBehaviour
{
    // PlayerPref
    // JSON .json
    // Configuration File .csv / .xml
    // ScriptableObject 
    // *Server*
    public static string prefName = "name";
    public static string prefInt = "health";
    public static string prefFloat = "mana";
    
    [Serializable]
    public class Player
    {
        public string name;
        public int health;
        public float mana;
    }

    public Player player = new Player();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
            Save();
    }

    public void Save()
    {
        PlayerPrefs.SetString(prefName,player.name);
        PlayerPrefs.SetInt(prefInt,player.health);
        PlayerPrefs.SetFloat(prefFloat,player.mana);
    }

    public void Load()
    {
        player.name = PlayerPrefs.GetString(prefName);
        player.health = PlayerPrefs.GetInt(prefInt);
        player.mana = PlayerPrefs.GetFloat(prefFloat);
    }
}

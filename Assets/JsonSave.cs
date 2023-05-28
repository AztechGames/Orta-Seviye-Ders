using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSave : MonoBehaviour
{
    [Serializable]
    public class Player
    {
        public string name;
        public int health;
        public float mana;
    }

    public Player player;


    public void SaveToJson()
    {
        Player _player = new Player();
        string json = JsonUtility.ToJson(_player);
        File.WriteAllText(Application.dataPath + "/JsonSaveFile.json",json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/JsonSaveFile.json");
        player = JsonUtility.FromJson<Player>(json);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) SaveToJson();
        if(Input.GetKeyDown(KeyCode.K)) LoadFromJson();
    }
}

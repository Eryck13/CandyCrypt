using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // attributes which can be used to save file later
    public int health;
    public int level;
    public int score;      //score for saving data

    public PlayerData(Player player)
    {
        // add whatever attribute you want to add in here from player class

        level = player.level;
        health = player.health;
        score = player.score;

    }
}

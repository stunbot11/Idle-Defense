using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public Gen1 gen1;
    public Gen2 gen2;
    public Gen3 gen3;
    public Gen4 gen4;
    public Gen5 gen5;

    public Enemy1 enemy1;
    public Enemy2 enemy2;
    public Enemy3 enemy3;

    private void Start()
    {
        GetComponent<GameManager>().life = PlayerPrefs.GetInt("life", 50);
        GetComponent<GameManager>().money = PlayerPrefs.GetInt("money", 30);
        GetComponent<GameManager>().round = PlayerPrefs.GetInt("round", 1);
        GetComponent<GameManager>().baseEnemies = PlayerPrefs.GetInt("enemies", 8);
        GetComponent<GameManager>().enemiesPerSecond = PlayerPrefs.GetFloat("spawning speed", .5f);

        //gens
        //g1
        gen1.moneyGen = PlayerPrefs.GetInt("gen1 money", 5);
        gen1.lvl = PlayerPrefs.GetInt("gen1 lvl", 1);
        gen1.lvlCost = PlayerPrefs.GetInt("gen1 lvl cost", 50);

        //g2
        gen2.moneyGen = PlayerPrefs.GetInt("gen2 money", 25);
        gen2.lvl = PlayerPrefs.GetInt("gen2 lvl", 0);
        gen2.lvlCost = PlayerPrefs.GetInt("gen2 lvl cost", 250);

        //g3
        gen3.moneyGen = PlayerPrefs.GetInt("gen3 money", 125);
        gen3.lvl = PlayerPrefs.GetInt("gen3 lvl", 0);
        gen3.lvlCost = PlayerPrefs.GetInt("gen3 lvl cost", 1250);

        //g4
        gen4.moneyGen = PlayerPrefs.GetInt("gen4 money", 625);
        gen4.lvl = PlayerPrefs.GetInt("gen4 lvl", 0);
        gen4.lvlCost = PlayerPrefs.GetInt("gen4 lvl cost", 6250);

        //g5
        gen5.moneyGen = PlayerPrefs.GetInt("gen5 money", 3125);
        gen5.lvl = PlayerPrefs.GetInt("gen5 lvl", 0);
        gen5.lvlCost = PlayerPrefs.GetInt("gen5 lvl cost", 31250);

        //enemies
        //enemy 1
        enemy1.health = PlayerPrefs.GetFloat("enemy1 health", 5f);
        enemy1.speed = PlayerPrefs.GetFloat("enemy1 speed", 3.0f);
        enemy1.round = PlayerPrefs.GetInt("enemy1 round", 0);

        //enemy 2
        enemy2.health = PlayerPrefs.GetFloat("enemy2 health", 15f);
        enemy2.speed = PlayerPrefs.GetFloat("enemy2 speed", 1.5f);
        enemy2.round = PlayerPrefs.GetInt("enemy2 round", 0);

        //enemy 3
        enemy3.health = PlayerPrefs.GetFloat("enemy3 health", 2f);
        enemy3.speed = PlayerPrefs.GetFloat("enemy3 speed", 9f);
        enemy3.round = PlayerPrefs.GetInt("enemy3 round", 0);
    }

    public void loadgame()
    {
        GetComponent<GameManager>().life = PlayerPrefs.GetInt("life", 50);
        GetComponent<GameManager>().money = PlayerPrefs.GetInt("money", 30);
        GetComponent<GameManager>().round = PlayerPrefs.GetInt("round", 1);
        GetComponent<GameManager>().baseEnemies = PlayerPrefs.GetInt("enemies", 8);
        GetComponent<GameManager>().enemiesPerSecond = PlayerPrefs.GetFloat("spawning speed", .5f);

        //gens
        //g1
        gen1.moneyGen = PlayerPrefs.GetInt("gen1 money", 5);
        gen1.lvl = PlayerPrefs.GetInt("gen1 lvl", 1);
        gen1.lvlCost = PlayerPrefs.GetInt("gen1 lvl cost", 50);

        //g2
        gen2.moneyGen = PlayerPrefs.GetInt("gen2 money", 25);
        gen2.lvl = PlayerPrefs.GetInt("gen2 lvl", 0);
        gen2.lvlCost = PlayerPrefs.GetInt("gen2 lvl cost", 250);

        //g3
        gen3.moneyGen = PlayerPrefs.GetInt("gen3 money", 125);
        gen3.lvl = PlayerPrefs.GetInt("gen3 lvl", 0);
        gen3.lvlCost = PlayerPrefs.GetInt("gen3 lvl cost", 1250);

        //g4
        gen4.moneyGen = PlayerPrefs.GetInt("gen4 money", 625);
        gen4.lvl = PlayerPrefs.GetInt("gen4 lvl", 0);
        gen4.lvlCost = PlayerPrefs.GetInt("gen4 lvl cost", 6250);

        //g5
        gen5.moneyGen = PlayerPrefs.GetInt("gen5 money", 3125);
        gen5.lvl = PlayerPrefs.GetInt("gen5 lvl", 0);
        gen5.lvlCost = PlayerPrefs.GetInt("gen5 lvl cost", 31250);

        //enemies
        //enemy 1
        enemy1.health = PlayerPrefs.GetFloat("enemy1 health", 5f);
        enemy1.speed = PlayerPrefs.GetFloat("enemy1 speed", 3.0f);
        enemy1.round = PlayerPrefs.GetInt("enemy1 round", 0);

        //enemy 2
        enemy2.health = PlayerPrefs.GetFloat("enemy2 health", 15f);
        enemy2.speed = PlayerPrefs.GetFloat("enemy2 speed", 1.5f);
        enemy2.round = PlayerPrefs.GetInt("enemy2 round", 0);

        //enemy 3
        enemy3.health = PlayerPrefs.GetFloat("enemy3 health", 2f);
        enemy3.speed = PlayerPrefs.GetFloat("enemy3 speed", 9f);
        enemy3.round = PlayerPrefs.GetInt("enemy3 round", 0);
    }

    public void SaveProfile()
    {
        PlayerPrefs.SetInt("life", GetComponent<GameManager>().life);
        PlayerPrefs.SetInt("money", GetComponent<GameManager>().money);
        PlayerPrefs.SetInt("round", GetComponent<GameManager>().round);
        PlayerPrefs.GetInt("enemies", GetComponent<GameManager>().baseEnemies);
        PlayerPrefs.GetFloat("spawning speed", GetComponent<GameManager>().enemiesPerSecond);

        //gens
        //g1
        PlayerPrefs.SetInt("gen1 money", gen1.moneyGen);
        PlayerPrefs.SetInt("gen1 lvl", gen1.lvl);
        PlayerPrefs.SetInt("gen1 lvl cost", gen1.lvlCost);

        //g2
        PlayerPrefs.SetInt("gen2 money", gen2.moneyGen);
        PlayerPrefs.SetInt("gen2 lvl", gen2.lvl);
        PlayerPrefs.SetInt("gen2 lvl cost", gen2.lvlCost);

        //g3
        PlayerPrefs.SetInt("gen3 money", gen3.moneyGen);
        PlayerPrefs.SetInt("gen3 lvl", gen3.lvl);
        PlayerPrefs.SetInt("gen3 lvl cost", gen3.lvlCost);

        //g4
        PlayerPrefs.SetInt("gen4 money", gen4.moneyGen);
        PlayerPrefs.SetInt("gen4 lvl", gen4.lvl);
        PlayerPrefs.SetInt("gen4 lvl cost", gen4.lvlCost);

        //g5
        PlayerPrefs.SetInt("gen5 money", gen5.moneyGen);
        PlayerPrefs.SetInt("gen5 lvl", gen5.lvl);
        PlayerPrefs.SetInt("gen5 lvl cost", gen5.lvlCost);

        //enemies
        //enemy1
        PlayerPrefs.SetFloat("enemy1 health", enemy1.health);
        PlayerPrefs.SetFloat("enemy1 speed", enemy1.speed);
        PlayerPrefs.SetInt("enemy1 round", enemy1.round);

        //enemy2
        PlayerPrefs.SetFloat("enemy2 health", enemy2.health);
        PlayerPrefs.SetFloat("enemy2 speed", enemy2.speed);
        PlayerPrefs.SetInt("enemy2 round", enemy2.round);

        //enemy3
        PlayerPrefs.SetFloat("enemy3 health", enemy3.health);
        PlayerPrefs.SetFloat("enemy3 speed", enemy3.speed);
        PlayerPrefs.SetInt("enemy3 round", enemy3.round);
    }

    public void deleteProfile()
    {
        PlayerPrefs.SetInt("life", 50);
        PlayerPrefs.SetInt("money", 30);
        PlayerPrefs.SetInt("round", 1);
        PlayerPrefs.GetInt("enemies", 8);
        PlayerPrefs.GetFloat("spawning speed", .5f);

        //g1
        PlayerPrefs.SetInt("gen1 money", 5);
        PlayerPrefs.SetInt("gen1 lvl", 1);
        PlayerPrefs.SetInt("gen1 lvl cost", 50);

        //g2
        PlayerPrefs.SetInt("gen2 money", 25);
        PlayerPrefs.SetInt("gen2 lvl", 0);
        PlayerPrefs.SetInt("gen2 lvl cost", 250);

        //g3
        PlayerPrefs.SetInt("gen3 money", 125);
        PlayerPrefs.SetInt("gen3 lvl", 0);
        PlayerPrefs.SetInt("gen3 lvl cost", 1250);

        //g4
        PlayerPrefs.SetInt("gen4 money", 625);
        PlayerPrefs.SetInt("gen4 lvl", 0);
        PlayerPrefs.SetInt("gen4 lvl cost", 6250);

        //g5
        PlayerPrefs.SetInt("gen5 money", 3125);
        PlayerPrefs.SetInt("gen5 lvl", 0);
        PlayerPrefs.SetInt("gen5 lvl cost", 31250);

        //enemies
        //enemy 1
        PlayerPrefs.SetFloat("enemy1 health", 5);
        PlayerPrefs.SetFloat("enemy1 speed", 3.0f);
        PlayerPrefs.SetInt("enemy1 round", 0);

        //enemy 2
        PlayerPrefs.SetFloat("enemy2 health", 15);
        PlayerPrefs.SetFloat("enemy2 speed", 1.5f);
        PlayerPrefs.SetInt("enemy2 round", 0);

        //enemy 3
        PlayerPrefs.SetFloat("enemy3 health", 2);
        PlayerPrefs.SetFloat("enemy3 speed", 9.0f);
        PlayerPrefs.SetInt("enemy3 round", 0);
    }
}

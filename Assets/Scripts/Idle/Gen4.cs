using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen4 : MonoBehaviour
{
    public GameManager gm;

    public int moneyGen = 625;
    public float timeToMake = 2.5f;
    public float genTimer = 0;
    public int lvl = 0;
    public int lvlCost = 6250;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (lvl >= 1)
        {
            if (genTimer < timeToMake)
                genTimer += Time.deltaTime;
            else
            {
                genTimer = 0;
                gm.money += moneyGen;
            }
        }
    }
}

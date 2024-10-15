using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen1 : MonoBehaviour
{
    public GameManager gm;

    public int moneyGen = 5;
    public float timeToMake = 2.5f;
    public float genTimer = 0;
    public int lvl = 1;
    public int lvlCost = 50;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
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
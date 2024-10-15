using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen6 : MonoBehaviour
{
    public GameManager gm;

    public bool isActive = false;
    public int lifeGen = 10;
    public float timeToMake = 30f;
    public float genTimer = 0;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isActive)
        {
            if (genTimer < timeToMake)
                genTimer += Time.deltaTime;
            else
            {
                genTimer = 0;
                gm.life += lifeGen;
            }
        }
    }
}

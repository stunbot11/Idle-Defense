using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 : MonoBehaviour
{
    //minigunner tower
    private bool hasUpgradeMenu = false;

    private bool canAtk = true;
    private Rigidbody2D myRB;
    private Transform myTransform;

    [SerializeField] private int totalMoney;

    public int upgrade1Cost = 50;
    public int upgrade2Cost = 100;
    public int upgrade3Cost = 500;

    public int upgrade1Lvl = 1;
    public int upgrade2Lvl = 1;
    public int upgrade3Lvl = 1;

    public float atkSpeed = .1f;
    public float atkCoolDown = 0;
    public float dmg = .1f;
    public int pierce = 1;
    public float bulletSpeed = 10000;
    public float spread = 25;

    public GameObject miniBullet;
    private GameObject upgrades;

    public GameManager gm;

    // upgrades will be dmg, firerate, spread
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!canAtk)
        {
            if (atkSpeed <= atkCoolDown)
            {
                canAtk = true;
                atkCoolDown = 0;
            }
            else
                atkCoolDown += Time.deltaTime;
        }

        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (canAtk)
        {
            float bloom = UnityEngine.Random.Range(-spread, spread);

            GameObject MB = Instantiate(miniBullet, transform.position, Quaternion.identity, myTransform);

            myRB.rotation = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90 + bloom;
            MB.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * bulletSpeed);
            canAtk = false;
        }

        if (hasUpgradeMenu)
        {
            gm.upgradeMenu.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (gm.money >= upgrade1Cost)
                {
                    upgradeDMG();
                    totalMoney += upgrade1Cost;
                    gm.money -= upgrade1Cost;
                    upgrade1Cost += upgrade1Cost / 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (gm.money >= upgrade2Cost)
                {
                    upgradeFireRate();
                    totalMoney += upgrade2Cost;
                    gm.money -= upgrade2Cost;
                    upgrade2Cost += upgrade2Cost / 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (gm.money >= upgrade3Cost)
                {
                    upgradeSpread();
                    totalMoney += upgrade3Cost;
                    gm.money -= upgrade3Cost;
                    upgrade3Cost += upgrade3Cost / 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                gm.money += totalMoney / 3;
                Destroy(gameObject);
            }

            gm.upgrade1.text = ("Damage: " + dmg + Environment.NewLine + "Level: " + upgrade1Lvl + Environment.NewLine + "Cost: " + upgrade1Cost);
            gm.upgrade2.text = ("Fire Rate: " + atkSpeed + Environment.NewLine + "Level: " + upgrade2Lvl + Environment.NewLine + "Cost: " + upgrade2Cost);
            gm.upgrade3.text = ("spread: " + spread + Environment.NewLine + "Level: " + upgrade3Lvl + Environment.NewLine + "Cost: " + upgrade3Cost);
        }
    }

    public void upgradeDMG()
    {
        dmg += dmg / 2;
    }

    public void upgradeFireRate()
    {
        atkSpeed *= .90f;
    }

    public void upgradeSpread()
    {
        spread *= .90f;
    }

    public void OnMouseDown()
    {
        hasUpgradeMenu = true;
        myTransform.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        hasUpgradeMenu = false;
        myTransform.transform.GetChild(0).gameObject.SetActive(false);
        gm.upgradeMenu.SetActive(false);
    }
}

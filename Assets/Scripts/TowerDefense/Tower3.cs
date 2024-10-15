using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Tower3 : MonoBehaviour
{
    //AOE tower
    [SerializeField] private bool canATK = true;
    [SerializeField] private bool enemyInRange = false;
    private bool hasUpgradeMenu = false;

    private Rigidbody2D myRB;
    private Transform myT;
    private CircleCollider2D myC;

    [SerializeField] private int totalMoney;

    public int upgrade1Cost = 200;
    public int upgrade2Cost = 100;
    public int upgrade3Cost = 650;

    public int upgrade1Lvl = 1;
    public int upgrade2Lvl = 1;
    public int upgrade3Lvl = 1;

    public float atkSpeed = .5f;
    public float atkCoolDown = 0;
    public int dmg = 1;
    public float range = 2.5f;
    public float bulletLifeSpan = .25f;

    public GameObject AOEbullet;

    private Vector2 upRight; 
    private Vector2 upLeft; 
    private Vector2 downLeft; 
    private Vector2 downRight; 
    private Vector2 down;
    private Vector3 circleSize;


    [SerializeField] private LayerMask enemyMask;
    private Transform target;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        upLeft = new Vector2(Mathf.Cos(-120), Mathf.Sin(-120)) * -1;
        upRight = new Vector2(Mathf.Cos(-120), Mathf.Sin(120));
        downRight = new Vector2(Mathf.Cos(-120), Mathf.Sin(-120));
        downLeft = new Vector2(Mathf.Cos(-120), Mathf.Sin(120)) * -1;
        down = new Vector2(0, -1);
        circleSize = new Vector3(5, 5, 1);
        myT = GetComponent<Transform>();
        myC = GetComponent<CircleCollider2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atkSpeed <= atkCoolDown)
        {
            findTarget();
            if (enemyInRange)
            {
                atk();
                atkCoolDown = 0;
            }
        }
        else
                atkCoolDown += Time.deltaTime;


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
                    upgradeRange();
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
            gm.upgrade3.text = ("Range: " + range + Environment.NewLine + "Level: " + upgrade3Lvl + Environment.NewLine + "Cost: " + upgrade3Cost);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }
#endif

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            enemyInRange = true;
        }
        else
            enemyInRange = false;
    }

    private bool checkTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    public void upgradeDMG()
    {
        dmg += dmg / 2;
    }

    public void upgradeFireRate()
    {
        atkSpeed *= .90f;
    }

    public void upgradeRange()
    {
        range *= 1.20f;
        circleSize = new Vector3(range * 2, range * 2, 1);
        myT.transform.GetChild(0).gameObject.transform.localScale = circleSize;
        bulletLifeSpan *= 1.2f;
    }

    public void OnMouseDown()
    {
        hasUpgradeMenu = true;
        myT.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        hasUpgradeMenu = false;
        myT.transform.GetChild(0).gameObject.SetActive(false);
        gm.upgradeMenu.SetActive(false);
    }

    private void atk()
    {
        GameObject AB = Instantiate(AOEbullet, transform.position, Quaternion.identity, myT);
        AB.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * 600);
        Destroy(AB, bulletLifeSpan);

        GameObject MB2 = Instantiate(AOEbullet, transform.position, Quaternion.identity, myT);
        MB2.GetComponent<Rigidbody2D>().AddRelativeForce(upRight * 600);
        Destroy(MB2, bulletLifeSpan);

        GameObject MB3 = Instantiate(AOEbullet, transform.position, Quaternion.identity, myT);
        MB3.GetComponent<Rigidbody2D>().AddRelativeForce(upLeft * 600);
        Destroy(MB3, bulletLifeSpan);

        GameObject MB4 = Instantiate(AOEbullet, transform.position, Quaternion.identity, myT);
        MB4.GetComponent<Rigidbody2D>().AddRelativeForce(downRight * 600);
        Destroy(MB4, bulletLifeSpan);

        GameObject MB5 = Instantiate(AOEbullet, transform.position, Quaternion.identity, myT);
        MB5.GetComponent<Rigidbody2D>().AddRelativeForce(downLeft * 600);
        Destroy(MB5, bulletLifeSpan);

        GameObject MB6 = Instantiate(AOEbullet, transform.position, Quaternion.identity, myT);
        MB6.GetComponent<Rigidbody2D>().AddRelativeForce(down * 600);
        Destroy(MB6, bulletLifeSpan);

        canATK = false;
    }

}

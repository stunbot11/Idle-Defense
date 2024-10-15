using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Tower2 : MonoBehaviour
{
    // tower
    private bool hasUpgradeMenu = false;

    private Rigidbody2D myRB;
    private Transform myTransform;
    private CircleCollider2D myC;


    [SerializeField] private int totalMoney;

    [SerializeField] private float range = 7.5f;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private GameObject projectile2;

    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgrade1;
    [SerializeField] private Button upgrade2;
    [SerializeField] private Button upgrade3;

    private Transform target;

    public int upgrade1Cost = 40;
    public int upgrade2Cost = 100;
    public int upgrade3Cost = 750;

    public int upgrade1Lvl = 1;
    public int upgrade2Lvl = 1;
    public int upgrade3Lvl = 1;

    public float atkSpeed = .5f;
    public float atkCoolDown = 0;
    public int dmg = 1;
    public int pierce = 3;
    public float bulletSpeed = 2000;



    private GameObject upgrades;

    public GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myC = GetComponent<CircleCollider2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            findTarget();
            return;
        }

        rotateTowardsTarget();

        if (!checkTargetIsInRange())
        {
            target = null;
        }
        else
        {
            if (atkSpeed <= atkCoolDown)
            {
                shoot();
                atkCoolDown = 0;
            }
                else
                    atkCoolDown += Time.deltaTime;
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
                    upgradePierce();
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
            gm.upgrade3.text = ("Pierce: " + pierce + Environment.NewLine + "Level: " + upgrade3Lvl + Environment.NewLine + "Cost: " + upgrade3Cost);
        }
    }

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool checkTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

# if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }
# endif
    private void shoot()
    {
        GameObject MB = Instantiate(projectile2, transform.position, Quaternion.identity, myTransform);
        MB.GetComponent<Rigidbody2D>().AddRelativeForce(transform.up * bulletSpeed);
    }

    private void rotateTowardsTarget()
    {
        Vector2 targetPos = (target.position - transform.position);
        myRB.rotation = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90;
    }

    public float projectile()
    {
        return MathF.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f; ;
    }

    public void upgradeDMG()
    {
        dmg += dmg;
    }

    public void upgradeFireRate()
    {
        atkSpeed *= .90f;
    }

    public void upgradePierce()
    {
        pierce ++;
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

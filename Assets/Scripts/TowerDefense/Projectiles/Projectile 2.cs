using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    private Tower2 t2;
    private Enemy1 enemy1;
    private Enemy2 enemy2;
    private Enemy3 enemy3;

    [SerializeField] private Rigidbody2D rb;

    public float damage;
    public int pierce;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t2 = GetComponentInParent<Tower2>();
        damage = t2.dmg;
        pierce = t2.pierce;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemy1")
        {
            enemy1 = collision.gameObject.GetComponent<Enemy1>();
            enemy1.gotHit(damage);
            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemy2")
        {
            enemy2 = collision.gameObject.GetComponent<Enemy2>();
            enemy2.gotHit(damage);
            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemy3")
        {
            enemy3 = collision.gameObject.GetComponent<Enemy3>();
            enemy3.gotHit(damage);
            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }
    }
}

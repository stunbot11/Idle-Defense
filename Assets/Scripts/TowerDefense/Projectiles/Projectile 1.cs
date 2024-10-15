using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    private Tower1 t1;
    private Enemy1 enemy1;
    private Enemy2 enemy2;
    private Enemy3 enemy3;

    public float damage;



    // Start is called before the first frame update
    void Start()
    {
        t1 = GetComponentInParent<Tower1>();
        damage = t1.dmg;
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
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemy2")
        {
            enemy2 = collision.gameObject.GetComponent<Enemy2>();
            enemy2.gotHit(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "enemy3")
        {
            enemy3 = collision.gameObject.GetComponent<Enemy3>();
            enemy3.gotHit(damage);
            Destroy(gameObject);
        }
    }
}

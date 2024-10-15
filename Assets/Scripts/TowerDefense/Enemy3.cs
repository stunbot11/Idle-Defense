using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public GameManager gm;

    [SerializeField] private Rigidbody2D rb;

    private bool alive = true;

    public float speed = 9;
    public float health = 2;
    public int round = 0;

    private Transform target;
    private int pathIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = gm.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= .25f)
        {
            pathIndex++;


            if (pathIndex == gm.path.Length)
            {
                gm.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                gm.life--;

                if (gm.life <= 0)
                {
                    gm.death();
                }
                return;
            }

            else
                target = gm.path[pathIndex];

            target = gm.path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    public void gotHit(float hurt)
    { 
        health -= hurt;
        if (health <= 0 && alive)
        {
            alive = false;
            gm.enemiesAlive--;
            Destroy(gameObject);
        }
    }

    public void roundEnd()
    {
        round++;
        if (round == 10)
        {
            speed *= 1.10f;
            health += (health / 15);
            round = 0;
        }
    }
}

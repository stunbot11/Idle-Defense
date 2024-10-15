using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plots : MonoBehaviour
{
    [SerializeField] GameManager gm;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject towerObj;

    private Color startColor;



    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (gm.IsHoveringUI()) return;

        if (towerObj != null)
        {
            return;
        }
        
        Tower towerToBuild = gm.GetSelectedTower();
        
        if (towerToBuild.cost > gm.money)
        {
            return;
        }

        gm.spendMoney(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}

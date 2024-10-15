using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private bool tengoLifeGen = false;

    public Transform startPoint;
    public Transform[] path;

    [Header("gens")]

    public Gen1 gen1;
    public Gen2 gen2;
    public Gen3 gen3;
    public Gen4 gen4;
    public Gen5 gen5;
    public Gen6 gen6;

    [Header("tower defence")]

    public UnityEvent onEnemyDestroy = new UnityEvent();


    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float timeBetweenRounds = 3f;
    [SerializeField] private bool isSpawning = false;
    [SerializeField] private float enemiesPerSecCap = 20f;

    

    public int baseEnemies = 8;
    public float enemiesPerSecond = 0.5f;

    public Enemy1 enemy1;
    public Enemy2 enemy2;
    public Enemy3 enemy3;

    public TMP_Text upgrade1;
    public TMP_Text upgrade2;
    public TMP_Text upgrade3;
    public GameObject upgradeMenu;

    private bool isHoveringUI = false;

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    
    private int SelectedTower = 3;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Tower[] towers;

    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    public void setSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }

    private float timeSinceLastSpawn;
    public int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;

    [Header("misc.")]

    public LinkedList ll;
    public bool onMainMenu = false;

    [Header("UI")]

    public int money;
    public Text moneyTxt;

    public int life = 100;
    public Text lifeTxt;

    //show and hide idle part
    public Image idle;
    public GameObject hide;
    public GameObject show;

    [Header("gen info")]

    //gen1
    public Image gen1Bar;
    public Text gen1Txt;
    public TMP_Text gen1LvlCostTxt;

    //gen2
    public GameObject gen2Info;
    public Image gen2Bar;
    public Text gen2Txt;
    public TMP_Text gen2LvlCostTxt;

    //gen3
    public GameObject gen3Info;
    public Image gen3Bar;
    public Text gen3Txt;
    public TMP_Text gen3LvlCostTxt;

    //gen4
    public GameObject gen4Info;
    public Image gen4Bar;
    public Text gen4Txt;
    public TMP_Text gen4LvlCostTxt;

    //gen5
    public GameObject gen5Info;
    public Image gen5Bar;
    public Text gen5Txt;
    public TMP_Text gen5LvlCostTxt;

    private Vector3 sI;
    private Vector3 hI;

    //life gen
    public GameObject lifeGenInfo;
    public Image lifeGenBar;

    public int round = 1;
    public Text roundTxt;

    void Start()
    {
        sI = new Vector3(-710, 0, 0);
        hI = new Vector3(-1210, 0, 0);

        StartCoroutine(StartRound());
    }

    // Update is called once per frame
    void Update()
    {
        if (!onMainMenu)
        {
            gen1Bar.fillAmount = (float)gen1.genTimer / (float)gen1.timeToMake;
            gen2Bar.fillAmount = (float)gen2.genTimer / (float)gen2.timeToMake;
            gen3Bar.fillAmount = (float)gen3.genTimer / (float)gen3.timeToMake;
            gen4Bar.fillAmount = (float)gen4.genTimer / (float)gen4.timeToMake;
            gen5Bar.fillAmount = (float)gen5.genTimer / (float)gen5.timeToMake;

            moneyTxt.text = ("Money: " + money);
            lifeTxt.text = ("Lifes: " + life);
            roundTxt.text = ("Round: " + round);

            //gen1 txt
            gen1Txt.text = ("" + gen1.moneyGen);
            if (gen1.lvl <= 9)
                gen1LvlCostTxt.text = ("Upgrade " + gen1.lvlCost);
            else
                gen1LvlCostTxt.text = ("Max");

            //gen2 txt
            gen2Txt.text = ("" + gen2.moneyGen);
            if (gen2.lvl == 0)
                gen2LvlCostTxt.text = ("Unlock " + gen2.lvlCost);
            if (gen2.lvl >= 1)
            {
                if (gen2.lvl <= 9)
                    gen2LvlCostTxt.text = ("Upgrade " + gen2.lvlCost);
                else
                    gen2LvlCostTxt.text = ("Max");
            }

            //gen3 txt
            gen3Txt.text = ("" + gen3.moneyGen);
            if (gen3.lvl == 0)
                gen3LvlCostTxt.text = ("Unlock " + gen3.lvlCost);
            if (gen3.lvl >= 1)
            {
                if (gen3.lvl <= 9)
                    gen3LvlCostTxt.text = ("Upgrade " + gen3.lvlCost);
                else
                    gen3LvlCostTxt.text = ("Max");
            }

            //gen4 txt
            gen4Txt.text = ("" + gen4.moneyGen);
            if (gen4.lvl == 0)
                gen4LvlCostTxt.text = ("Unlock " + gen4.lvlCost);
            if (gen4.lvl >= 1)
            {
                if (gen4.lvl <= 9)
                    gen4LvlCostTxt.text = ("Upgrade " + gen4.lvlCost);
                else
                    gen4LvlCostTxt.text = ("Max");
            }

            //gen5 txt
            gen5Txt.text = ("" + gen5.moneyGen);
            if (gen5.lvl == 0)
                gen5LvlCostTxt.text = ("Unlock " + gen5.lvlCost);
            if (gen5.lvl >= 1)
            {
                if (gen5.lvl <= 9)
                    gen5LvlCostTxt.text = ("Upgrade " + gen5.lvlCost);
                else
                    gen5LvlCostTxt.text = ("Max");
            }

            //life gen
            if (tengoLifeGen)
            {
                lifeGenBar.fillAmount = (float)gen6.genTimer / (float)gen6.timeToMake;
            }


            if (!isSpawning) return;


            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
            {
                enemiesLeftToSpawn--;
                enemiesAlive++;
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
            }

            if (enemiesAlive <= 0 && enemiesLeftToSpawn == 0)
                endRound();
        }
    }

    public void setSeleceted()
    {

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private int EnemiesPerRound()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(round, difficultyScalingFactor));
    }

    private float EPS()
    {
        return Mathf.Clamp((enemiesPerSecond * Mathf.Pow(round, difficultyScalingFactor)), 0f , enemiesPerSecCap);
    }



    public void hideIdle()
    {
        idle.transform.SetLocalPositionAndRotation(hI, Quaternion.identity);
        hide.SetActive(false);
        show.SetActive(true);
    }

    public void showIdle()
    {
        idle.transform.SetLocalPositionAndRotation(sI, Quaternion.identity);
        hide.SetActive(true);
        show.SetActive(false);
    }

    public void upgradeGen1()
    {
        if (gen1.lvlCost <= money)
        {
            if (gen1.lvl <= 8)
            {
                gen1.moneyGen += (gen1.moneyGen / 2);
                money -= gen1.lvlCost;
                gen1.lvl++;
                gen1.lvlCost += (gen1.lvlCost / 3);
            }
            else if (gen1.lvl == 9)
            {
                gen1.moneyGen += (gen1.moneyGen / 2);
                money -= gen1.lvlCost;
                gen1.lvl++;
                gen1.lvlCost += (gen1.lvlCost / 3);
                ll.InsertHead(1);
                ll.getCount();
                ll.addLifeGen();
            }
        }
    }

    public void upgradeGen2()
    {
        if (gen2.lvlCost <= money)
        {
            if (gen2.lvl == 0)
            {
                gen2.lvl++;
                gen2.lvlCost += gen2.lvlCost;
            }
            else if (gen2.lvl <= 8 && gen2.lvl >= 1)
            {
                gen2.moneyGen += (gen2.moneyGen / 2);
                money -= gen2.lvlCost;
                gen2.lvl++;
                gen2.lvlCost += (gen2.lvlCost / 3);
            }
            else if (gen2.lvl == 9)
            {
                gen2.moneyGen += (gen2.moneyGen / 2);
                money -= gen2.lvlCost;
                gen2.lvl++;
                gen2.lvlCost += (gen2.lvlCost / 3);
                ll.InsertHead(2);
                ll.getCount();
                ll.addLifeGen();
            }
        }
    }

    public void upgradeGen3()
    {
        if (gen3.lvlCost <= money)
        {
            if (gen3.lvl == 0)
            {
                gen3.lvl++;
                gen3.lvlCost += gen3.lvlCost;
            }
            else if (gen3.lvl <= 8 && gen3.lvl >= 1)
            {
                gen3.moneyGen += (gen3.moneyGen / 2);
                money -= gen3.lvlCost;
                gen3.lvl++;
                gen3.lvlCost += (gen3.lvlCost / 3);
            }
            else if (gen3.lvl == 9)
            {
                gen3.moneyGen += (gen3.moneyGen / 2);
                money -= gen3.lvlCost;
                gen3.lvl++;
                gen3.lvlCost += (gen3.lvlCost / 3);
                ll.InsertHead(3);
                ll.getCount();
                ll.addLifeGen();
            }
        }
    }

    public void upgradeGen4()
    {
        if (gen4.lvlCost <= money)
        {
            if (gen4.lvl == 0)
            {
                gen4.lvl++;
                gen4.lvlCost += gen4.lvlCost;
            }
            else if (gen4.lvl <= 8 && gen4.lvl >= 1)
            {
                gen4.moneyGen += (gen4.moneyGen / 2);
                money -= gen4.lvlCost;
                gen4.lvl++;
                gen4.lvlCost += (gen4.lvlCost / 3);
            }
            else if (gen4.lvl == 9)
            {
                gen4.moneyGen += (gen4.moneyGen / 2);
                money -= gen4.lvlCost;
                gen4.lvl++;
                gen4.lvlCost += (gen4.lvlCost / 3);
                ll.InsertHead(4);
                ll.getCount();
                ll.addLifeGen();
            }
        }
    }

    public void upgradeGen5()
    {
        if (gen5.lvlCost <= money)
        {
            if (gen5.lvl == 0)
            {
                gen5.lvl++;
                gen5.lvlCost += gen5.lvlCost;
            }
            else if (gen5.lvl <= 8 && gen5.lvl >= 1)
            {
                gen5.moneyGen += (gen5.moneyGen / 2);
                money -= gen5.lvlCost;
                gen5.lvl++;
                gen5.lvlCost += (gen5.lvlCost / 3);
            }
            else if (gen5.lvl == 9)
            {
                gen5.moneyGen += (gen5.moneyGen / 2);
                money -= gen5.lvlCost;
                gen5.lvl++;
                gen5.lvlCost += (gen5.lvlCost / 3);
                ll.InsertHead(5);
                ll.getCount();
                ll.addLifeGen();
            }
        }
    }

    public void unlockLifeGen()
    {
        lifeGenInfo.SetActive(true);
        gen6.isActive = true;
        tengoLifeGen = true;
    }

    public bool spendMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
            return true;
        }
        else
        {
            return false;
            //dont have enough money
        }
    }


    public void endRound()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0;
        enemy1.roundEnd();
        enemy2.roundEnd();
        enemy3.roundEnd();
        round++;
        enemiesPerSecond *= 1.025f;
        GetComponent<SaveLoad>().SaveProfile();
        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerRound();
        enemiesPerSecond = EPS();
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, startPoint.position, Quaternion.identity);
    }

    public void death()
    {
        SceneManager.LoadScene(0);
        GetComponent<SaveLoad>().deleteProfile();
    }

    public void newGame()
    {
        death();
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        SceneManager.LoadScene(1);
        GetComponent<SaveLoad>().loadgame();
    }

    public void quit()
    {
        Application.Quit();
    }
}

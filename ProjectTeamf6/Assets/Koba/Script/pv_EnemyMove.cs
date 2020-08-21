using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class pv_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力")]
    private float health = 5;
    [SerializeField, Header("攻撃力")]
    private float damage = 5;
    [SerializeField, Header("スピード")]
    private float speed = 5;
    [SerializeField, Header("状態の更新時間"), Range(0, 100)]
    private float updateTime = 3.0f;
    [SerializeField, Header("ノックバック距離"), Range(0, 100)]
    private float knockBack = 2.0f;
    [SerializeField]
    private Player playerScript;
    [SerializeField]
    private SearchAreaMove searchScript;

    Base village1Script;
    Base village2Script;
    Base village3Script;
    Base village4Script;

    GameObject player;
    GameObject playerBase;
    GameObject village1;
    GameObject village2;
    GameObject village3;
    GameObject village4;
    GameObject attractObj;

    public GameObject walk1;
    public GameObject walk2;

    Rigidbody2D rb;

    Vector2 oldPos;

    Vector3 playerPos, playerBasePos, village1Pos, village2Pos, village3Pos, village4Pos, attractObjPos;
    Vector3 targetPosNoma;
    /*
    float pex, pey, pesq;
    float pbex, pbey, pbesq;
    float pv1ex, pv1ey, pv1esq;
    float pv2ex, pv2ey, pv2esq;
    float pv3ex, pv3ey, pv3esq;
    float pv4ex, pv4ey, pv4esq;
    */
    float sqrMin;

    public int village1Judg;
    public int village2Judg;
    public int village3Judg;
    public int village4Judg;

    string forward;
    string varti;

    //ゾンビの状態
    float state;

    float EnemySX, EnemySY;
    float playerDamage;
    float currentTime;

    //public bool isAttract;
    bool isDamage;
    public bool isSearchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //CSVの読み込み
        {
            /*
            string path = @"C:\Users\yuta\Documents\GitHub\ProjectTeam6F\ProjectTeamf6\Assets\Koba\CSV\zombie_parameter.csv";
            //var csv = Resources.Load(path) as TextAsset;
            //var sr = new StringReader(csv.text);
            Debug.Log(path);
            //var cr = new CsvReader(sr);
            //cr.Configuration.RegisterClassMap<>
        */
        
        }

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerBase = GameObject.Find("playerBase");
        village1 = GameObject.Find("village1");
        village2 = GameObject.Find("village2");
        village3 = GameObject.Find("playerVillage1");
        village4 = GameObject.Find("playerVillage2");

        playerScript = player.GetComponent<Player>();
        village1Script = village1.GetComponent<Base>();
        village2Script = village2.GetComponent<Base>();
        village3Script = village3.GetComponent<Base>();
        village4Script = village4.GetComponent<Base>();

        currentTime = 3.0f;

        int rand = Random.Range(1, 5);

        if (rand == 1)
        {
            health = CSVReader.csvIntDatas[0, 1];
            damage = CSVReader.csvIntDatas[0, 2];
            speed = CSVReader.csvIntDatas[0, 3];
        }
        if (rand == 2)
        {
            health = CSVReader.csvIntDatas[1, 1];
            damage = CSVReader.csvIntDatas[1, 2];
            speed = CSVReader.csvIntDatas[1, 3];
        }
        if (rand == 3)
        {
            health = CSVReader.csvIntDatas[2, 1];
            damage = CSVReader.csvIntDatas[2, 2];
            speed = CSVReader.csvIntDatas[2, 3];
        }
        if (rand == 4)
        {
            health = CSVReader.csvIntDatas[3, 1];
            damage = CSVReader.csvIntDatas[3, 2];
            speed = CSVReader.csvIntDatas[3, 3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = rb.velocity;
        //Debug.Log(CSVReader.csvIntDatas[0][1]);
        //velocity = Vector2.zero;
        playerPos = player.transform.position;
        if (playerBase != null)
        {
            playerBasePos = playerBase.transform.position;
        }

        if (village1 != null)
        {
            village1Pos = village1.transform.position;
        }
        if (village2 != null)
        {
            village2Pos = village2.transform.position;
        }
        if (village3 != null)
        {
            village3Pos = village3.transform.position;
        }
        if (village4 != null)
        {
            village4Pos = village4.transform.position;
        }

        if (attractObj == null)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            attractObj = GameObject.Find("attractObject(Clone)");
            isSearchPlayer = searchScript.RetrunIsSearchPlayer();
            if (isSearchPlayer == true)
            {
                gameObject.layer = LayerMask.NameToLayer("AttractEnemy");
                state = 1;
            }
            else {
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                currentTime += Time.deltaTime;
                if (updateTime < currentTime)
                {

                    //一番近い地点を出す
                    {
                        village1Judg = village1Script.ReturnBaseType();
                        village2Judg = village2Script.ReturnBaseType();
                        village3Judg = village3Script.ReturnBaseType();
                        village4Judg = village4Script.ReturnBaseType();

                        if ((playerPos - transform.position).sqrMagnitude
                            < (playerBasePos - transform.position).sqrMagnitude)
                        {
                            sqrMin = (playerPos - transform.position).sqrMagnitude;
                            state = 1;
                        }
                        else
                        {
                            sqrMin = (playerBasePos - transform.position).sqrMagnitude;
                            state = 2;
                        }

                        if (sqrMin > ((village1Pos - transform.position).sqrMagnitude)
                            && village1Judg != 4)
                        {
                            sqrMin = (village1Pos - transform.position).sqrMagnitude;
                            state = 3;
                        }

                        if (sqrMin > ((village2Pos - transform.position).sqrMagnitude)
                            && village2Judg != 4)
                        {
                            sqrMin = (village2Pos - transform.position).sqrMagnitude;
                            state = 4;
                        }

                        if (sqrMin > ((village3Pos - transform.position).sqrMagnitude)
                            && village3Judg != 4)
                        {
                            sqrMin = (village3Pos - transform.position).sqrMagnitude;
                            state = 5;
                        }

                        if (sqrMin > ((village4Pos - transform.position).sqrMagnitude)
                            && village4Judg != 4)
                        {
                            sqrMin = (village4Pos - transform.position).sqrMagnitude;
                            state = 6;
                        }
                    }
                    currentTime = 0.0f;
                }
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("AttractEnemy");
            state = 7;
        }

        Debug.Log(state);

        if (state == 1)
        {
            State1();
        }
        if (state == 2)
        {
            State2();
        }
        if (state == 3)
        {
            State3();
        }
        if (state == 4)
        {
            State4();
        }
        if (state == 5)
        {
            State5();
        }
        if (state == 6)
        {
            State6();
        }
        if (state == 7)
        {
            State7();
        }

        //transform.position += new Vector3(EnemySX, EnemySY);
        transform.position += targetPosNoma * speed;

        forward = (oldPos.x > transform.position.x) ? "left" : (oldPos.x < transform.position.x) ? "right" : forward;
        varti = (oldPos.y > transform.position.y) ? "down" : (oldPos.y < transform.position.y) ? "up" : varti;
        Debug.Log(forward);
        Debug.Log(varti);
        ChangeSprite();
        if (isDamage == true)
        {
            Damage();
        }
        IsDestroy();
        oldPos = transform.position;
        //velocity += new Vector2(EnemySX, EnemySY);
        //rb.velocity = velocity;
    }

    void State1()
    {

        targetPosNoma = (playerPos - transform.position).normalized;
    }

    void State2()
    {
        targetPosNoma = (playerBasePos - transform.position).normalized;
    }

    void State3()
    {
        targetPosNoma = (village1Pos - transform.position).normalized;
    }
    void State4()
    {
        targetPosNoma = (village2Pos - transform.position).normalized;
    }

    void State5()
    {
        targetPosNoma = (village3Pos - transform.position).normalized;
    }

    void State6()
    {
        targetPosNoma = (village4Pos - transform.position).normalized;
    }

    void State7()
    {
        attractObjPos = attractObj.transform.position;
        targetPosNoma = (attractObjPos - transform.position).normalized;
    }

    void Damage()
    {
        playerDamage = playerScript.ReturnAttackP();
        health -= playerDamage;

        if (forward == "left")
        {
            if (varti == "down")
            {
                transform.position += new Vector3(knockBack, knockBack, 0.0f);
            }
            if (varti == "up")
            {
                transform.position += new Vector3(knockBack, -knockBack, 0.0f);
            }
        }
        if (forward == "right")
        {
            if (varti == "down")
            {
                transform.position += new Vector3(-knockBack, knockBack, 0.0f);
            }
            if (varti == "up")
            {
                transform.position += new Vector3(-knockBack, -knockBack, 0.0f);
            }
        }

        isDamage = false;

    }

    void ChangeSprite()
    {
        if (forward == "left")
        {
            walk1.SetActive(true);
            walk2.SetActive(false);
        }
        if (forward == "right")
        {
            walk1.SetActive(false);
            walk2.SetActive(true);
        }
    }

    void IsDestroy()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Attack"))
        {
            isDamage = true;
        }
    }

    /*
    void OnTriggerExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Attack"))
        {
            isDamage = false;
        }
    }
    */

    public float ReturnEnemyAttackP()
    {
        return damage;
    }
}

/*
public class ZombieMapper : CsvHelper.Configuration.ClassMap<ZombieParameter>
{
    public ZombieParameter()
    {
        Map(x => x.Name).Index(0);
        Map(x => x.Hp).Index(0);
        Map(x => x.Atk).Index(0);
        Map(x => x.Speed).Index(0);
    }
}
*/
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
    [SerializeField, Header("注意を引くエリア"), Range(0, 100)]
    private float attractSeachArea = 4.0f;
    [SerializeField]
    private Player playerScript;
    [SerializeField]
    private SearchAreaMove searchScript;

    public Sprite defZombie_r;
    public Sprite defZombie_l;
    public Sprite hpZombie_r;
    public Sprite hpZombie_l;
    public Sprite atkZombie_r;
    public Sprite atkZombie_l;
    public Sprite speedZombie_r;
    public Sprite speedZombie_l;
    SpriteRenderer mainSpriteRender;
    int spriteNum;

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

    //public GameObject walk1;
    //public GameObject walk2;

    Rigidbody2D rb;

    Vector2 oldPos;

    Vector3 playerPos, playerBasePos, village1Pos, village2Pos, village3Pos, village4Pos, attractObjPos;
    Vector3 targetPosNoma;

    Vector3 relayPoint1,relayPoint2, relayPoint3, relayPoint4, relayPoint5,relayPoint6;

    /*
    float pex, pey, pesq;
    float pbex, pbey, pbesq;
    float pv1ex, pv1ey, pv1esq;
    float pv2ex, pv2ey, pv2esq;
    float pv3ex, pv3ey, pv3esq;
    float pv4ex, pv4ey, pv4esq;
    */
    float sqrMin;
    Vector3 startPos;
    float startPosMagni;
    int curTime;
    int rps;

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

    float oldState;
    float newState;

    bool isRelayPointMove;
    bool inAttractArea;
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

        mainSpriteRender = gameObject.GetComponent<SpriteRenderer>();

        currentTime = 3.0f;

        oldState = 0;

        isRelayPointMove = false;
        inAttractArea = false;

        int rand = Random.Range(1, 5);

        if (rand == 1)
        {
            health = CSVReader.csvIntDatas[0, 1];
            damage = CSVReader.csvIntDatas[0, 2];
            speed = CSVReader.csvIntDatas[0, 3];
            spriteNum = 1;
        }
        if (rand == 2)
        {
            health = CSVReader.csvIntDatas[1, 1];
            damage = CSVReader.csvIntDatas[1, 2];
            speed = CSVReader.csvIntDatas[1, 3];
            spriteNum = 2;
        }
        if (rand == 3)
        {
            health = CSVReader.csvIntDatas[2, 1];
            damage = CSVReader.csvIntDatas[2, 2];
            speed = CSVReader.csvIntDatas[2, 3];
            spriteNum = 3;
        }
        if (rand == 4)
        {
            health = CSVReader.csvIntDatas[3, 1];
            damage = CSVReader.csvIntDatas[3, 2];
            speed = CSVReader.csvIntDatas[3, 3];
            spriteNum = 4;
        }


    }

    // Update is called once per frame
    void Update()
    {
        var velocity = rb.velocity;
        velocity = Vector3.zero;

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

        attractObj = GameObject.Find("attractObject(Clone)");
        //Debug.Log(attractObjPos);

        if (attractObj != null)
        {
            attractObjPos = attractObj.transform.position;
            if ((attractObjPos - transform.position).magnitude < attractSeachArea)
            {
                inAttractArea = true;
            }
        }
        else
            inAttractArea = false;

        if (inAttractArea != true)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
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
                        
                        /*
                        if (state != oldState)
                        {
                            isRelayPointMove = true;
                        }
                        */
                    }
                    currentTime = 0.0f;
                }
                //}
                // else
                //  relayPointState();
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("AttractEnemy");
            state = 7;
        }

        //Debug.Log(state);

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

        oldState = state;
        //transform.position += targetPosNoma * speed;
        Vector2 nomaVec2 = targetPosNoma;
        //Debug.Log(targetPosNoma);
        //Debug.Log(nomaVec2);
        velocity += nomaVec2 * speed;
        rb.velocity = velocity;
        /*
        forward = (oldPos.x > transform.position.x) ? "left" : (oldPos.x < transform.position.x) ? "right" : forward;
        varti = (oldPos.y > transform.position.y) ? "down" : (oldPos.y < transform.position.y) ? "up" : varti;
        */

        forward = (velocity.x < 0) ? "left" : (velocity.x > 0) ? "right" : forward;
        varti = (velocity.y < 0) ? "down" : (velocity.y > 0) ? "up" : varti;

        //Debug.Log(forward);
        //Debug.Log(varti);
        ChangeSprite();
        if (isDamage == true)
        {
            Damage();
        }
        IsDestroy();
        oldPos = transform.position;
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
        //attractObjPos = attractObj.transform.position;
        targetPosNoma = (attractObjPos - transform.position).normalized;
    }

    /*
    void relayPointState()
    {
        
        float sqrMagMin;

        if ((relayPoint1 - transform.position).sqrMagnitude < (relayPoint2 - transform.position).sqrMagnitude)
        {
            sqrMagMin = (relayPoint1 - transform.position).sqrMagnitude;
            rps = 1;
        }
        else
        {
            sqrMagMin = (relayPoint2 - transform.position).sqrMagnitude;
            rps = 2;
        }

        if (sqrMagMin > (relayPoint3 - transform.position).sqrMagnitude)
        {
            sqrMagMin = (relayPoint3 - transform.position).sqrMagnitude;
            rps = 3;
        }

        if (sqrMagMin > (relayPoint4 - transform.position).sqrMagnitude)
        {
            sqrMagMin = (relayPoint4 - transform.position).sqrMagnitude;
            rps = 4;
        }

        if (sqrMagMin > (relayPoint5 - transform.position).sqrMagnitude)
        {
            sqrMagMin = (relayPoint5 - transform.position).sqrMagnitude;
            rps = 5;
        }

        if (curTime == 0)
        {
            startPos = transform.position;
            curTime++;
        }

        if (rps == 1)
        {
            var velocity = rb.velocity;
            velocity = Vector3.zero;
            targetPosNoma = (relayPoint1 - transform.position).normalized;
            Vector2 nomaVec2 = targetPosNoma;
            velocity += nomaVec2 * speed;
            if ((startPos - transform.position).sqrMagnitude > (startPos - relayPoint1).sqrMagnitude)
            {
                isRelayPointMove = false;
                curTime = 0;
            }
        }

        if (rps == 2)
        {
            var velocity = rb.velocity;
            velocity = Vector3.zero;
            targetPosNoma = (relayPoint2 - transform.position).normalized;
            Vector2 nomaVec2 = targetPosNoma;
            velocity += nomaVec2 * speed;
            if ((startPos - transform.position).sqrMagnitude > (startPos - relayPoint2).sqrMagnitude)
            {
                isRelayPointMove = false;
                curTime = 0;
            }
        }

        if (rps == 3)
        {
            var velocity = rb.velocity;
            velocity = Vector3.zero;
            targetPosNoma = (relayPoint3 - transform.position).normalized;
            Vector2 nomaVec2 = targetPosNoma;
            velocity += nomaVec2 * speed;
            if ((startPos - transform.position).sqrMagnitude > (startPos - relayPoint3).sqrMagnitude)
            {
                isRelayPointMove = false;
                curTime = 0;
            }
        }

        if (rps == 4)
        {
            var velocity = rb.velocity;
            velocity = Vector3.zero;
            targetPosNoma = (relayPoint4 - transform.position).normalized;
            Vector2 nomaVec2 = targetPosNoma;
            velocity += nomaVec2 * speed;
            if ((startPos - transform.position).sqrMagnitude > (startPos - relayPoint4).sqrMagnitude)
            {
                isRelayPointMove = false;
                curTime = 0;
            }
        }

        if (rps == 5)
        {
            var velocity = rb.velocity;
            velocity = Vector3.zero;
            targetPosNoma = (relayPoint5 - transform.position).normalized;
            Vector2 nomaVec2 = targetPosNoma;
            velocity += nomaVec2 * speed;
            if ((startPos - transform.position).sqrMagnitude > (startPos - relayPoint5).sqrMagnitude)
            {
                isRelayPointMove = false;
                curTime = 0;
            }
        }
    }
    */

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

    void Dijkstra(Vector3 tagepos)
    {
        int dijkState = 0;
        float dijkSqrMin = 0;
        if ((relayPoint1 - transform.position).sqrMagnitude
                           < (relayPoint2 - transform.position).sqrMagnitude)
        {
            dijkSqrMin = (relayPoint1 - transform.position).sqrMagnitude;
            dijkState = 1;
        }
        else
        {
            dijkSqrMin = (relayPoint2 - transform.position).sqrMagnitude;
            dijkState = 2;
        }

        if (sqrMin > ((relayPoint3 - transform.position).sqrMagnitude))
        {
            dijkSqrMin = (relayPoint3 - transform.position).sqrMagnitude;
            dijkState = 3;
        }

        if (sqrMin > ((relayPoint4 - transform.position).sqrMagnitude))
        {
            dijkSqrMin = (relayPoint4 - transform.position).sqrMagnitude;
            dijkState = 4;
        }

        if (sqrMin > ((relayPoint5 - transform.position).sqrMagnitude))
        {
            dijkSqrMin = (relayPoint5 - transform.position).sqrMagnitude;
            dijkState = 5;
        }

        if (sqrMin > ((relayPoint6 - transform.position).sqrMagnitude))
        {
            dijkSqrMin = (relayPoint6 - transform.position).sqrMagnitude;
            dijkState = 6;
        }

        
    }
    
    void ChangeSprite()
    {
        if(spriteNum == 1)
        {
            if (forward == "left")
            {
                mainSpriteRender.sprite = defZombie_l;
            }
            if (forward == "right")
            {
                mainSpriteRender.sprite = defZombie_r;
            }
        }
        if(spriteNum == 2)
        {
            if (forward == "left")
            {
                mainSpriteRender.sprite = hpZombie_l;
            }
            if (forward == "right")
            {
                mainSpriteRender.sprite = hpZombie_r;
            }
        }
        if (spriteNum == 3)
        {
            if (forward == "left")
            {
                mainSpriteRender.sprite = atkZombie_l;
            }
            if (forward == "right")
            {
                mainSpriteRender.sprite = atkZombie_r;
            }
        }
        if (spriteNum == 4)
        {
            if (forward == "left")
            {
                mainSpriteRender.sprite = speedZombie_l;
            }
            if (forward == "right")
            {
                mainSpriteRender.sprite = speedZombie_r;
            }
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

        if (other.gameObject.CompareTag("SpecialAttack"))
        {
            health -= 200;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Village"))
        {
            transform.position += new Vector3(1.0f, 1.0f, 0.0f);
            //Debug.Log("当たりました");
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attractSeachArea);
    }
}

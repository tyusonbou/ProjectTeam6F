using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pv_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力"), Range(0, 100)]
    private float health = 5;
    [SerializeField, Header("攻撃力"), Range(0, 100)]
    private float damage = 5;
    [SerializeField, Header("スピード"), Range(0, 100)]
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

    Rigidbody2D rb;

    Vector2 oldPos;

    Vector2 playerPos, playerBasePos, village1Pos, village2Pos, village3Pos, village4Pos, attractObjPos;
    float pex, pey, pesq;
    float pbex, pbey, pbesq;
    float pv1ex, pv1ey, pv1esq;
    float pv2ex, pv2ey, pv2esq;
    float pv3ex, pv3ey, pv3esq;
    float pv4ex, pv4ey, pv4esq;
    float sqrMax;

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
            health = 100.0f;
            damage = 5.0f;
            //speed = 0.5f;
        }
        if (rand == 2)
        {
            health = 200.0f;
            damage = 5.0f;
            //speed = 0.1f;
        }
        if (rand == 3)
        {
            health = 50.0f;
            damage = 10.0f;
            //speed = 0.5f;
        }
        if (rand == 4)
        {
            health = 50.0f;
            damage = 5.0f;
            //speed = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = rb.velocity;

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
                    //プレイヤーとの距離をだす
                    {
                        pex = (playerPos.x - transform.position.x);
                        pey = (playerPos.y - transform.position.y);
                        pesq = Mathf.Sqrt((pex * pex) + (pey * pey));
                    }

                    //プレイヤーの本拠地との距離を出す
                    {
                        pbex = (playerBasePos.x - transform.position.x);
                        pbey = (playerBasePos.y - transform.position.y);
                        pbesq = Mathf.Sqrt((pbex * pbex) + (pbey * pbey));
                    }

                    //村1との距離を出す
                    {
                        pv1ex = (village1Pos.x - transform.position.x);
                        pv1ey = (village1Pos.y - transform.position.y);
                        pv1esq = Mathf.Sqrt((pv1ex * pv1ex) + (pv1ey * pv1ey));
                    }

                    //村2との距離を出す
                    {
                        pv2ex = (village2Pos.x - transform.position.x);
                        pv2ey = (village2Pos.y - transform.position.y);
                        pv2esq = Mathf.Sqrt((pv2ex * pv2ex) + (pv2ey * pv2ey));
                    }

                    //村3との距離を出す
                    {
                        pv3ex = (village3Pos.x - transform.position.x);
                        pv3ey = (village3Pos.y - transform.position.y);
                        pv3esq = Mathf.Sqrt((pv3ex * pv3ex) + (pv3ey * pv3ey));
                    }

                    //村4との距離を出す
                    {
                        pv4ex = (village4Pos.x - transform.position.x);
                        pv4ey = (village4Pos.y - transform.position.y);
                        pv4esq = Mathf.Sqrt((pv4ex * pv4ex) + (pv4ey * pv4ey));
                    }

                    //一番近い地点を出す
                    {
                        village1Judg = village1Script.ReturnBaseType();
                        village2Judg = village2Script.ReturnBaseType();
                        village3Judg = village3Script.ReturnBaseType();
                        village4Judg = village4Script.ReturnBaseType();

                        if (pesq < pbesq)
                        {
                            sqrMax = pesq;
                            state = 1;
                        }
                        else
                        {
                            sqrMax = pbesq;
                            state = 2;
                        }

                        if (sqrMax > pv1esq && village1Judg != 4)
                        {
                            sqrMax = pv1esq;
                            state = 3;
                        }

                        if (sqrMax > pv2esq && village2Judg != 4)
                        {
                            sqrMax = pv2esq;
                            state = 4;
                        }

                        if (sqrMax > pv3esq && village3Judg != 4)
                        {
                            sqrMax = pv3esq;
                            state = 5;
                        }

                        if (sqrMax > pv4esq && village4Judg != 4)
                        {
                            sqrMax = pv4esq;
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

        transform.position += new Vector3(EnemySX, EnemySY);

        forward = (oldPos.x > transform.position.x) ? "left" : (oldPos.x > transform.position.x) ? "right" : forward;
        varti = (oldPos.y > transform.position.y) ? "down" : (oldPos.y < transform.position.y) ? "up" : varti;
        Debug.Log(forward);
        Debug.Log(varti);
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
        pex = (playerPos.x - transform.position.x);
        pey = (playerPos.y - transform.position.y);
        pesq = Mathf.Sqrt((pex * pex) + (pey * pey));
        EnemySX = pex / pesq * speed;
        EnemySY = pey / pesq * speed;
    }

    void State2()
    {
        pbex = (playerBasePos.x - transform.position.x);
        pbey = (playerBasePos.y - transform.position.y);
        pbesq = Mathf.Sqrt((pbex * pbex) + (pbey * pbey));
        EnemySX = pbex / pbesq * speed;
        EnemySY = pbey / pbesq * speed;
        //transform.position += new Vector3(EnemySX, EnemySY);
    }

    void State3()
    {
        pv1ex = (village1Pos.x - transform.position.x);
        pv1ey = (village1Pos.y - transform.position.y);
        pv1esq = Mathf.Sqrt((pv1ex * pv1ex) + (pv1ey * pv1ey));
        EnemySX = pv1ex / pv1esq * speed;
        EnemySY = pv1ey / pv1esq * speed;
    }
    void State4()
    {
        pv2ex = (village2Pos.x - transform.position.x);
        pv2ey = (village2Pos.y - transform.position.y);
        pv2esq = Mathf.Sqrt((pv2ex * pv2ex) + (pv2ey * pv2ey));
        EnemySX = pv2ex / pv2esq * speed;
        EnemySY = pv2ey / pv2esq * speed;
    }

    void State5()
    {
        pv3ex = (village3Pos.x - transform.position.x);
        pv3ey = (village3Pos.y - transform.position.y);
        pv3esq = Mathf.Sqrt((pv3ex * pv3ex) + (pv3ey * pv3ey));
        EnemySX = pv3ex / pv3esq * speed;
        EnemySY = pv3ey / pv3esq * speed;
    }

    void State6()
    {
        pv4ex = (village4Pos.x - transform.position.x);
        pv4ey = (village4Pos.y - transform.position.y);
        pv4esq = Mathf.Sqrt((pv4ex * pv4ex) + (pv4ey * pv4ey));
        EnemySX = pv4ex / pv4esq * speed;
        EnemySY = pv4ey / pv4esq * speed;
    }

    void State7()
    {
        attractObjPos = attractObj.transform.position;
        float x = (attractObjPos.x - transform.position.x);
        float y = (attractObjPos.y - transform.position.y);
        float xysqr = Mathf.Sqrt((x * x) + (y * y));
        EnemySX = x / xysqr * speed;
        EnemySY = y / xysqr * speed;
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
        if(forward == "right")
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

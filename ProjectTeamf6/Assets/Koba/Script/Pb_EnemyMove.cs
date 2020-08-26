using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pb_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力")]
    private float health = 5;
    [SerializeField, Header("攻撃力")]
    private float atk = 5;
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

    GameObject player;
    GameObject playerBase;
    GameObject attractObj;

    public GameObject walk1;
    public GameObject walk2;

    Rigidbody2D rb;

    Vector3 playerPos, playerBasePos, attractObjPos;
    Vector3 targetPosNoma;
    float pex, pey, pesq;
    float pbex, pbey, pbesq;
    float sqrMax;

    string forward;
    string varti;

    //ゾンビの状態
    float state;

    float EnemySX, EnemySY;
    float playerDamage;
    float currentTime;

    bool inAttractArea;
    //public bool isAttract;
    bool isDamage;
    public bool isSearchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerBase = GameObject.Find("playerBase");

        playerScript = player.GetComponent<Player>();

        currentTime = 3.0f;

        int rand = Random.Range(1, 5);

        if (rand == 1)
        {
            health = CSVReader.csvIntDatas[0, 1];
            atk = CSVReader.csvIntDatas[0, 2];
            speed = CSVReader.csvIntDatas[0, 3];
        }
        if (rand == 2)
        {
            health = CSVReader.csvIntDatas[1, 1];
            atk = CSVReader.csvIntDatas[1, 2];
            speed = CSVReader.csvIntDatas[1, 3];
        }
        if (rand == 3)
        {
            health = CSVReader.csvIntDatas[2, 1];
            atk = CSVReader.csvIntDatas[2, 2];
            speed = CSVReader.csvIntDatas[2, 3];
        }
        if (rand == 4)
        {
            health = CSVReader.csvIntDatas[3, 1];
            atk = CSVReader.csvIntDatas[3, 2];
            speed = CSVReader.csvIntDatas[3, 3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = rb.velocity;
        velocity = Vector2.zero;

        playerPos = player.transform.position;
        if (playerBase != null)
        {
            playerBasePos = playerBase.transform.position;
        }
        attractObj = GameObject.Find("attractObject(Clone)");

        if (attractObj != null)
        {
            attractObjPos = attractObj.transform.position;
            if ((attractObjPos - transform.position).magnitude < attractSeachArea)
            {
                inAttractArea = true;
            }
            else
                inAttractArea = false;
        }

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
                    state = 2;
                    currentTime = 0.0f;
                }
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

        if (state == 7)
        {
            State7();
        }

        //transform.position += new Vector3(EnemySX, EnemySY);
        //transform.position += targetPosNoma * speed;
        Vector2 nomaVec2 = targetPosNoma;
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
            HitDamage();
        }
        IsDestroy();

        //velocity += new Vector2(EnemySX, EnemySY);
        //rb.velocity = velocity;
    }

    void State1()
    {
        /*
        pex = (playerPos.x - transform.position.x);
        pey = (playerPos.y - transform.position.y);
        pesq = Mathf.Sqrt((pex * pex) + (pey * pey));
        EnemySX = pex / pesq * speed;
        EnemySY = pey / pesq * speed;
    */
        targetPosNoma = (playerPos - transform.position).normalized;
    }

    void State2()
    {
        /*
        pbex = (playerBasePos.x - transform.position.x);
        pbey = (playerBasePos.y - transform.position.y);
        pbesq = Mathf.Sqrt((pbex * pbex) + (pbey * pbey));
        EnemySX = pbex / pbesq * speed;
        EnemySY = pbey / pbesq * speed;
        //transform.position += new Vector3(EnemySX, EnemySY);
    */
        targetPosNoma = (playerBasePos - transform.position).normalized;
    }

    void State7()
    {
        if (attractObj != null)
        {
            attractObjPos = attractObj.transform.position;
        }
        targetPosNoma = (attractObjPos - transform.position).normalized;
        /*
        float x = (attractObjPos.x - transform.position.x);
        float y = (attractObjPos.y - transform.position.y);
        float xysqr = Mathf.Sqrt((x * x) + (y * y));
        EnemySX = x / xysqr * speed;
        EnemySY = y / xysqr * speed;
    */
    }

    void HitDamage()
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
        if (other.gameObject.CompareTag("SpecialAttack"))
        {
            health -= 200;
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
        return atk;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attractSeachArea);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pl_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力")]
    private float health = 5;
    [SerializeField, Header("攻撃力")]
    private float atk = 5;
    [SerializeField, Header("スピード")]
    private float speed = 5;
    [SerializeField, Header("ノックバック距離"), Range(0, 100)]
    private float knockBack = 2.0f;
    [SerializeField, Header("注意を引くエリア"), Range(0, 100)]
    private float attractSeachArea = 4.0f;
    [SerializeField]
    private Player playerScript;

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

    GameObject player;
    public GameObject attractObj;

    public GameObject walk1;
    public GameObject walk2;

    Vector2 oldPos;
    Vector3 playerPos, attractObjPos;
    Vector3 targetPosNoma;

    string forward;
    string varti;

    float EnemySX, EnemySY;

    float playerDamage;

    bool inAttractArea;

    bool isDamage;

    Rigidbody2D rb;

    NavMeshAgent navMeshAge;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        navMeshAge = GetComponent<NavMeshAgent>();
        navMeshAge.updateRotation = false;
        navMeshAge.updateUpAxis = false;

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        mainSpriteRender = gameObject.GetComponent<SpriteRenderer>();

        int rand = Random.Range(1, 5);
        if (rand == 1)
        {
            health = CSVReader.csvIntDatas[0, 1];
            atk = CSVReader.csvIntDatas[0, 2];
            //speed = CSVReader.csvIntDatas[0, 3];
            navMeshAge.speed = CSVReader.csvIntDatas[0, 3];
            spriteNum = 1;
        }
        if (rand == 2)
        {
            health = CSVReader.csvIntDatas[1, 1];
            atk = CSVReader.csvIntDatas[1, 2];
            //speed = CSVReader.csvIntDatas[1, 3];
            navMeshAge.speed = CSVReader.csvIntDatas[1, 3];
            spriteNum = 2;
        }
        if (rand == 3)
        {
            health = CSVReader.csvIntDatas[2, 1];
            atk = CSVReader.csvIntDatas[2, 2];
            //speed = CSVReader.csvIntDatas[2, 3];
            navMeshAge.speed = CSVReader.csvIntDatas[2, 3];
            spriteNum = 3;
        }
        if (rand == 4)
        {
            health = CSVReader.csvIntDatas[3, 1];
            atk = CSVReader.csvIntDatas[3, 2];
            //speed = CSVReader.csvIntDatas[3, 3];
            navMeshAge.speed = CSVReader.csvIntDatas[3, 3];
            spriteNum = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = rb.velocity;
        velocity = Vector3.zero;

        attractObj = GameObject.Find("attractObject(Clone)");
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
            State1();
        }
        else
        {
            State7();
        }

        //transform.position += targetPosNoma * speed;
        Vector2 nomaVec2 = targetPosNoma;
        velocity += nomaVec2 * speed;
        rb.velocity = velocity;
        /*
        forward = (oldPos.x > transform.position.x) ? "left" : (oldPos.x < transform.position.x) ? "right" : forward;
        varti = (oldPos.y > transform.position.y) ? "down" : (oldPos.y < transform.position.y) ? "up" : varti;
        */

        //Debug.Log(velocity);
        forward = (navMeshAge.velocity.x < 0) ? "left" : (navMeshAge.velocity.x > 0) ? "right" : forward;
        varti = (navMeshAge.velocity.y < 0) ? "down" : (navMeshAge.velocity.y > 0) ? "up" : varti;
        //Debug.Log(forward);
        //Debug.Log(varti);

        ChangeSprite();
        if (isDamage == true)
        {
            HitDamage();
        }
        IsDestroy();
        oldPos = transform.position;
    }

    void State1()
    {
        playerPos = player.transform.position;

        if (navMeshAge.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            navMeshAge.SetDestination(playerPos);
        }
        //targetPosNoma = (playerPos - transform.position).normalized;

        //var velocity = rb.velocity;
    }

    void State7()
    {
        if (attractObj != null)
        {
            attractObjPos = attractObj.transform.position;

            if (navMeshAge.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                navMeshAge.SetDestination(attractObjPos);
            }
        }
        
        //targetPosNoma = (attractObjPos - transform.position).normalized;
        /*
        float x = (attractObjPos.x - transform.position.x);
        float y = (attractObjPos.y - transform.position.y);
        float xysq = Mathf.Sqrt((x * x) + (y * y));

        EnemySX = x / xysq * speed;
        EnemySY = y / xysq * speed;
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
        if (spriteNum == 1)
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
        if (spriteNum == 2)
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

    public float RetrunEnemyAttackP()
    {
        return atk;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attractSeachArea);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力")]
    private float health = 5;
    [SerializeField, Header("攻撃力")]
    private float damage = 5;
    [SerializeField, Header("スピード")]
    private float speed = 5;
    [SerializeField, Header("ノックバック距離"), Range(0, 100)]
    private float knockBack = 2.0f;
    [SerializeField]
    private Player playerScript;

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

    bool isDamage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

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
        velocity = Vector3.zero;

        if (attractObj == null)
        {
            attractObj = GameObject.Find("attractObject(Clone)");
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
        forward = (velocity.x < 0) ? "left" : (velocity.x > 0) ? "right" : forward;
        varti = (velocity.y < 0) ? "down" : (velocity.y > 0) ? "up" : varti;
        Debug.Log(forward);
        Debug.Log(varti);

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
        playerPos = player.transform.position;
        targetPosNoma = (playerPos - transform.position).normalized;

        //var velocity = rb.velocity;
    }

    void State7()
    {
        attractObjPos = attractObj.transform.position;
        targetPosNoma = (attractObjPos - transform.position).normalized;
        /*
        float x = (attractObjPos.x - transform.position.x);
        float y = (attractObjPos.y - transform.position.y);
        float xysq = Mathf.Sqrt((x * x) + (y * y));

        EnemySX = x / xysq * speed;
        EnemySY = y / xysq * speed;
    */
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

    public float RetrunEnemyAttackP()
    {
        return damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力"), Range(0, 100)]
    private float health = 5;
    [SerializeField, Header("攻撃力"), Range(0, 100)]
    private float damage = 5;
    [SerializeField, Header("スピード"), Range(0, 100)]
    private float speed = 5;
    [SerializeField, Header("ノックバック距離"), Range(0, 100)]
    private float knockBack = 2.0f;
    [SerializeField]
    private Player playerScript;

    GameObject player;
    public GameObject attractObj;

    Vector2 oldPos;
    Vector2 playerPos, attractObjPos;

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
        if (attractObj == null)
        {
            attractObj = GameObject.Find("attractObject(Clone)");
            State1();
        }
        else
        {
            State7();
        }

        transform.position += new Vector3(EnemySX, EnemySY);

        forward = (oldPos.x > transform.position.x) ? "left" : (oldPos.x > transform.position.x) ? "right" : forward;
        varti = (oldPos.y > transform.position.y) ? "down" : (oldPos.y < transform.position.y) ? "up" : varti;
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
        //var velocity = rb.velocity;

        //プレイヤーとの距離をだす
        float x = (playerPos.x - transform.position.x);
        float y = (playerPos.y - transform.position.y);
        float xysq = Mathf.Sqrt((x * x) + (y * y));

        EnemySX = x / xysq * speed;
        EnemySY = y / xysq * speed;

    }

    void State7()
    {
        attractObjPos = attractObj.transform.position;
        float x = (attractObjPos.x - transform.position.x);
        float y = (attractObjPos.y - transform.position.y);
        float xysq = Mathf.Sqrt((x * x) + (y * y));

        EnemySX = x / xysq * speed;
        EnemySY = y / xysq * speed;
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

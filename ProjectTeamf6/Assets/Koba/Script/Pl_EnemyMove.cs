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
    [SerializeField]
    private Player playerScript;

    GameObject player;
    public GameObject attractObj;

    Vector2 playerPos, attractObjPos;

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
        if (isDamage == true)
        {
            Damage();
        }
        IsDestroy();
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
        /*
        if (forward == "left")
        {
            if (varti == "down")
            {
                transform.position += new Vector3(3.0f, 3.0f, 0.0f);
            }
            if (varti == "up")
            {
                transform.position += new Vector3(3.0f, -3.0f, 0.0f);
            }
        }
        else
        {
            if (varti == "down")
            {
                transform.position += new Vector3(-3.0f, 3.0f, 0.0f);
            }
            if (varti == "up")
            {
                transform.position += new Vector3(-3.0f, -3.0f, 0.0f);
            }
        }
        */
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

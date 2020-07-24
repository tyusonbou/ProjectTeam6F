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

    public GameObject player;

    Vector2 playerPos;
    float pex, pey, pesq;
    float EnemySX, EnemySY;

    float playerDamage;

    bool isDamage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        playerPos = player.transform.position;
        var velocity = rb.velocity;

        //プレイヤーとの距離をだす
        {
            pex = (playerPos.x - transform.position.x);
            pey = (playerPos.y - transform.position.y);
            pesq = Mathf.Sqrt((pex * pex) + (pey * pey));
        }

        EnemySX = pex / pesq * speed;
        EnemySY = pey / pesq * speed;
        transform.position += new Vector3(EnemySX, EnemySY);
        if (isDamage == true)
        {
            Damage();
        }
        IsDestroy();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力"), Range(0, 100)]
    private float health = 5;
    [SerializeField, Header("攻撃力"), Range(0, 100)]
    private float damege = 5;
    [SerializeField, Header("スピード"), Range(0, 100)]
    private float speed = 5;

    public GameObject player;
    public GameObject playerBase;

    private Vector2 playerPos, playerBasePos;
    float pex, pey, pesq;
    float pbex, pbey, pbesq;
    float X,Y,Sqr,EnemySX, EnemySY;
    private Rigidbody2D rb;
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
        playerBasePos = playerBase.transform.position;
        var velocity = rb.velocity;

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

        //プレイヤーと本拠地の近いほうを出す
        if (pbesq > pesq)
        {
            X = pex;
            Y = pey;
            Sqr = pesq;
        }
        else
        {
            X = pbex;
            Y = pbey;
            Sqr = pbesq;
        }
        EnemySX = X / Sqr * speed;
        EnemySY = Y / Sqr * speed;
        transform.position += new Vector3(EnemySX, EnemySY);
    }
}

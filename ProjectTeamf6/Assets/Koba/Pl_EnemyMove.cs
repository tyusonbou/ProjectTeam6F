using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力"), Range(0, 100)]
    private float health = 5;
    [SerializeField, Header("攻撃力"), Range(0, 100)]
    private float damege = 5;
    [SerializeField, Header("スピード"), Range(0, 100)]
    private float speed = 5;
    public GameObject player;
    private Vector2 playerPos;
    float pex, pey, pesq;
    float EnemySX, EnemySY;
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
    }
}

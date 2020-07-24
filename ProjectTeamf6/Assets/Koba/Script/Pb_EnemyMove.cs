using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pb_EnemyMove : MonoBehaviour
{
    [SerializeField, Header("体力"), Range(0, 100)]
    private float health = 5;
    [SerializeField, Header("攻撃力"), Range(0, 100)]
    private float damege = 5;
    [SerializeField, Header("スピード"), Range(0, 100)]
    private float speed = 5;
    public GameObject playerBase;
    private Vector2 playerBasePos;
    float pbex, pbey, pbesq;
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
        playerBasePos = playerBase.transform.position;
        var velocity = rb.velocity;

        //プレイヤーの本拠地との距離を出す
        {
            pbex = (playerBasePos.x - transform.position.x);
            pbey = (playerBasePos.y - transform.position.y);
            pbesq = Mathf.Sqrt((pbex * pbex) + (pbey * pbey));
        }

        EnemySX = pbex / pbesq * speed;
        EnemySY = pbey / pbesq * speed;
        transform.position += new Vector3(EnemySX, EnemySY);

    }
}

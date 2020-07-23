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
    [SerializeField, Header("状態の更新時間"), Range(0, 100)]
    private float updateTime = 3.0f;

    public GameObject player;
    public GameObject playerBase;
    public GameObject village1;
    public GameObject village2;
    public GameObject village3;
    public GameObject village4;

    Rigidbody2D rb;

    Vector2 playerPos, playerBasePos, village1Pos, village2Pos, village3Pos, village4Pos;
    float pex, pey, pesq;
    float pbex, pbey, pbesq;
    float pv1ex, pv1ey, pv1esq;
    float pv2ex, pv2ey, pv2esq;
    float pv3ex, pv3ey, pv3esq;
    float pv4ex, pv4ey, pv4esq;
    float sqrMax;

    //ゾンビの状態
    float state;

    float EnemySX, EnemySY;

    float currentTime;

    bool isAttract;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTime = 3.0f;
        isAttract = false;
    }

    // Update is called once per frame
    void Update()
    {
        //var velocity = rb.velocity;
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
                if (isAttract == true)
                {
                    state = 1;
                }
                else
                {
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

                    if (sqrMax > pv1esq && village1 != null)
                    {
                        sqrMax = pv1esq;
                        state = 3;
                    }

                    if (sqrMax > pv2esq && village2 != null)
                    {
                        sqrMax = pv2esq;
                        state = 4;
                    }

                    if (sqrMax > pv3esq && village3 != null)
                    {
                        sqrMax = pv3esq;
                        state = 5;
                    }

                    if (sqrMax > pv4esq && village4 != null)
                    {
                        sqrMax = pv4esq;
                        state = 6;
                    }
                }
            }
            currentTime = 0.0f;
        }
        Debug.Log(state);

        if (state == 1 || state == 7)
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

        transform.position += new Vector3(EnemySX, EnemySY);

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
}

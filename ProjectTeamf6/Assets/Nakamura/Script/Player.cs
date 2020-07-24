using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    float WalkSped;　//歩き速度
    [SerializeField]
    float RunSpeed;　//走り速度
    [SerializeField]
    public static float PlayerHP; //プレイヤーHP
    [SerializeField]
    public static float PlayerAttack;

    public float EnemyAttack;

    private float doAttack;//攻撃コンボ用

    [SerializeField]
    GameObject attackSword;
    [SerializeField]
    GameObject attackSword2;
    [SerializeField]
    GameObject attackSword3;
    [SerializeField]
    GameObject attackBullet;
    [SerializeField]
    GameObject hatuentou;

    string MoveState;　//向き

    float LR; //左右移動用
    float UD; //上下移動用

    public float ATimer;
    public float ALimitTimer;
    public float STimer;
    public float SLimitTimer;
    public float invisibleTimer;
    public float invisibleInterval;

    [SerializeField]
    bool isAttack;
    [SerializeField]
    bool isKnockBack;
    [SerializeField]
    public bool isScream;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PlayerHP = 10;
        PlayerAttack = 5;
        MoveState = "RIGHT";

        doAttack = 0;
        attackSword.SetActive(false);
        attackSword2.SetActive(false);
        attackSword3.SetActive(false);
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        Attack();
        Bullet();
        Scream();
        KnockBack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //向き判定
    void ChangeState()
    {
        if (isAttack) { return; }

        LR = 0;
        UD = 0;

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            LR = 1;
            MoveState = "RIGHT";
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            LR = -1;
            MoveState = "LEFT";
            transform.rotation = Quaternion.Euler(180, 0, 90);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            UD = 1;
            MoveState = "UP";
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            UD = -1;
            MoveState = "DOWN";
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }

    //移動
    void Move()
    {
        if (isAttack) { return; }

        transform.position += new Vector3(LR, UD, 0).normalized * (WalkSped * Time.deltaTime);

        if (Input.GetButton("A"))
        {
            transform.position += new Vector3(LR, UD, 0).normalized * (RunSpeed * Time.deltaTime);
        }
    }

    //攻撃
    void Attack()
    {
        if (Input.GetButtonDown("Y") && !isAttack && doAttack == 0)
        {
            attackSword.SetActive(true);
            isAttack = true;
            doAttack = 1;
            ATimer = 0;
        }
        if (Input.GetButtonUp("Y") && doAttack == 1)
        {
            doAttack = 2;
        }

        if(Input.GetButtonDown("Y") && doAttack == 2)
        {
            attackSword.SetActive(false);
            attackSword2.SetActive(true);
            doAttack = 3;
            ATimer -= 10;
        }
        if (Input.GetButtonUp("Y") && doAttack == 3)
        {
            doAttack = 4;
        }

        if (Input.GetButtonDown("Y") && doAttack == 4)
        {
            attackSword2.SetActive(false);
            attackSword3.SetActive(true);
            doAttack = 5;
            ATimer -= 15;
        }
        if (Input.GetButtonUp("Y") && doAttack == 5)
        {
            doAttack = 6;
        }

        if (isAttack)
        {
            ATimer += Time.timeScale;

            if (ATimer > ALimitTimer)
            {
                attackSword.SetActive(false);
                attackSword2.SetActive(false);
                attackSword3.SetActive(false);
                isAttack = false;
              
                doAttack = 0;
            }
        }

    }

    void Bullet()
    {
        if (Input.GetButtonDown("X") && !isAttack)
        {
            Instantiate(attackBullet, transform.position , transform.rotation);
        }
    }

    void Scream()
    {
        if (Input.GetButtonDown("B") && !isScream)
        {
            Instantiate(hatuentou, transform.position, Quaternion.identity);
            isScream = true;
            STimer = 0;
        }

        if (isScream)
        {
            STimer += Time.timeScale;

            if (STimer > SLimitTimer)
            {
                isScream = false;
            }
        }
    }

    void KnockBack()
    {
        if (isKnockBack)
        {
            invisibleTimer += Time.deltaTime;
            if (invisibleTimer > invisibleInterval)
            {
                invisibleTimer = 0;
                
                isKnockBack = false;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if ((col.gameObject.tag == "Enemy") && (!isKnockBack))
    //    {
    //        isKnockBack = true;
    //        PlayerHP -= 1;

    //        Vector3 knockBackDirection = (col.gameObject.transform.position - transform.position).normalized;

    //        knockBackDirection.x *= -1;
    //        knockBackDirection.y -= 1;
    //        knockBackDirection.z += 1;

    //        rb2d.velocity = Vector2.zero;
    //        rb2d.AddForce(knockBackDirection * EnemyAttack);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if ((col.gameObject.tag == "Enemy")  && (isKnockBack))
    //    {
    //        rb2d.velocity = Vector2.zero;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Enemy") && (!isKnockBack))
        {
            isKnockBack = true;
            PlayerHP -= 1;

            Vector3 knockBackDirection = (col.gameObject.transform.position - transform.position).normalized;

            knockBackDirection.x *= -1;
            knockBackDirection.y -= 1;
            knockBackDirection.z += 1;

            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(knockBackDirection * EnemyAttack);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Enemy") && (isKnockBack))
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    public float ReturnPlayerHP()
    {
        return PlayerHP;
    }
    public float ReturnAttackP()
    {
        return PlayerAttack;
    }
}

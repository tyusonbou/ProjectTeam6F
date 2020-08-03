using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public static float WalkSped;　//歩き速度
    [SerializeField]
    float RunSpeed;　//走り速度
    
    public float PlayerHP; //プレイヤーHP
    public float PlayerMaxHP;

    public float PlayerMP;

    public float PlayerAttack;//プレイヤー攻撃力
    public float ATKRB;//攻撃時の前進速度

    public float ScremCount;//発煙筒使用回数
    public float LimitScremCount;

    private float doAttack;//攻撃コンボ用
    [SerializeField]
    List<GameObject> BulletCount = new List<GameObject>();
    public int BulletLimit;

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

    Status status;

    string MoveState;　//向き

    float LR; //左右移動用
    float UD; //上下移動用

    public float ATimer;//  攻撃時間
    public float ALimitTimer;
    public float STimer;//　発煙筒クールタイム
    public float SLimitTimer;


    [SerializeField]
    bool isAttack;
    [SerializeField]
    public bool isScream;

  

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        status = GetComponent<Status>();
        MoveState = "RIGHT";

        doAttack = 0;
        attackSword.SetActive(false);
        attackSword2.SetActive(false);
        attackSword3.SetActive(false);
        isAttack = false;

        ScremCount = 0;

        //spriteRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetStatus();
        ChangeState();
        PlayerRotate();
        Attack();
        Bullet();
        Scream();
        Death();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void SetStatus()
    {
        PlayerHP = status.HP;
        PlayerMaxHP = status.MaxHP;
        PlayerAttack = status.Attack;
        WalkSped = status.Speed;
        //if (PlayerHP >PlayerMaxHP)
        //{
        //    PlayerHP = PlayerMaxHP;
        //}
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
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            LR = -1;
            MoveState = "LEFT";    
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            UD = 1;
            MoveState = "UP";     
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            UD = -1;
            MoveState = "DOWN";       
        }
    }

    //向き変更
    void PlayerRotate()
    {
        switch (LR)
        {
            case (1):
                
                switch (UD)
                {
                    case (1):
                        transform.rotation = Quaternion.Euler(180, 0, 225);
                        break;
                    case (-1):
                        transform.rotation = Quaternion.Euler(0, 0, 225);
                        break;
                    case (0):
                        transform.rotation = Quaternion.Euler(0, 0, 270);
                        break;
                }
                break;

            case (-1):
                
                switch (UD)
                {
                    case (1):
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                        break;
                    case (-1):
                        transform.rotation = Quaternion.Euler(180, 0, 45);
                        break;
                    case (0):
                        transform.rotation = Quaternion.Euler(180, 0, 90);
                        break;
                }
                break;

            case (0):
                switch (UD)
                {
                    case (1):
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case (-1):
                        transform.rotation = Quaternion.Euler(180, 0, 0);
                        break;
                }
                break;
        }
        
    }

    //移動
    void Move()
    {
        if (isAttack) { return; }

        transform.position += new Vector3(LR, UD, 0).normalized * (WalkSped * Time.deltaTime);

        //if (Input.GetButton("A"))
        //{
        //    transform.position += new Vector3(LR, UD, 0).normalized * (WalkSped * RunSpeed * Time.deltaTime);
        //}
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
            //rb2d.AddForce(transform.up * ATKRB);
            
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
            ATimer -= 5;
            rb2d.velocity = Vector2.zero;

            rb2d.AddForce(transform.up * ATKRB * 1.2f);
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
            ATimer -= 5;
            rb2d.velocity = Vector2.zero;

            rb2d.AddForce(transform.up * ATKRB * 1.5f);
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
                rb2d.velocity = Vector2.zero;
                doAttack = 0;
            }
        }

    }

    //遠距離攻撃
    void Bullet()
    {
        if (Input.GetButtonDown("X") && !isAttack && BulletCount.Count < BulletLimit)
        {
            BulletCount.Add(Instantiate(attackBullet, transform.position, transform.rotation));
        }
        if(BulletCount.Count == BulletLimit)
        {
            BulletCount.Clear();
        }
        
    }

    //発煙設置
    void Scream()
    {
        if (Input.GetButtonDown("B") && !isScream && ScremCount < LimitScremCount)
        {
            Instantiate(hatuentou, transform.position, Quaternion.identity);
            isScream = true;
            STimer = 0;
            ScremCount += 1;
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

   

    //死亡処理
    void Death()
    {
        if (PlayerHP <= 0)
        {
            Destroy(gameObject);
        }
    }

   

    public float ReturnPlayerHP()
    {
        return PlayerHP;
    }
    public float ReturnPlayerMaxHP()
    {
        return PlayerMaxHP;
    }
    public float ReturnAttackP()
    {
        return PlayerAttack;
    }
    public float ReturnSpeed()
    {
        return WalkSped;
    }
    public float ReturnScremCount()
    {
        return ScremCount;
    }
}

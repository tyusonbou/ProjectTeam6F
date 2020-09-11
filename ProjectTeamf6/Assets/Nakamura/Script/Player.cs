using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    public static float WalkSped;　//歩き速度
    [SerializeField]
    float ChargeWalk;　//走り速度
    
    public float PlayerHP; //プレイヤーHP
    public float PlayerMaxHP;

    public float PlayerMP; //プレイヤーMP
    public float PlayerMaxMP;

    public float PlayerAttack;//プレイヤー攻撃力
    public float ATKRB;//攻撃時の前進速度

    public float ScremCount;//発煙筒使用回数
    public float LimitScremCount;

    public float ChargeTimer; //チャージ時間
    public float LimitChargeTimer; //チャージ超過させない用
    public float LimitChargeTimerDef; //チャージ時間リミット

    private float doAttack;//攻撃コンボ用
    private float EXdoAttack;//チャージ段階用

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
    [SerializeField]
    GameObject ChargeEffect;
    SpriteRenderer CESpprite;

    [SerializeField]
    GameObject EXAttack1;
    [SerializeField]
    GameObject EXAttack2;
    [SerializeField]
    GameObject EXAttack3;
    [SerializeField]
    GameObject EXAttack4;

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

    Base ATKBase1;
    Base ATKBase2;

    Animator AttackAnim1;
    Animator AttackAnim2;
    Animator AttackAnim3;

    [SerializeField]
    AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        status = GetComponent<Status>();
        MoveState = "RIGHT";

        AttackAnim1 = attackSword.GetComponent<Animator>();
        AttackAnim2 = attackSword2.GetComponent<Animator>();
        AttackAnim3 = attackSword3.GetComponent<Animator>();

        doAttack = 0;
        attackSword.SetActive(false);
        attackSword2.SetActive(false);
        attackSword3.SetActive(false);
        isAttack = false;

        EXAttack3.SetActive(false);

        ChargeEffect.SetActive(false);

        ATKBase1 = GameObject.Find("playerVillage2").GetComponent<Base>();
        ATKBase2 = GameObject.Find("village2").GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }
        if (CountDown.isCountDown) { return; }

        SetStatus();
        ChangeState();
        PlayerRotate();
        Attack();
        Bullet();
        Scream();
        EXAttack();
        Death();
    }

    private void FixedUpdate()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }
        if (CountDown.isCountDown) { return; }

        Move();
    }

    void SetStatus()
    {
        PlayerHP = status.HP;
        PlayerMaxHP = status.MaxHP;
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
        }
        PlayerAttack = status.Attack;
        WalkSped = status.Speed;
        if (PlayerMP >= PlayerMaxMP)
        {
            PlayerMP = PlayerMaxMP;
        }
        if (PlayerMP <= 0)
        {
            PlayerMP = 0;
        }

        PlayerMP += 1*Time.deltaTime;
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
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                        break;
                    case (-1):
                        transform.rotation = Quaternion.Euler(0, 0, -45);
                        break;
                    case (0):
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
                break;

            case (-1):
                
                switch (UD)
                {
                    case (1):
                        transform.rotation = Quaternion.Euler(0, 180, 45);
                        break;
                    case (-1):
                        transform.rotation = Quaternion.Euler(0, 180, -45);
                        break;
                    case (0):
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                }
                break;

            case (0):
                switch (UD)
                {
                    case (1):
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case (-1):
                        transform.rotation = Quaternion.Euler(180, 0, 90);
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
            audioSource.PlayOneShot(audioClips[0]);
            
        }
        if (Input.GetButtonUp("Y") && doAttack == 1 )
        {   //攻撃アップが1つ以上でコンボ2
            if(ATKBase1.ReturnBaf() == true || ATKBase2.ReturnBaf() == true)
            {
                doAttack = 2;
            }
        }

        if(Input.GetButtonDown("Y") && doAttack == 2)
        {
            attackSword.SetActive(false);
            attackSword2.SetActive(true);
            doAttack = 3;
            ATimer -= 5;

            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(transform.right * ATKRB * 1.2f);

            audioSource.PlayOneShot(audioClips[1]);
        }
        if (Input.GetButtonUp("Y") && doAttack == 3)
        {   //攻撃アップが2つでコンボ3
            if (ATKBase1.ReturnBaf() == true && ATKBase2.ReturnBaf() == true)
            {
                doAttack = 4;
            }
        }

        if (Input.GetButtonDown("Y") && doAttack == 4)
        {
            attackSword2.SetActive(false);
            attackSword3.SetActive(true);
            doAttack = 5;
            ATimer -= 5;

            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(transform.right * ATKRB * 1.5f);

            audioSource.PlayOneShot(audioClips[2]);
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
                
                isAttack = false;
                rb2d.velocity = Vector2.zero;
                doAttack = 0;
            }
        }

        if (AttackAnim1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            attackSword.SetActive(false);
        }
        if (AttackAnim2.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            attackSword2.SetActive(false);
        }
        if (AttackAnim3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            attackSword3.SetActive(false);
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
        if (Input.GetButtonDown("B") && !isScream && !isAttack && ScremCount > 0)
        {
            Instantiate(hatuentou, transform.position, Quaternion.identity);
            isScream = true;
            STimer = 0;
            ScremCount -= 1;
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

    //範囲攻撃
    void EXAttack()
    {
        LimitChargeTimer = LimitChargeTimerDef * (PlayerMP / PlayerMaxMP); //溜め時間上限＝MP/最大MP
        if (ChargeTimer >= LimitChargeTimer)
        {
            ChargeTimer = LimitChargeTimer;
        }

        if (Input.GetButtonDown("X"))
        {
            WalkSped = WalkSped / ChargeWalk;
            EXdoAttack = 0;
        }

        if(Input.GetButton("X") && !isAttack)
        {
            ChargeTimer += Time.deltaTime;
            
            CESpprite = ChargeEffect.GetComponent<SpriteRenderer>();

            
            if (ATKBase1.ReturnBaf() == true && ATKBase2.ReturnBaf() == true)//攻撃アップが2つでチャージ段階３，４
            {
                if (ChargeTimer >= LimitChargeTimerDef / 4 && ChargeTimer < LimitChargeTimerDef / 2)
                {
                    ChargeEffect.SetActive(true);

                    CESpprite.color = Color.blue;
                    EXdoAttack = 1;
                }
                if (ChargeTimer >= LimitChargeTimerDef / 2 && ChargeTimer < LimitChargeTimerDef * 3 / 4)
                {
                    CESpprite.color = Color.green;
                    EXdoAttack = 2;
                }
                if (ChargeTimer >= LimitChargeTimerDef * 3 / 4 && ChargeTimer < LimitChargeTimerDef)
                {
                    CESpprite.color = Color.yellow;
                    EXdoAttack = 3;
                }
                if (ChargeTimer >= LimitChargeTimerDef)
                {
                    CESpprite.color = Color.red;
                    EXdoAttack = 4;
                }
            }
            else if (ATKBase1.ReturnBaf() == true || ATKBase2.ReturnBaf() == true)//攻撃アップが1つ以上でチャージ段階１，２
            {
                if (ChargeTimer >= LimitChargeTimerDef / 2)
                {
                    ChargeTimer = LimitChargeTimerDef / 2;
                }

                if (ChargeTimer >= LimitChargeTimerDef / 4 && ChargeTimer < LimitChargeTimerDef / 2)
                {
                    ChargeEffect.SetActive(true);

                    CESpprite.color = Color.blue;
                    EXdoAttack = 1;
                }
                if (ChargeTimer >= LimitChargeTimerDef / 2 && ChargeTimer < LimitChargeTimerDef * 3 / 4)
                {
                    CESpprite.color = Color.green;
                    EXdoAttack = 2;
                }
            }
            else
            {
                if (ChargeTimer >= 0)
                {
                    ChargeTimer = 0;
                }
            }

            

            
        }
        if (Input.GetButtonUp("X"))
        {
            ChargeEffect.SetActive(false);
            ChargeTimer = 0;
            WalkSped = WalkSped * ChargeWalk;

            if (EXdoAttack == 1)
            {
                PlayerMP -= PlayerMaxMP / 4;
                Instantiate(EXAttack1, transform.position, transform.rotation);

                audioSource.PlayOneShot(audioClips[4]);
            }
            if (EXdoAttack == 2)
            {
                PlayerMP -= PlayerMaxMP / 2;
                Instantiate(EXAttack2, transform.position , transform.rotation);

                audioSource.PlayOneShot(audioClips[3]);
            }
            if (EXdoAttack == 3)
            {
                PlayerMP -= PlayerMaxMP * 3 / 4;
                EXAttack3.SetActive(true);

                audioSource.PlayOneShot(audioClips[4]);
            }
            if (EXdoAttack == 4)
            {  
                PlayerMP -= PlayerMaxMP;
                Instantiate(EXAttack4, transform.position , transform.rotation);

                audioSource.PlayOneShot(audioClips[5]);
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
    public float ReturnPlayerMP()
    {
        return PlayerMP;
    }
    public float ReturnPlayerMaxMP()
    {
        return PlayerMaxMP;
    }
    public float ReturnScremCount()
    {
        return ScremCount;
    }
}

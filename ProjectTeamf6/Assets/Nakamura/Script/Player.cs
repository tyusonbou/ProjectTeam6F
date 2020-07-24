using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float WalkSped;　//歩き速度
    [SerializeField] 
    float RunSpeed;　//走り速度
    [SerializeField]
    public static float PlayerHP; //プレイヤーHP
    [SerializeField]
    public static float PlayerAttack;

    [SerializeField]
    GameObject attackSword;
    [SerializeField]
    GameObject attackBullet;

    string MoveState;　//向き

    float LR; //左右移動用
    float UD; //上下移動用

    public float timer;
    public float limitTimer;

    [SerializeField]
    bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        MoveState = "RIGHT";
        attackSword.SetActive(false);
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        Attack();
        Bullet();
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
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            LR = -1;
            MoveState = "LEFT";
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            UD = 1;
            MoveState = "UP";
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            UD = -1;
            MoveState = "DOWN";
            transform.rotation = Quaternion.Euler(0, 180, 270);
        }
    }

    //移動
    void Move()
    {
        if (isAttack) { return; }

        transform.position += new Vector3( LR, UD, 0).normalized*(WalkSped * Time.deltaTime);

        if (Input.GetButton("A"))
        {
            transform.position += new Vector3(LR, UD, 0).normalized * (RunSpeed * Time.deltaTime);
        }
    }

    //攻撃
    void Attack()
    {
        if (Input.GetButtonDown("Y") && !isAttack)
        {
            attackSword.SetActive(true);
            isAttack = true;
            timer = 0;
        }

        if (isAttack)
        {
            timer += Time.timeScale;

            if (timer > limitTimer)
            {
                attackSword.SetActive(false);
                isAttack = false;
            }
        }
        
    }

    void Bullet()
    {
        if (Input.GetButtonDown("X"))
        {
            Instantiate(attackBullet);
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

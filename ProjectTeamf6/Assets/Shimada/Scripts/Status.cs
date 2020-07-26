using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float HP;
    public float Attack;
    public float Speed;
    public float MaxHP;

    Rigidbody2D rb2d;
    [SerializeField]
    bool isKnockBack;
    public float EnemyP;
    public float EnemyAttack;
    public float invisibleTimer;
    public float invisibleInterval;
    Renderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP >= MaxHP)
        {
            HP = MaxHP;
        }

        KnockBack();
    }

    //ノックバック
    void KnockBack()
    {
        if (isKnockBack)
        {
            invisibleTimer += Time.deltaTime;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            spriteRenderer.material.color = new Color(1f, 1f, 1f, level);
            if (invisibleTimer > invisibleInterval)
            {
                invisibleTimer = 0;

                isKnockBack = false;
            }
        }
        else
        {
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Enemy") && (!isKnockBack))
        {
            isKnockBack = true;
            HP -= EnemyP;

            Vector3 knockBackDirection = (col.gameObject.transform.position - transform.position).normalized;

            knockBackDirection.x *= -1;
            knockBackDirection.y *= -1;
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
}

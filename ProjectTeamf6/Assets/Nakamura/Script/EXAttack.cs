using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAttack : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float attackPoint;

    public float timer;
    public float limitTimer;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed;

        timer += Time.timeScale;
        if (timer > limitTimer)
        {
            Destroy(gameObject);
        }

        animator.SetFloat("Timer", timer);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}

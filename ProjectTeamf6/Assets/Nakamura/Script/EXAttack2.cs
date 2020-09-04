using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAttack2 : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float attackPoint;

    public float timer;
    public float limitTimer;

    [Range(0, 1f)]
    public float BoomTime;
    [SerializeField]
    AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < limitTimer / 2)
        {
            transform.position += transform.right * speed * curve.Evaluate(timer/limitTimer);
        }
        else 
        {
            transform.position -= transform.right * speed * curve.Evaluate(timer / limitTimer);
        }


        timer += Time.timeScale;
        if (timer > limitTimer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

    }
}

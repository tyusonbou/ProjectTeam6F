﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float attackPoint;

    public float timer;
    public float limitTimer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

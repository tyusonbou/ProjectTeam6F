using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    Status status;
    GameObject Player;
    private float AttackPoint;
    public bool ON;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("空 全体図");
        status = Player.GetComponent<Status>();
        ON = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ON)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine("AttackUp");
                ON = false;
            }
        }
    }

    IEnumerator AttackUp()
    {
        AttackPoint = status.Attack * 0.5f;
        status.Attack = status.Attack + AttackPoint;
        yield return null;
    }
}

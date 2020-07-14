using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float WalkSped;
    [SerializeField]
    float RunSpeed;

    string MoveState;

    float LR;
    float UD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ChangeState()
    {
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

    void Move()
    {
        transform.position += new Vector3(RunSpeed * Time.deltaTime * LR, RunSpeed * Time.deltaTime * UD, 0);
    }
}

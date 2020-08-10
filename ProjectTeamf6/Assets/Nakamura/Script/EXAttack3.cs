using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAttack3 : MonoBehaviour
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
        transform.Rotate(0, 0, speed);

        timer += Time.timeScale;
        if (timer > limitTimer)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

    }
}

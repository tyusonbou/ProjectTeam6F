using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseMove : MonoBehaviour
{
    [SerializeField, Header("スポン時間"), Range(0, 100)]
    private float spornTime = 3.0f;

    public GameObject enemy;
    public GameObject pb_enemy;
    public GameObject pl_enemy;
    private float currentTime = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Sporn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(enemy, transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
        currentTime += Time.deltaTime;
        if(spornTime < currentTime)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            Instantiate(pb_enemy, transform.position, Quaternion.identity);
            Instantiate(pl_enemy, transform.position, Quaternion.identity);
            currentTime = 0.0f;
        }

        //StartCoroutine("Sporn");
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}

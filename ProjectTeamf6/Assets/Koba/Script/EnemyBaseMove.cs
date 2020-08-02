using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseMove : MonoBehaviour
{
    [SerializeField, Header("スポン時間"), Range(0, 100)]
    private float spawnTime = 3.0f;
    [SerializeField, Header("スポンの上限"), Range(0, 100)]
    private float maxSpawn = 3.0f;

    public GameObject pv_enemy;
    public GameObject pl_enemy;
    public GameObject pb_enemy;

    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 3.0f;
    }

    IEnumerator Sporn()
    {
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpawn() == true)
        {
            currentTime += Time.deltaTime;
            if (spawnTime < currentTime)
            {
                Instantiate(pv_enemy, transform.position, Quaternion.identity);
                Instantiate(pl_enemy, transform.position, Quaternion.identity);
                Instantiate(pv_enemy, transform.position, Quaternion.identity);
                Instantiate(pl_enemy, transform.position, Quaternion.identity);
                Instantiate(pb_enemy, transform.position, Quaternion.identity);
                currentTime = 0.0f;
            }
        }
        //StartCoroutine("Sporn");
    }

    bool IsSpawn()
    {
        GameObject[] enemes = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemes.Length >= maxSpawn)
        {
            return false;
        }
        return true;
    }
}

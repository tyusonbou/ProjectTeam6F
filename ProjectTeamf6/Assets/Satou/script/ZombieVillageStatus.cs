using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVillageStatus : MonoBehaviour
{
    [Header("ゾンビ村の耐久力")]
    [SerializeField]
    float HP;
    //List<GameObject> zombiemura = new List<GameObject>();

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //zombiemura = GameObject.FindGameObjectWithTag("ZombieVillage");        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            //Debug.Log(" q");
            HP -= player.GetComponent<Player>().ReturnAttackP();
        }
    }
}

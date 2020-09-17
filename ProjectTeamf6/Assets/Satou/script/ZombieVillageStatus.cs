using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVillageStatus : MonoBehaviour
{
    [Header("ゾンビ村の耐久力")]
    [SerializeField]
    float HP;
    [Header("本拠地か村か")]
    [SerializeField]
    int EBType;

    GameObject[] zombiemura = new GameObject[2];
    SpriteRenderer renderer;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        zombiemura[0] = GameObject.Find("enemyBase (1)");
        zombiemura[1] = GameObject.Find("enemyBase (2)");
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        
        if(EBType == 0 && zombiemura[0] == null && zombiemura[1] == null)
        {
            renderer.color = new Color32(65, 63, 63, 255);
        }
        if (EBType == 0 && zombiemura[0] && zombiemura[1])
        {
            renderer.color = new Color32(65, 63, 63, 100);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Attack"))
        {
            //Debug.Log(" q");

            //本拠地は拠点が残っているとダメージを受けない
            if (EBType == 0 && zombiemura[0] == null && zombiemura[1] == null)
            {
                HP -= player.GetComponent<Player>().ReturnAttackP();
            }
            if(EBType == 1)
            {
                HP -= player.GetComponent<Player>().ReturnAttackP();
            }
        }
    }

    public float ReturnHP()
    {
        return HP;
    }
}

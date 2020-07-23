using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVillageSpawn : MonoBehaviour
{
    GameObject[] zombieVillage = new GameObject[5];
    List<GameObject> firstZombieVillage = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //スポーンポイント登録
        for (int i = 0; i < zombieVillage.Length; i++)
        {
            zombieVillage[i] = GameObject.Find("Point" + (i + 1));
        }
        int point1 = Random.Range(0, zombieVillage.Length);
        int point2 = Random.Range(0, zombieVillage.Length);

        if (point2 == point1)
        {
            point2 = Random.Range(0, zombieVillage.Length);
        }

        //最初からあるゾンビ村
        firstZombieVillage.Add(zombieVillage[point1]);
        firstZombieVillage.Add(zombieVillage[point2]);

        foreach (var n in firstZombieVillage)
        {
            //ゾンビ村の色変える
            n.GetComponentInChildren<Renderer>().material.color = Color.red;
            n.gameObject.tag = "ZombieVillage";
        }
    }

    // Update is called once per frame
    void Update()
    {


        foreach (var n in zombieVillage)
        {
            Debug.Log(n);
        }
    }
}

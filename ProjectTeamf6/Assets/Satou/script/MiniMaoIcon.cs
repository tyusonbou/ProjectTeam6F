using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BaseType
{
    MyVill,
    EnemyVill,
    NeutralVill,
}
public class MiniMaoIcon : MonoBehaviour
{
    [SerializeField]
    List<GameObject> iconVillage;
    GameObject myVillage, enemyVillage, neutralVillage;
    int iconBaseType;

    // Start is called before the first frame update
    void Start()
    {
        iconVillage = new List<GameObject>();
        //myVillage = new List<GameObject>();
        //enemyVillage = new List<GameObject>();
        //neutralVillage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < iconVillage.Count; i++)
        {
            var index = iconVillage.ToString().IndexOf("Neutral");
            iconVillage[i] = iconVillage[index];
        }
        for (int i = 0; i < iconVillage.Count; i++)
        {
            var index = iconVillage.ToString().IndexOf("Enemy");
            iconVillage[i] = iconVillage[index];
        }
        for (int i = 0; i < iconVillage.Count; i++)
        {
            var index = iconVillage.ToString().IndexOf("Friend");
            iconVillage[i] = iconVillage[index];
        }
        foreach (var i in iconVillage)
        {
            Debug.Log(i);
        }
        //if (iconVillage.ToString().Contains("Zombie"))
        //{

        //}
    }
}

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
    List<GameObject> iconVillage = new List<GameObject>();
    List<GameObject> myVillage, enemyVillage, neutralVillage;
    int iconBaseType;//0:my 1:enemy 2:neutral
    //Base bbase = new Base();

    // Start is called before the first frame update
    void Start()
    {
        //myVillage = new List<GameObject>();
        //enemyVillage = new List<GameObject>();
        //neutralVillage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<Renderer>().material.color == Color.gray)
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }

        if (GetComponentInParent<Renderer>().material.color == Color.red)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (GetComponentInParent<Renderer>().material.color == Color.blue)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        if (GetComponentInParent<Renderer>().material.color == Color.green)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        /*
        int foundN= iconVillage.ToString().IndexOf("Neutral");
        //while (0 <= foundN)
        //{
        //    neutralVillage.Add(iconVillage[foundN]);
        //    if (foundN + 1 < iconVillage.Count)
        //    {
        //        //次の要素を検索する
        //        foundN = iconVillage.ToString().IndexOf("Neutral",foundN+1); //Array.IndexOf(ary, searchWord, foundIndex + 1);
        //    }
        //    else
        //    {
        //        //最後まで検索したときはループを抜ける
        //        break;
        //    }
        //}
        //int foundM = iconVillage.ToString().IndexOf("Friend");
        //while (0 <= foundM)
        //{
        //    myVillage.Add(iconVillage[foundM]);
        //    if (foundM + 1 < iconVillage.Count)
        //    {
        //        //次の要素を検索する
        //        foundM = iconVillage.ToString().IndexOf("Friend", foundM + 1); //Array.IndexOf(ary, searchWord, foundIndex + 1);
        //    }
        //    else
        //    {
        //        //最後まで検索したときはループを抜ける
        //        break;
        //    }
        //}
        //int foundE = iconVillage.ToString().IndexOf("Zombie");
        //while (0 <= foundE)
        //{
        //    enemyVillage.Add(iconVillage[foundE]);
        //    if (foundE + 1 < iconVillage.Count)
        //    {
        //        //次の要素を検索する
        //        foundE = iconVillage.ToString().IndexOf("Zombie", foundE + 1); //Array.IndexOf(ary, searchWord, foundIndex + 1);
        //    }
        //    else
        //    {
        //        //最後まで検索したときはループを抜ける
        //        break;
        //    }
        //}
        ////for (int i = 0; i < iconVillage.Count; i++)
        ////{
        ////    var index = iconVillage.ToString().IndexOf("Neutral");
        ////    iconVillage[i] = iconVillage[index];
        ////}
        //foreach (var i in enemyVillage)
        //{
        //    Debug.Log(i);
        //}
        //BaseType basetype = BaseType.MyVill;
        //switch (basetype) {
        //    case BaseType.MyVill:
        //        myVillage[iconBaseType].gameObject.GetComponentInChildren<Renderer>().material.color = Color.blue;
        //        break;
        //    case BaseType.EnemyVill:
        //        enemyVillage[iconBaseType].gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        //        break;
        //    case BaseType.NeutralVill:
        //        neutralVillage[iconBaseType].gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
        //        break;
        //}*/

        //1-3でバフ返してなかったら中立それ以外ゾンビ

    }
}

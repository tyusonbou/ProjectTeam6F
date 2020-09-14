using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageIcon : MonoBehaviour
{
    Base Village; //村
    SpriteRenderer renderer;  //スプライト
    // Start is called before the first frame update
    void Start()
    {
        Village = GetComponentInParent<Base>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Village.ReturnBaseType() == 1 || Village.ReturnBaseType() == 2 || Village.ReturnBaseType() == 3)　//バフ村の時
        {
            if (Village.ReturnBaf() == true) 
            {
                renderer.color = Color.blue;  //プレイヤー側の時青
            }
            else
            {
                renderer.color = Color.gray;　//中立状態の時灰色
            }
        }
        else if(Village.ReturnBaseType() == 4)
        {
            renderer.color = Color.red;  //ゾンビ村の時赤
        }
    }
}

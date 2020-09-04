using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHPUI : MonoBehaviour
{
    [SerializeField]
    GameObject BaseHP; //ゲームオブジェクト
    [SerializeField]
    Slider BHPSlider; //スライダー
    [SerializeField]
    Image Guage;　//ゲージ色
    [SerializeField]
    GameObject YButton;　//ボタン

    Base PlayerBase;

    // Start is called before the first frame update
    void Start()
    {
        //BaseHP = GetComponentInChildren<GameObject>();
        PlayerBase = GetComponentInParent<Base>();
        BHPSlider = BaseHP.GetComponent<Slider>();
        Guage = GetComponentInChildren<Image>();
        //Button = GameObject.Find("Button");

        YButton.SetActive(false);      

        BHPSlider.maxValue = PlayerBase.ReturnHP();
    }

    // Update is called once per frame
    void Update()
    {
        BHPSlider.value = PlayerBase.ReturnHP();

        if (PlayerBase.ReturnBaseType() == 1)　//攻撃アップ赤
        {
            Guage.color = new Color32(255, 0, 0, 150);
        }
        if (PlayerBase.ReturnBaseType() == 2)　//速さアップ青
        {
            Guage.color = new Color32(0, 0, 255, 150);
        }
        if (PlayerBase.ReturnBaseType() == 3)  //HP回復緑
        {
            Guage.color = new Color32(0, 255, 0, 150);
        }
        if (PlayerBase.ReturnBaseType() == 4)  //ゾンビ村灰
        {
            Guage.color = new Color32(53, 53, 53, 150);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") 　//プレイヤー近づいたらボタン表示
        {
            if(PlayerBase.ReturnBaseType() == 0　|| PlayerBase.ReturnBaseType() == 4) //プレイヤー拠点またはゾンビ村はボタン非表示
            {
                return;
            }

            //if (PlayerBase.ReturnBaf() == false)　//バフを受けてないとき表示
            //{
            //    YButton.SetActive(true);
            //}

            //if (PlayerBase.ReturnBaf() == true)　//バフを受けてるとき非表示
            //{
            //    YButton.SetActive(false);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            YButton.SetActive(false);
        }
    }
}

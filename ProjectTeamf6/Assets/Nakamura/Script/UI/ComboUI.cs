using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    Base ATKBase1;//攻撃村１
    Base ATKBase2;//攻撃村２

    [SerializeField]
    Image[] Buttons; //ボタン画像
    [SerializeField]
    Image[] Bafs;  //バフ画像
    [SerializeField]
    Text ChargeText;

    Player player;

    Slider MPSlider;
    Image Guage;

    // Start is called before the first frame update
    void Start()
    {
        ATKBase1 = GameObject.Find("playerVillage2").GetComponent<Base>();
        ATKBase2 = GameObject.Find("village2").GetComponent<Base>();

        //for (int i = 0; i < 5; i++)
        //{
        //    Buttons[i].color = Color.gray;
        //    Bafs[i].color = Color.gray;
        //}

        player = GameObject.Find("Player").GetComponent<Player>();
        
        MPSlider = GameObject.Find("MPGuageP").GetComponent<Slider>();
        Guage = MPSlider.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ATKBase1.ReturnBaf() == true || ATKBase2.ReturnBaf() == true) //攻撃アップが1つ以上の時
        {
            Buttons[0].color = Color.white;
            //Buttons[2].color = Color.white;
            Bafs[0].color = Color.clear;
            Bafs[2].color = Color.clear;
            Bafs[4].color = Color.clear;
            ChargeText.color = new Color32(238, 244, 60, 255);
        }
        else
        {
            Buttons[0].color = Color.gray;
            //Buttons[2].color = Color.gray;
            Bafs[0].color = Color.gray;
            Bafs[2].color = Color.gray;
            Bafs[4].color = Color.gray;
            ChargeText.color = Color.gray;
        }

        if (ATKBase1.ReturnBaf() == true && ATKBase2.ReturnBaf() == true)//攻撃アップが2つ以上の時
        {
            Buttons[1].color = Color.white;
            Bafs[1].color = Color.clear;
            Bafs[3].color = Color.clear;
        }
        else
        {
            Buttons[1].color = Color.gray;
            Bafs[1].color = Color.gray;
            Bafs[3].color = Color.gray;
        }

        MPSlider.value = player.ChargeTimer;
        MPSlider.maxValue = player.LimitChargeTimerDef;

        if (MPSlider.value < MPSlider.maxValue / 4)
        {
            Guage.color = Color.gray;
        }
        else if (MPSlider.value < MPSlider.maxValue / 2)
        {
            Guage.color = Color.blue;
        }
        else if (MPSlider.value < MPSlider.maxValue * 3 / 4)
        {
            Guage.color = Color.green;
        }
        else if (MPSlider.value < MPSlider.maxValue)
        {
            Guage.color = Color.yellow;
        }
        else if (MPSlider.value >= MPSlider.maxValue)
        {
            Guage.color = Color.red;
        }
    }
}

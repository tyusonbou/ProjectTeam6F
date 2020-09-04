using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    Text HPText;  //プレイヤーHPテキスト
    [SerializeField]
    Text MPText;  //プレイヤーMPテキスト
    [SerializeField]
    Text ATKText; //プレイヤーATKテキスト
    [SerializeField]
    Text SPDText; //プレイヤーSPDテキスト

    Slider HPSlider; //プレイヤーHPゲージ
    Slider MPSlider; //プレイヤーMPゲージ
    



    [SerializeField]
    GameObject[] UPImage;  //バフ表示

    Player player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
       

        HPSlider = GameObject.Find("HPGuage").GetComponent<Slider>();
        MPSlider = GameObject.Find("MPGuage").GetComponent<Slider>();
        
        for(int i = 0; i < 4; i++)
        {
            UPImage[i] = GameObject.Find("Bafu" + i);
        }

        for (int i=0; i < 4; i++)
        {
            UPImage[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = "HP:" + (int)player.ReturnPlayerHP() + "/" + (int)player.ReturnPlayerMaxHP();
        MPText.text = "MP:" + (int)player.ReturnPlayerMP() + "/" + (int)player.ReturnPlayerMaxMP();
        ATKText.text = "ATK:" + (int)player.ReturnAttackP();
        SPDText.text = "SPD:" + (int)player.ReturnSpeed();

        if (player.ReturnAttackP() > 50)
        {
            UPImage[0].SetActive(true); //一定以上で攻撃表示
        }
        else
        {
            UPImage[0].SetActive(false);
        }
        if (player.ReturnAttackP() > 70)
        {
            UPImage[1].SetActive(true);
        }
        else
        {
            UPImage[1].SetActive(false);
        }

        if (player.ReturnSpeed() > 10) //一定以上で速さ表示
        {
            UPImage[2].SetActive(true);
        }
        else
        {
            UPImage[2].SetActive(false);
        }
        

        HPSlider.value = player.ReturnPlayerHP();
        MPSlider.value = player.ReturnPlayerMP();
        HPSlider.maxValue = player.ReturnPlayerMaxHP();
        MPSlider.maxValue = player.ReturnPlayerMaxMP();
    }
}

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
    Image[] UPImage;  //バフ表示

    Player player;//プレイヤー
    Base SPDBase;//速さ村
    Base HPBase;//HP回復村

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        SPDBase = GameObject.Find("playerVillage1").GetComponent<Base>();
        HPBase = GameObject.Find("village1").GetComponent<Base>();

        HPSlider = GameObject.Find("HPGuage").GetComponent<Slider>();
        MPSlider = GameObject.Find("MPGuage").GetComponent<Slider>();
        
        for(int i = 0; i < 4; i++)
        {
            UPImage[i] = GameObject.Find("Bafu" + i).GetComponent<Image>();
        }

        for (int i=0; i < 4; i++)
        {
            UPImage[i].color = Color.white;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = (int)player.ReturnPlayerHP() + "/" + (int)player.ReturnPlayerMaxHP();
        MPText.text = (int)player.ReturnPlayerMP() + "/" + (int)player.ReturnPlayerMaxMP();
        ATKText.text = "ATK:" + (int)player.ReturnAttackP();
        SPDText.text = "SPD:" + (int)player.ReturnSpeed();

        if (player.ReturnAttackP() > 50)
        {
            UPImage[0].color = Color.white; //一定以上で攻撃表示
        }
        else
        {
            UPImage[0].color = new Color32(50, 50, 50, 200);
        }
        if (player.ReturnAttackP() > 70)
        {
            UPImage[1].color = Color.white;
        }
        else
        {
            UPImage[1].color = new Color32(50, 50, 50, 200);
        }

        if (SPDBase.ReturnBaf() == true) //速さUP表示
        {
            UPImage[2].color = Color.white;
        }
        else
        {
            UPImage[2].color = new Color32(50, 50, 50, 200);
        }

        if (HPBase.ReturnBaf() == true) //HP回復バフを表示
        {
            UPImage[3].color = Color.white;
        }
        else
        {
            UPImage[3].color = new Color32(50, 50, 50, 200);
        }


        HPSlider.value = player.ReturnPlayerHP();
        MPSlider.value = player.ReturnPlayerMP();
        HPSlider.maxValue = player.ReturnPlayerMaxHP();
        MPSlider.maxValue = player.ReturnPlayerMaxMP();
    }
}

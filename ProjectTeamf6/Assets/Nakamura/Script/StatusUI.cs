using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    Text HPText;
    [SerializeField]
    Text MPText;
    [SerializeField]
    Text ATKText;
    [SerializeField]
    Text SPDText;

    [SerializeField]
    Slider HPSlider;
    [SerializeField]
    Slider MPSlider;
    [SerializeField]
    Slider PbSlider;
    [SerializeField]
    Slider EbSlider;

    [SerializeField]
    GameObject[] ATKUPImage;
    [SerializeField]
    GameObject[] SPDUPImage;

    Player player;
    Base PlayerBase;
    ZombieVillageStatus EnemyBase;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        PlayerBase = GameObject.Find("playerBase").GetComponent<Base>();
        EnemyBase = GameObject.Find("enemyBase").GetComponent<ZombieVillageStatus>();

        HPSlider = GameObject.Find("HPGuage").GetComponent<Slider>();
        MPSlider = GameObject.Find("MPGuage").GetComponent<Slider>();
        //PbSlider = GameObject.Find("PbGuage").GetComponent<Slider>();
        //EbSlider = GameObject.Find("EbGuage").GetComponent<Slider>();

        for (int i=0; i < 2; i++)
        {
            ATKUPImage[i].SetActive(false);
        }
        for (int i = 0; i < 2; i++)
        {
            SPDUPImage[i].SetActive(false);
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
            ATKUPImage[0].SetActive(true);
        }
        if (player.ReturnAttackP() > 70)
        {
            ATKUPImage[1].SetActive(true);
        }

        if (player.ReturnSpeed() > 10)
        {
            SPDUPImage[0].SetActive(true);
        }
        if (player.ReturnSpeed() > 18)
        {
            SPDUPImage[1].SetActive(true);
        }

        HPSlider.value = player.ReturnPlayerHP();
        MPSlider.value = player.ReturnPlayerMP();

        HPSlider.maxValue = player.ReturnPlayerMaxHP();
        MPSlider.maxValue = player.ReturnPlayerMaxMP();

        //PbSlider.value=
        //EbSlider.value=

        //PbSlider.maxValue=
        //EbSlider.maxValue=
    }
}

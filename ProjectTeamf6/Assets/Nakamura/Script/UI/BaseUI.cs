using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [SerializeField]
    Slider PbSlider; //プレイヤー本拠地ゲージ
    [SerializeField]
    Slider EbSlider; //エネミー本拠地ゲージ

    [SerializeField]
    Base PlayerBase;
    [SerializeField]
    ZombieVillageStatus EnemyBase;

    [SerializeField]
    Slider[] ViSlider; //村ゲージ
    
    [SerializeField]
    Base[] Villeges;
    [SerializeField]
    ZombieVillageStatus[] Spawners;

    [SerializeField]
    Image[] Guages;
    [SerializeField]
    int[] BaseType;//拠点確認用

    // Start is called before the first frame update
    void Start()
    {
        PbSlider = GameObject.Find("PbGuage").GetComponent<Slider>();
        EbSlider = GameObject.Find("EbGuage").GetComponent<Slider>();

        PlayerBase = GameObject.Find("playerBase").GetComponent<Base>();
        EnemyBase = GameObject.Find("enemyBase").GetComponent<ZombieVillageStatus>();

        for (int i = 0; i < 6; i++)
        {
            ViSlider[i] = GameObject.Find("ViGuage" + i).GetComponent<Slider>();
            Guages[i] = GameObject.Find("ViGuage" + i).GetComponentInChildren<Image>();
        }

        Guages[6] = GameObject.Find("EbGuage").GetComponentInChildren<Image>();

        Villeges[0] = GameObject.Find("playerVillage1").GetComponent<Base>();
        Villeges[1] = GameObject.Find("playerVillage2").GetComponent<Base>();
        Villeges[2] = GameObject.Find("village1").GetComponent<Base>();
        Villeges[3] = GameObject.Find("village2").GetComponent<Base>();

        Spawners[0] = GameObject.Find("enemyBase (1)").GetComponent<ZombieVillageStatus>();
        Spawners[1] = GameObject.Find("enemyBase (2)").GetComponent<ZombieVillageStatus>();

        PbSlider.maxValue = PlayerBase.ReturnHP();
        EbSlider.maxValue = EnemyBase.ReturnHP();

        for (int i = 0; i < 4; i++)
        {
            ViSlider[i].maxValue = Villeges[i].ReturnHP();
        }
        ViSlider[4].maxValue = Spawners[0].ReturnHP();
        ViSlider[5].maxValue = Spawners[1].ReturnHP();
    }

    // Update is called once per frame
    void Update()
    {
        PbSlider.value = PlayerBase.ReturnHP();
        EbSlider.value = EnemyBase.ReturnHP();

        for(int i = 0; i < 4; i++)
        {
            ViSlider[i].value = Villeges[i].ReturnHP();
            BaseType[i] = Villeges[i].ReturnBaseType();

            if (BaseType[i] == 1)
            {
                Guages[i].color = new Color32(255, 0, 0, 255);
            }
            if (BaseType[i] == 2)
            {
                Guages[i].color = new Color32(0, 0, 255, 255);
            }
            if (BaseType[i] == 3)
            {
                Guages[i].color = new Color32(0, 255, 0, 255);
            }
            if (BaseType[i] == 4)
            {
                Guages[i].color = new Color32(150, 150, 150, 255);
            }
        }
        ViSlider[4].value = Spawners[0].ReturnHP();
        ViSlider[5].value = Spawners[1].ReturnHP();

        if(Spawners[0] == null)
        {
            Destroy(ViSlider[4]);
        }
        if (Spawners[1] == null)
        {
            Destroy(ViSlider[5]);
        }

        if(Spawners[0] == null && Spawners[1] == null)
        {
            Guages[6].color = new Color32(150, 150, 150, 255);
        }
    }
}

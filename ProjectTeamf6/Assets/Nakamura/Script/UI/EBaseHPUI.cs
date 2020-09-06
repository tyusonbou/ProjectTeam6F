using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBaseHPUI : MonoBehaviour
{
    [SerializeField]
    GameObject BaseHP; //ゲームオブジェクト
    [SerializeField]
    Slider BHPSlider; //スライダー
    
    ZombieVillageStatus EnemyBase;

    // Start is called before the first frame update
    void Start()
    {
       
        EnemyBase = GetComponentInParent<ZombieVillageStatus>();
        BHPSlider = BaseHP.GetComponent<Slider>();

        BHPSlider.maxValue = EnemyBase.ReturnHP();
    }

    // Update is called once per frame
    void Update()
    {
        BHPSlider.value = EnemyBase.ReturnHP();

        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        
    }
}

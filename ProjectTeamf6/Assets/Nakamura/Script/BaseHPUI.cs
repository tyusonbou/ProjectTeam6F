using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHPUI : MonoBehaviour
{
    [SerializeField]
    GameObject BaseHP;
    [SerializeField]
    Slider BHPSlider;
    Image Guage;
    Image Huti;

    Base PlayerBase;

    // Start is called before the first frame update
    void Start()
    {
        BaseHP = GameObject.Find("BaseHPGuage");
        PlayerBase = GameObject.Find("playerBase").GetComponent<Base>();
        BHPSlider = BaseHP.GetComponent<Slider>();
        Guage = GetComponentInChildren<Image>();
        Huti = GameObject.Find("FillB").GetComponent<Image>();

        BaseHP.SetActive(false);
        //Guage.color = Color.clear;
        //Huti.color = Color.clear;

        BHPSlider.maxValue = PlayerBase.ReturnHP();
    }

    // Update is called once per frame
    void Update()
    {
        BHPSlider.value = PlayerBase.ReturnHP();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject)
        {
            BaseHP.SetActive(true);
            //Guage.color = new Color(0, 0, 255, 100);
            //Huti.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject)
        {
            BaseHP.SetActive(false);
            //Guage.color = Color.clear;
            //Huti.color = Color.clear;
        }
    }
}

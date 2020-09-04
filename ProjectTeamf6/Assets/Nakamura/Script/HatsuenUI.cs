using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatsuenUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] ScreamImage;
    [SerializeField]
    Slider HaSlider;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        HaSlider = GameObject.Find("HatuenSlider").GetComponent<Slider>();
       

        for (int i = 0; i < 3; i++)
        {
            ScreamImage[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ReturnScremCount() == 2)
        {
            ScreamImage[2].SetActive(false);
        }
        if (player.ReturnScremCount() == 1)
        {
            ScreamImage[1].SetActive(false);
        }
        if (player.ReturnScremCount() == 0)
        {
            ScreamImage[0].SetActive(false);
        }

        HaSlider.value = player.STimer;
        HaSlider.maxValue = player.SLimitTimer;
    }
}

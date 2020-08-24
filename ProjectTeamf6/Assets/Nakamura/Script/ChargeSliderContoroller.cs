using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSliderContoroller : MonoBehaviour
{
    Player player;
    GameObject playerPos;

    [SerializeField]
    Slider MPSlider;
    Image Guage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerPos = GameObject.Find("Player");
        MPSlider = GameObject.Find("MPGuageP").GetComponent<Slider>();
        Guage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y + 1f, playerPos.transform.position.z);

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

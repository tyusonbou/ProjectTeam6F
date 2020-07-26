using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    Text HPText;
    [SerializeField]
    Text ATKText;
    [SerializeField]
    Text SPDText;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = "HP:" + player.ReturnPlayerHP() + "/" + player.ReturnPlayerMaxHP();
        ATKText.text = "ATK:" + player.ReturnAttackP();
        SPDText.text = "SPD:" + player.ReturnSpeed();
    }
}

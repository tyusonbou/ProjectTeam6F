using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //　トータル制限時間
    [SerializeField]
    private float totalTime;
    [SerializeField]
    private int minute;
    [SerializeField]
    private float seconds;
    //　前のUpdateの時の秒数
    private float oldSeconds;

    [SerializeField]
    private Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = minute * 60 + seconds;
        timerText = GetComponentInChildren<Text>();
        oldSeconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }
        if (CountDown.isCountDown) { return; }
        //　制限時間が0秒以下なら何もしない
        //if (totalTime <= 0f)
        //{
        //    return;
        //}

        //　一旦トータルの制限時間を計測；
        totalTime = minute * 60 + seconds;
        totalTime += Time.deltaTime;

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //　値が変わった時だけテキストUIを更新
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;

        ////　制限時間以下になったら死亡
        //if (totalTime <= 0f)
        //{
        //    Destroy(GameObject.Find("Player"));
        //}
    }
}

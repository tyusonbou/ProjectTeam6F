using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    
    //カウントダウンテキスト
    [SerializeField]
    Text countDownText;

    public static bool isCountDown = true;
    
    float countDown = 4;
    int count;

    public float a;
    // Start is called before the first frame update
    void Start()
    {
        isCountDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        countDown -= Time.deltaTime;
        count = (int)countDown;

        a = Time.timeScale;

        if (countDown >= 1)
        {
            countDownText.text = count.ToString();

            Time.timeScale = 0;
        }
        else if (countDown >= 0)
        {
            countDownText.text = "START";
        }
        if (countDown < 0)
        {
            countDownText.text = "";
            Time.timeScale = 1f;
            isCountDown = false;
            gameObject.SetActive(false);
        }
    }
}

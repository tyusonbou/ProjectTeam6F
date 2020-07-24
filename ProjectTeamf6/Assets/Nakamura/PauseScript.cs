﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    // ポーズメニュー用
    static int number;

    static bool menu;
    // コントローラー連打対策用
    int cnt;
    //テキスト格納
    public GameObject retry;
    public GameObject exit;

    //ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;

    void Start()
    {
        pauseUI.SetActive(false);
        cnt = 0;
        number = 0;
        menu = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu)
            {
                menu = true;
                Time.timeScale = 0f;
            }

        }
        // menuがfalseなら非表示
        if (!menu)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
        cnt++;
        if (cnt > 15)
            cnt = 15;
        Debug.Log(number);
        GameOverSelect();

        RedText();

    }

    void GameOverSelect()
    {
        //menuがtrueなら
        if (menu)
        {
            //表示
            pauseUI.SetActive(true);
            //input
            InPut();
            // リトライ
            if (number == 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    menu = false;
                    Time.timeScale = 1f;
                }
            }

            // 終了
            if (number == 1)
            {

                // アプリケーション終了
                if (Input.GetKey(KeyCode.Space))
                {
                    Quit();
                }
            }
        }
    }

    void InPut()
    {
        // メニュ選択(Input)下
        if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < 0 && cnt >= 15)
        {
            number++;
            cnt = 0;
        }
        // 上
        if (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0 && cnt >= 15)
        {
            number--;
            cnt = 0;
        }

        // 折り返し
        if (number == -1)
            number = 1;

        if (number == 2)
            number = 0;

    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    void RedText()
    {
        // 番号と同じ時赤くする
        if (number == 0)
        {
            // 赤
            retry.GetComponent<Text>().color = new Color(1, 0, 0, 1);
        }
        else retry.GetComponent<Text>().color = new Color(255, 255, 255, 1);

        if (number == 1)
        {
            // 赤
            exit.GetComponent<Text>().color = new Color(1, 0, 0, 1);
        }
        else exit.GetComponent<Text>().color = new Color(255, 255, 255, 1);
    }

}

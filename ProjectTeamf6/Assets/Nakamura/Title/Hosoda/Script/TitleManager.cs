using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ポーズメニュー用
    static int number;

    // コントローラー連打対策用
    int cnt;
    //テキスト格納
    public GameObject start;
    public GameObject exit;

    public string GameScene;

    void Start()
    {
        cnt = 0;
        number = 0;
    }
    // Update is called once per frame
    void Update()
    {
        cnt++;
        if (cnt > 15)
            cnt = 15;
        Debug.Log(number);
        Select();

        RedText();

    }

    void Select()
    {

        //input
        InPut();
        // ゲーム開始
        if (number == 0)
        {
            if (Input.GetButtonDown("START") || Input.GetButtonDown("A"))
            {
                SceneManager.LoadScene(GameScene);
            }
        }

        // 終了
        if (number == 1)
        {

            // アプリケーション終了
            if (Input.GetButtonDown("START") || Input.GetButtonDown("A"))
            {
                Quit();
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
            start.GetComponent<Text>().color = new Color(1, 0, 0, 1);
        }
        else start.GetComponent<Text>().color = new Color(0, 0, 0, 1);

        if (number == 1)
        {
            // 赤
            exit.GetComponent<Text>().color = new Color(1, 0, 0, 1);
        }
        else exit.GetComponent<Text>().color = new Color(0, 0, 0, 1);
    }
}

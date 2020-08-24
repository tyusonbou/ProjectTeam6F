using System.Collections;
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
    public GameObject clear;

    //ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;

    void Start()
    {
        pauseUI.SetActive(false);
        cnt = 0;
        number = 0;
        menu = false;
        clear.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("START")) 
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
        //Debug.Log(number);
        GameOverSelect();

        RedText();

        if (GameObject.Find("Player") == null || GameObject.Find("enemyBase") == null || GameObject.Find("playerBase") == null)
        {
            retry.GetComponent<Text>().text = "リトライ";
            if (!menu)
            {
                menu = true;
                Time.timeScale = 0f;
            }
        }

        if(GameObject.Find("enemyBase") == null)
        {
            clear.SetActive(true);
        }
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
                if (Input.GetButtonDown("A")) 
                {
                    if (GameObject.Find("Player") == null || GameObject.Find("enemyBase") == null || GameObject.Find("playerBase") == null)
                    {
                        // 現在のScene名を取得する
                        Scene loadScene = SceneManager.GetActiveScene();
                        // Sceneの読み直し
                        SceneManager.LoadScene(loadScene.name);
                    }
                    else
                    {
                        menu = false;
                        Time.timeScale = 1f;
                    }
                    
                }
            }

            // 終了
            if (number == 1)
            {

                // アプリケーション終了
                if (Input.GetButtonDown("A"))
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

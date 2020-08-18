using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    BoxCollider2D bc;
    Status status;
    GameObject Player;
    public bool Touch;//触れているか
    public bool ON;//一回だけ
    [SerializeField]
    private int TerrainType;//地形の種類
    [SerializeField]
    private float RecoveryPoint;//回復量
    [SerializeField]
    private float RecoveryUpPoint;//インスペクタで変更できる値
    [SerializeField]
    private float SpeedDownPoint;//インスペクタで変更できる値
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        status = Player.GetComponent<Status>();
        Touch = false;
        ON = true;
        bc = this.gameObject.GetComponent<BoxCollider2D>();
        switch (TerrainType)
        {
            //何もなし
            case 0:
                bc.isTrigger = true;
                break;
            //砂地 HPが徐々に減る
            case 1:
                bc.isTrigger = true;
                break;
            //岩 何回か攻撃を当てると壊れる
            case 2:
                bc.isTrigger = false;
                break;
            //木 壊せない
            case 3:
                bc.isTrigger = false;
                break;
            //沼 移動速度が遅くなる
            case 4:
                bc.isTrigger = true;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Touch == true)
        {
            switch (TerrainType)
            {
                //何もなし
                case 0:
                    break;
                //砂地 HPが徐々に減る
                case 1:
                    if (ON == true)
                    {
                        StartCoroutine("RecoveryDown");
                        ON = false;
                    }
                        break;
                //岩 何回か攻撃を当てると壊れる
                case 2:
                   break;
                //木 壊せない
                case 3:
                    bc.isTrigger = false;
                    break;
                //沼 移動速度が遅くなる
                case 4:
                    if (ON == true)
                    {
                        StartCoroutine("SpeedDown");
                        ON = false;
                    }
                    break;
            }
        }
        if (Touch == false)
        {
            switch (TerrainType)
            {
                //何もなし
                case 0:

                    break;
                //砂地 HPが徐々に減る
                case 1:
                    if (ON == false)
                    {
                        StopCoroutine("RecoveryDown");
                        ON = true;
                    }
                    break;
                //岩 何回か攻撃を当てると壊れる
                case 2:

                    break;
                //木 壊せない
                case 3:

                    break;
                //沼 移動速度が遅くなる
                case 4:
                    if (ON == false)
                    {
                        StartCoroutine("SpeedReset");
                        ON = true;
                    }
                    break;
            }
        }
    }
    IEnumerator RecoveryDown()
    {
        while (true)
        {
            RecoveryPoint = status.MaxHP * (RecoveryUpPoint - 1.0f);
            status.HP = status.HP - RecoveryPoint;
            yield return new WaitForSeconds(1.0f);
        }
    }
    IEnumerator SpeedDown()
    {
        //速度の値を計算
        status.Speed = status.Speed - SpeedDownPoint;
        yield return null;
    }
    IEnumerator SpeedReset()
    {
        //速度の値を計算
        status.Speed = status.Speed + SpeedDownPoint;
        yield return null;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player"||
            col.gameObject.name == "player_Enemy" || col.gameObject.name == "player_Enemy(Clone)"||
            col.gameObject.name == "pv_Enemy" || col.gameObject.name == "pv_Enemy(Clone)")
        {
            Touch = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" ||
            col.gameObject.name == "player_Enemy" || col.gameObject.name == "player_Enemy(Clone)" ||
            col.gameObject.name == "pv_Enemy" || col.gameObject.name == "pv_Enemy(Clone)")
        {
            Touch = false;
        }
    }
}
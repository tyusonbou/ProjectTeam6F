using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Base : MonoBehaviour
{
    Status status;
    GameObject Player;
    GameObject pb_enemy;
    GameObject pl_enemy;
    GameObject enemy;
    Pl_EnemyMove plen;
    [SerializeField]
    private float HP;//拠点の体力
    [SerializeField]
    private float AttackPoint;//攻撃上昇値
    [SerializeField]
    private float SpeedPoint;//速度上昇値
    [SerializeField]
    private float RecoveryPoint;//回復量
    [SerializeField]
    private float AttackUpPoint;//インスペクタで変更できる値
    [SerializeField]
    private float SpeedUpPoint;//インスペクタで変更できる値
    [SerializeField]
    private float RecoveryUpPoint;//インスペクタで変更できる値
    [SerializeField]
    private int BaseType;//拠点の種類
    public bool ON;//一回だけ
    public bool Touch;//触れているか
    public bool Baf;//バフを発生しているかどうか
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        
        status = Player.GetComponent<Status>();
        ON = true;
        Touch = false;
        Baf = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.Find("enemy");
        pb_enemy = GameObject.Find("pb_Enemy");
        pl_enemy = GameObject.Find("player_Enemy");
        if (Touch == true)
        {
            if (Input.GetButtonDown("A"))
            {
                switch (BaseType)
                {
                    //拠点の場合
                    case 0:

                        break;
                    //村（攻撃力バフ）の場合
                    case 1:
                        if (ON == true && Baf == false)
                        {
                            StartCoroutine("AttackUp");
                            ON = false;
                        }
                        break;
                    //村（速度バフ）の場合
                    case 2:
                        if (ON == true && Baf == false)
                        {
                            StartCoroutine("SpeedUp");
                            ON = false;
                        }
                        break;
                    //村（回復力バフ）の場合
                    case 3:
                        if (ON == true && Baf == false)
                        {
                            StartCoroutine("RecoveryUp");
                            ON = false;
                        }
                        break;
                }
            }
        }
        if(HP <= 0)
        {
            HP = 0;
            if(Baf == false)
            {
                BaseType = 4;
            }
            switch (BaseType)
            {
                //拠点の場合
                case 0:

                    break;
                //村（攻撃力バフ）の場合
                case 1:
                    if (ON == false && Baf == true)
                    {
                        StartCoroutine("AttackDown");
                        ON = true;
                    }
                        break;
                //村（速度バフ）の場合
                case 2:
                    if (ON == false && Baf == true)
                    {
                        StartCoroutine("SpeedDown");
                        ON = true;
                    }
                    break;
                //村（回復力バフ）の場合
                case 3:
                    if (ON == false && Baf == true)
                    {
                        Baf = false;
                        StopCoroutine("RecoveryUp");
                        ON = true;
                    }
                        break;
            }
        }
        switch(BaseType)
        {
            case 4:
                //ゾンビ村スクリプト起動
                GetComponent<Renderer>().material.color = Color.grey;
                //Reset();
                break;
        }
    }

    //攻撃力バフ
    IEnumerator AttackUp()
    {
        Baf = true;
        GetComponent<Renderer>().material.color = Color.red;
        //攻撃力の増加する値を計算
        AttackPoint = status.Attack * (AttackUpPoint - 1.0f);
        status.Attack = status.Attack + AttackPoint;
        yield return null;
    }
    //攻撃力ダウン
    IEnumerator AttackDown()
    {
        Baf = false;
        GetComponent<Renderer>().material.color = Color.white;
        //攻撃力の増加する値を計算
        status.Attack = status.Attack - AttackPoint;
        yield return null;
    }

    //速度バフ
    IEnumerator SpeedUp()
    {
        Baf = true;
        GetComponent<Renderer>().material.color = Color.blue;
        //速度の増加する値を計算
        SpeedPoint = status.Speed * (SpeedUpPoint - 1.0f);
        status.Speed = status.Speed + SpeedPoint;
        yield return null;
    }
    //速度ダウン
    IEnumerator SpeedDown()
    {
        Baf = false;
        GetComponent<Renderer>().material.color = Color.clear;
        //速度の増加する値を計算
        status.Speed = status.Speed - SpeedPoint;
        yield return null;
    }

    //全回復
    IEnumerator RecoveryUp()
    {
        Baf = true;
        GetComponent<Renderer>().material.color = Color.green;
        //ステータスの最大HPを回復
        status.HP = status.HP + status.MaxHP;
        while(true)
        {
            RecoveryPoint = status.MaxHP * (RecoveryUpPoint - 1.0f);
            status.HP = status.HP + RecoveryPoint;
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    void Reset()
    {
        HP += 100;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "enemy")
        {
            
        }
        else if(col.gameObject.name == "pb_Enemy")
        {
            
        }
        else if(col.gameObject.name == "player_Enemy")
        {
            plen = col.gameObject.GetComponent<Pl_EnemyMove>();
            //HP = HP - plen.damege;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Touch = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Touch = false;
        }
    }

#if UNITY_EDITOR
    /**
     * Inspector拡張クラス
     */
    [CustomEditor(typeof(Base))]               //!< 拡張するときのお決まりとして書いてね
    public class BaseEditor : Editor           //!< Editorを継承するよ！
    {
        bool folding = false;

        public override void OnInspectorGUI()
        {
            //値の変更をする
            Undo.RecordObject(target, "ba");
            // target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
            Base ba = target as Base;

            //拠点の種類
            ba.BaseType = EditorGUILayout.IntField("拠点の種類", ba.BaseType);

            // -- 拠点の体力 --
            ba.HP = EditorGUILayout.FloatField("拠点の耐久値", ba.HP);

            // -- 攻撃 --
            ba.AttackUpPoint = EditorGUILayout.FloatField("攻撃上昇倍率", ba.AttackUpPoint);

            // -- 速度 --
            ba.SpeedUpPoint = EditorGUILayout.FloatField("速度上昇倍率", ba.SpeedUpPoint);

            // -- 回復量 --
            ba.RecoveryUpPoint = EditorGUILayout.FloatField("回復量", ba.RecoveryUpPoint);

            //値の変更を保存
            EditorUtility.SetDirty(ba);
        }
    }
#endif
}

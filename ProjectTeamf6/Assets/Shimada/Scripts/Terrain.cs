using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Terrain : MonoBehaviour
{
    TilemapCollider2D bc;
    Status status;
    GameObject Player;
    //pv_EnemyMove pv_enemy;
    //Pl_EnemyMove pl_enemy;
    public bool PlayerTouch;//プレイヤーが触れているか
    public bool pv_EnemyTouch;//pv_Enemyが触れているか
    public bool player_EnemyTouch;//player_Enemyが触れているか
    public bool Touch;//何かのオブジェクトが触れているか
    public bool ON;//一回だけ
    public Sprite Sand;//砂地
    public Sprite Rock;//岩
    public Sprite Wood;//木
    public Sprite Swamp;//沼
    private bool CoroutineON;
    //SpriteRenderer MainSprite;
    [SerializeField]
    private float HP;//岩の耐久値
    [SerializeField]
    private int TerrainType;//地形の種類
    private float RecoveryPoint;//回復量
    [SerializeField]
    private float RecoveryDownPoint;//インスペクタで変更できる値
    [SerializeField]
    private float SpeedDownPoint;//インスペクタで変更できる値
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        status = Player.GetComponent<Status>();
        Touch = false;
        PlayerTouch = false;
        pv_EnemyTouch = false;
        player_EnemyTouch = false;
        ON = true;
        CoroutineON = false;
        bc = this.gameObject.GetComponent<TilemapCollider2D>();
        //MainSprite = gameObject.GetComponent<SpriteRenderer>();

        switch (TerrainType)
        {
            //何もなし
            case 0:
                bc.isTrigger = true;
                break;
            //砂地 HPが徐々に減る
            case 1:
                bc.isTrigger = true;
                //MainSprite.sprite = Sand;
                break;
            //岩 何回か攻撃を当てると壊れる
            case 2:
                bc.isTrigger = false;
                //MainSprite.sprite = Rock;
                HP = 100;
                break;
            //木 壊せない
            case 3:
                bc.isTrigger = false;
                //MainSprite.sprite = Wood;
                break;
            //沼 移動速度が遅くなる
            case 4:
                bc.isTrigger = true;
                //MainSprite.sprite = Swamp;
                //MainSprite.color = new Color(10,0,10);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            if(TerrainType == 2)
            {
                Destroy(this.gameObject);
            }
        }
        if (Touch == true)
        {
            switch (TerrainType)
            {
                //砂地 HPが徐々に減る
                case 1:
                    if (ON == true)
                    {
                        StartCoroutine("RecoveryDown");
                        CoroutineON = true;
                        ON = false;
                    }
                        break;
                //沼 移動速度が遅くなる
                case 4:
                    if (ON == true && CoroutineON == false)
                    {
                        StartCoroutine("SpeedDown");
                        CoroutineON = true;
                        ON = false;
                    }
                    break;
            }
        }
        if (Touch == false)
        {
            switch (TerrainType)
            {
               //砂地 HPが徐々に減る
                case 1:
                    if (ON == false)
                    {
                        StopCoroutine("RecoveryDown");
                        CoroutineON = false;
                        ON = true;
                    }
                    break;
                //沼 移動速度が遅くなる
                case 4:
                    if (ON == false)
                    {
                        StartCoroutine("SpeedReset");
                        CoroutineON = false;
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
            //プレイヤーが触れていたら
            if (PlayerTouch == true)
            {
                Player.GetComponent<SpriteRenderer>().material.color = Color.red;
                //体力を減らす
                RecoveryPoint = status.MaxHP * (RecoveryDownPoint - 1.0f);
                status.HP = status.HP - RecoveryPoint;
                
                yield return new WaitForSeconds(1f);
                Player.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            //pv_Enemyが触れていたら
            if(pv_EnemyTouch == true)
            {
                
            }
            //player_Enemyが触れていたら
            if(player_EnemyTouch == true)
            {

            }
        }
    }
    IEnumerator SpeedDown()
    {
        //プレイヤーが触れていたら
        if (PlayerTouch == true)
        {
            //速度を減らす
            status.Speed = status.Speed - SpeedDownPoint;
            yield return null;
        }
        //pv_Enemyが触れていたら
        if(pv_EnemyTouch == true)
        {
            
        }
        //player_Enemyが触れていたら
        if(player_EnemyTouch == true)
        {

        }
    }
    IEnumerator SpeedReset()
    {

        if (PlayerTouch == false)
        {
            //速度を戻す
            status.Speed = status.Speed + SpeedDownPoint;
            yield return null;
        }
        if(pv_EnemyTouch == false)
        {
            
        }
        if(player_EnemyTouch == false)
        {

        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(TerrainType == 2)
        {
            if(col.gameObject.tag == "Attack")
            {
                HP = HP - status.Attack;
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Touch = true;
            PlayerTouch = true;
        }
        else if (col.gameObject.name == "pv_Enemy(Clone)")
        {
            //Touch = true;
            //pv_EnemyTouch = true;
            //スクリプトを参照
            //pv_enemy = col.gameObject.GetComponent<pv_EnemyMove>();
        }
        else if (col.gameObject.name == "player_Enemy(Clone)")
        {
            //Touch = true;
            //player_EnemyTouch = true;
            //スクリプトを参照
            //pl_enemy = col.gameObject.GetComponent<Pl_EnemyMove>();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Touch = false;
            PlayerTouch = false;
        }
        //else if (col.gameObject.name == "pv_Enemy(Clone)")
        //{
        //    Touch = false;
        //    pv_EnemyTouch = false;
        //}
        //else if (col.gameObject.name == "player_Enemy(Clone)")
        //{
        //    Touch = false;
        //    player_EnemyTouch = false;
        //}
    }

#if UNITY_EDITOR
    /**
     * Inspector拡張クラス
     */
    [CustomEditor(typeof(Terrain))]               //!< 拡張するときのお決まりとして書いてね
    public class BaseEditor : Editor           //!< Editorを継承するよ！
    {
        bool folding = false;

        public override void OnInspectorGUI()
        {
            //値の変更をする
            Undo.RecordObject(target, "te");
            // target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
            Terrain te = target as Terrain;

            //拠点の種類
            te.TerrainType = EditorGUILayout.IntField("地形の種類", te.TerrainType);

            // -- 拠点の体力 --
            te.HP = EditorGUILayout.FloatField("岩の耐久値", te.HP);

            // -- 速度 --
            te.SpeedDownPoint = EditorGUILayout.FloatField("速度減少値", te.SpeedDownPoint);

            // -- 回復量 --
            te.RecoveryDownPoint = EditorGUILayout.FloatField("ダメージ量倍率", te.RecoveryDownPoint);

            //値の変更を保存
            EditorUtility.SetDirty(te);
        }
    }
#endif
}
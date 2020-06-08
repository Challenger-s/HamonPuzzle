using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RippleGenerator : MonoBehaviour
{
    [SerializeField] int maxRippleCount;        // 同時に存在できる波紋の数
    [SerializeField] GameObject ripplePrefab;   // 波紋のPrefab
    [SerializeField] GameObject resonanceRipplePrefab;   // 共鳴の波紋のPrefab
    [SerializeField] RippleList rippleList;     // 波紋のList
    [SerializeField] RippleList resonanceRippleList; // 共鳴から発生した波紋のList
    [SerializeField] GameDirector m_gameDirector;
    [SerializeField] UI_RippleCount ui_RippleCount;
    Text m_remainRippleCountText;

    [SerializeField] float m_resonanceDelay;

    [SerializeField] ResonancePointList resonancePointList;

    int remainRippleCount;      // 波紋を生成できる残りの数
    bool overObject = false;    //　カーソルがオブジェクトに重なっているかのフラグ
    bool poseFlag = false;      //　ポーズ画面が開いているかのフラグ

    //　画面遷移時に実行
    void Start()
    {
        remainRippleCount = maxRippleCount;
        m_remainRippleCountText = this.transform.GetChild(0).           // 子オブジェクト(キャンバス)の取得
                gameObject.transform.GetChild(0).
                gameObject.transform.GetChild(0).                       // UI取得
                gameObject.GetComponent<Text>();                        // 子オブジェクト(テキスト)の取得

        RemainRippleCountTextUpdate();

        //m_gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    //　常に実行
    void Update()
    {
        if (m_gameDirector.m_phase == GameDirector.Phase.Play)
        {
            if (Input.GetMouseButtonDown(0) && remainRippleCount > 0 && !overObject && !poseFlag)
            {
                GenerateRipple();
            }
        }
    }

    // 波紋の生成関数
    void GenerateRipple()
    {
        // マウスのワールド座標取得
        Vector2 GeneratePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 波紋を作成
        GameObject ripple = Instantiate(ripplePrefab,
                                        GeneratePosition,
                                        Quaternion.identity);
        RippleController rippleController = ripple.transform.GetChild(0).GetComponent<RippleController>();
        rippleController.SetCenterPoint(GeneratePosition);
        rippleController.SetRippleGenerator(this);
        rippleList.AddRipple(rippleController);
        
        // 波紋の残りの数を減らす
        remainRippleCount--;
        RemainRippleCountTextUpdate();

        ripple.tag = "Ripple";

        ui_RippleCount.GenerateUIRipple();  //　UIの波紋が出てくる
    }
    
    //　右クリック時に呼ばれる
    public void Restart()
    {
        remainRippleCount = maxRippleCount;     //　カウントを復活
        RemainRippleCountTextUpdate();          //　テキストを更新
    }

    public void GenerateResonanceRipple(Vector2 position)
    {
        StartCoroutine(GenerateResonanceRipple_Coroutine(position));
    }
    

    IEnumerator GenerateResonanceRipple_Coroutine(Vector2 position)
    {

        //1フレーム停止
        yield return new WaitForSeconds(m_resonanceDelay);

        // 波紋を作成
        GameObject ripple = Instantiate(resonanceRipplePrefab,
                                        position,
                                        Quaternion.identity);
        RippleController rippleController = ripple.transform.GetChild(0).GetComponent<RippleController>();
        rippleController.SetCenterPoint(position);
        rippleController.SetRippleGenerator(this);
        resonanceRippleList.AddRipple(rippleController);

        ripple.tag = "ResonanceRipple";
    }

    //　波紋を起こせる回数が復活する処理
    public void IncreaseRemainRippleCount()
    {
        remainRippleCount++;
        RemainRippleCountTextUpdate();
    }

    //　波紋を起こせる回数のUI更新
    void RemainRippleCountTextUpdate()
    {
        m_remainRippleCountText.text = remainRippleCount.ToString();
    }

    //　カーソルがオブジェクトに重なっているかの判定を受け取る関数
    public void OverObject(bool check)
    {
        this.overObject = check;
    }

    //　カーソルがオブジェクトに重なっているかを返す
    public bool ReturnOverObject()
    {
        return overObject;
    }

    //　ポーズフラグを変更する関数
    public void ChangePoseFlag(bool flag)
    {
        poseFlag = flag;
    }

    //PlayScene_MusicDirectorに変数を送る
    public int GetRippleCount()
    {
        return remainRippleCount;
    }
}

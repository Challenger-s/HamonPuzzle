﻿using System.Collections;
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
    GameDirector m_gameDirector;
    Text m_remainRippleCountText;

    [SerializeField] float m_resonanceDelay;

    [SerializeField] ResonancePointList resonancePointList;
    [SerializeField] FitzoneList fitzoneList;

    int remainRippleCount;  // 波紋を生成できる残りの数


    // Start is called before the first frame update
    void Start()
    {
        remainRippleCount = maxRippleCount;
        m_remainRippleCountText = this.transform.GetChild(0).             // 子オブジェクト(キャンバス)の取得
                gameObject.transform.GetChild(0).   // 子オブジェクト(テキスト)の取得
                gameObject.GetComponent<Text>();

        RemainRippleCountTextUpdate();

        m_gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gameDirector.m_phase == GameDirector.Phase.Play)
        {
            if (Input.GetMouseButtonDown(0) && remainRippleCount > 0)
            {
                GenerateRipple();
            }
            if (Input.GetMouseButtonDown(1))
            {
                Restart();
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

    }
    
    void Restart()
    {
        remainRippleCount = maxRippleCount;
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
        GameObject ripple = Instantiate(ripplePrefab,
                                        position,
                                        Quaternion.identity);
        RippleController rippleController = ripple.transform.GetChild(0).GetComponent<RippleController>();
        rippleController.SetCenterPoint(position);
        rippleController.SetRippleGenerator(this);
        resonanceRippleList.AddRipple(rippleController);

    }

    public void IncreaseRemainRippleCount()
    {
        remainRippleCount++;
        RemainRippleCountTextUpdate();
    }

    void RemainRippleCountTextUpdate()
    {
        m_remainRippleCountText.text = remainRippleCount.ToString();
    }

    bool HitCheck()
    {

        int fitzoneCount = fitzoneList.;
        for(int i = 0;i < )


        return false;
    }
}

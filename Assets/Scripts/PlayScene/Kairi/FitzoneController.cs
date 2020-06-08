﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitzoneController : MonoBehaviour
{
    [SerializeField] int m_clearCount;  // クリアになる波紋の数

    [SerializeField] RippleList m_rippleList;
    [SerializeField] RippleList m_resonanceRippleList;
    [SerializeField] RippleGenerator rippleGenerator;
    [SerializeField] GameDirector m_gameDirector;

    public int m_hittingRippleCount = 0;   // 現在重なっている波紋の数
    Text m_countText;               // フィットゾーンの数字

    //　画面遷移時に実行
    void Start()
    {
        m_countText = this.transform.Find("Canvas").           // 子オブジェクト(キャンバス)の取得
                        gameObject.transform.GetChild(0).   // 子オブジェクト(テキスト)の取得
                        gameObject.GetComponent<Text>();
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_countText.rectTransform.position = RectTransformUtility.WorldToScreenPoint(camera,this.transform.position);

        //rippleGenerator = GameObject.Find("RippleGenerator").GetComponent<RippleGenerator>();

        CountTextUpdate();
    }


    //　常に実行
    void Update()
    {
        if(m_gameDirector.m_phase == GameDirector.Phase.Play)
        {
            RippleHitCheck();
            CountTextUpdate();
        }
    }

    // いくつ波紋が重なっているか調べる関数
    void RippleHitCheck()
    {
        m_hittingRippleCount = 0;

        int rippleCount = m_rippleList.GetRippleCount();
        for(int i = 0;i < rippleCount; i++)
        {
            RippleController rippleController = m_rippleList.GetRippleController(i);
            Vector2 rippleCenterPoint = rippleController.GetRippleCenterPoint();
            float rippleSize = rippleController.GetRippleSize();

            // フィットゾーンの中心点から波紋の中心点への直線距離の取得
            float distanceFromRipple = Mathf.Abs(Mathf.Sqrt((this.transform.position.x-rippleCenterPoint.x)*
                                                  (this.transform.position.x-rippleCenterPoint.x)+
                                                  (this.transform.position.y - rippleCenterPoint.y) *
                                                  (this.transform.position.y - rippleCenterPoint.y)));

            float distanceFromRipple_scaleCalculated_inner = (distanceFromRipple - this.transform.localScale.x/2) - (rippleSize + rippleController.GetRippleColliderWidth());
            float distanceFromRipple_scaleCalculated_outer = (rippleSize - rippleController.GetRippleColliderWidth()) - (distanceFromRipple + this.transform.localScale.x/2);
            
            // 円の当たり判定の応用
            if (distanceFromRipple_scaleCalculated_inner < 0 && distanceFromRipple_scaleCalculated_outer < 0)
            {
                m_hittingRippleCount++;
            }
            
        }
        int resonanceRippleCount = m_resonanceRippleList.GetRippleCount();
        for(int i = 0;i < resonanceRippleCount; i++)
        {
            RippleController rippleController = m_resonanceRippleList.GetRippleController(i);
            Vector2 rippleCenterPoint = rippleController.GetRippleCenterPoint();
            float rippleSize = rippleController.GetRippleSize();

            // フィットゾーンの中心点から波紋の中心点への直線距離の取得
            float distanceFromRipple = Mathf.Abs(Mathf.Sqrt((this.transform.position.x-rippleCenterPoint.x)*
                                                  (this.transform.position.x-rippleCenterPoint.x)+
                                                  (this.transform.position.y - rippleCenterPoint.y) *
                                                  (this.transform.position.y - rippleCenterPoint.y)));

            float distanceFromRipple_scaleCalculated_inner = (distanceFromRipple - this.transform.localScale.x/2) - (rippleSize + rippleController.GetRippleColliderWidth());
            float distanceFromRipple_scaleCalculated_outer = (rippleSize - rippleController.GetRippleColliderWidth()) - (distanceFromRipple + this.transform.localScale.x/2);
            
            // 円の当たり判定の応用
            if (distanceFromRipple_scaleCalculated_inner < 0 && distanceFromRipple_scaleCalculated_outer < 0)
            {
                m_hittingRippleCount++;
            }
        }
    }
    
    // 数字の表示を更新する関数
    void CountTextUpdate()
    {
        m_countText.text = (m_clearCount - m_hittingRippleCount).ToString();
    }

    public int GetCount()
    {
        return m_clearCount - m_hittingRippleCount;
    }

    //　右クリック時に呼ばれる
    public void Restart()
    {
        m_hittingRippleCount = 0;   // クリアカウントをリセット
        CountTextUpdate();                  //　テキストを更新
    }

    //　カーソルが重なっているかの判定 
    private void OnMouseEnter()
    {
        this.rippleGenerator.OverObject(true);
    }

    //　カーソルが離れた時の判定
    private void OnMouseExit()
    {
        this.rippleGenerator.OverObject(false);
    }
}

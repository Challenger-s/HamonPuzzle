﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonancePointController : MonoBehaviour
{
    [SerializeField] RippleList m_rippleList;
    [SerializeField] float ColliderSize;
    [SerializeField] float rotationTakeTime = 10f;
    [SerializeField] float speedUpTimes = 5f;
    [SerializeField] float returnTakeTime = 5f;
    [SerializeField] RippleGenerator m_rippleGenerator;

    List<bool> m_ripplesIsHittedList = new List<bool>();

    float defoultRotationTakeTime = 0f;

    AudioSource[] audioSource; //オーディオソース使用

    int resonanceCounter = 0; //共鳴が鳴った回数を

    //　最初の1フレームのみ実行
    private void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得

        defoultRotationTakeTime = rotationTakeTime;
    }


    //　常に実行
    private void Update()
    {
        HitCheck();
        Rotate();
    }


    void HitCheck()
    {
        int rippleCount = m_rippleList.GetRippleCount();
        for (int i = 0; i < rippleCount; i++)
        {
            if (m_ripplesIsHittedList[i] == true) { continue; }

            RippleController rippleController = m_rippleList.GetRippleController(i);
            Vector2 rippleCenterPoint = rippleController.GetRippleCenterPoint();
            float rippleSize = rippleController.GetRippleSize();

            // 共鳴の中心点から波紋の中心点への直線距離の取得
            float distanceFromRipple = Mathf.Abs(Mathf.Sqrt((this.transform.position.x - rippleCenterPoint.x) *
                                                  (this.transform.position.x - rippleCenterPoint.x) +
                                                  (this.transform.position.y - rippleCenterPoint.y) *
                                                  (this.transform.position.y - rippleCenterPoint.y)));

            float distanceFromRipple_scaleCalculated_inner = distanceFromRipple - rippleSize + rippleController.GetRippleColliderWidth();
            float distanceFromRipple_scaleCalculated_outer = rippleSize - rippleController.GetRippleColliderWidth() - distanceFromRipple;

            //float distanceFromRipple_scaleCalculated_inner = (distanceFromRipple - this.transform.localScale.x / 2) - (rippleSize + rippleController.GetRippleColliderWidth());
            //float distanceFromRipple_scaleCalculated_outer = (rippleSize - rippleController.GetRippleColliderWidth()) - (distanceFromRipple + this.transform.localScale.x / 2);


            //Debug.Log(i.ToString());
            // 円の当たり判定の応用
            if (distanceFromRipple_scaleCalculated_inner < ColliderSize && distanceFromRipple_scaleCalculated_outer < ColliderSize)
            {
                switch (this.resonanceCounter)
                {
                    case 0:
                        audioSource[Random.Range(0, 2)].Play(); //0か1番目の音を鳴らす
                        this.resonanceCounter = 1; //カウントを１に
                        break;
                    case 1:
                        audioSource[Random.Range(2, 4)].Play(); //2か3番目の音を鳴らす
                        this.resonanceCounter = 0; //カウントを０に
                        break;
                }

                m_ripplesIsHittedList[i] = true;
                rotationTakeTime = rotationTakeTime / speedUpTimes;
                m_rippleGenerator.GenerateResonanceRipple(this.transform.position);
            }

        }

    }

    //　回転
    void Rotate()
    {
        //　徐々に通常の回転速度に戻す処理
        if (rotationTakeTime < defoultRotationTakeTime)
        {
            rotationTakeTime += Time.deltaTime * (defoultRotationTakeTime - rotationTakeTime / speedUpTimes) / returnTakeTime;

            if (rotationTakeTime > defoultRotationTakeTime)
            {
                rotationTakeTime = defoultRotationTakeTime;
            }
        }
        this.transform.Rotate(0, 0, Time.deltaTime * 360 / rotationTakeTime);  //　常に回転
    }

    public void Add_ripplesIsHittedList()
    {
        bool b = false;
        m_ripplesIsHittedList.Add(b);
    }

    public void Remove_ripplesIsHittedList(int index)
    {
        m_ripplesIsHittedList.Remove(m_ripplesIsHittedList[index]);
    }

    //　カーソルが重なっているかの判定 
    private void OnMouseEnter()
    {
        this.m_rippleGenerator.OverObject(true);
    }

    //　カーソルが離れた時の判定
    private void OnMouseExit()
    {
        this.m_rippleGenerator.OverObject(false);        
    }
}

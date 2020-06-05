﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusicDirector : MonoBehaviour
{
    AudioSource[] audioSource; //オーディオソース使用

    bool destroyFlag = false; //カウントを始めるか判断するフラグ
    float destroyCountUpTimer = 0; //削除までのカウント

    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //左マウスボタンを押した瞬間
        {
            audioSource[0].Play(); //音を鳴らす
            this.destroyFlag = true; //フラグをオン(破壊カウント開始)
        }

        if(this.destroyFlag = true)
        {
            this.destroyCountUpTimer += Time.deltaTime; // タイマー加算
            if (this.destroyCountUpTimer < 3) //３秒後に判定
            {
                Destroy(this.gameObject);//このオブジェクトをデストロイする
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene_MusicDIrector : MonoBehaviour
{
    AudioSource[] audioSource; //オーディオソース使用

    int randomMusicPattern = -1; //音のパターン
    int musicCounter = 0; //音をカウントする

    [SerializeField]
    RippleGenerator rippleGenerator; //他スクリプトから使用

    [SerializeField] GameDirector gameDirector;
    [SerializeField] GameD poseDirector;

    int rippleCounter = 0; //波紋を出せる回数

    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得

        this.randomMusicPattern = Random.Range(0, 2); //パターン変更
        Debug.Log("パターン変更" + this.randomMusicPattern);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.rippleCounter > 0 && gameDirector.ReturnPhase() && !rippleGenerator.ReturnOverObject() && !poseDirector.ReturnPoseDisplay())
        {
            if (this.randomMusicPattern == 0) //ランダムパターンが０だったら
            {
                switch (this.musicCounter)
                {
                    case 0: //カウンターが０なら
                        audioSource[0].Play();
                        this.musicCounter = 1; //カウンターを１にする
                        break;
                    case 1: //カウンターが１なら
                        audioSource[1].Play();
                        this.musicCounter = 2; //カウンターを２にする
                        break;
                    case 2: //カウンターが２なら
                        audioSource[2].Play();
                        this.musicCounter = 0; //カウンターをリセットする
                        this.randomMusicPattern = Random.Range(0, 2); //パターン変更
                        Debug.Log("パターン変更" + this.randomMusicPattern);
                        break;
                }
            }
            else if (this.randomMusicPattern == 1) //ランダムパターンが1だったら
            {
                switch (this.musicCounter)
                {
                    case 0: //カウンターが０なら
                        audioSource[1].Play();
                        this.musicCounter = 1; //カウンターを１にする
                        break;
                    case 1: //カウンターが１なら
                        audioSource[0].Play();
                        this.musicCounter = 2; //カウンターを２にする
                        break;
                    case 2: //カウンターが２なら
                        audioSource[2].Play();
                        this.musicCounter = 0; //カウンターをリセットする
                        this.randomMusicPattern = Random.Range(0, 2); //パターン変更
                        Debug.Log("パターン変更" + this.randomMusicPattern);
                        break;
                }
            }
        }
        this.rippleCounter = rippleGenerator.GetRippleCount(); //RippleGeneratorから波紋を出せる回数を取得
    }
}

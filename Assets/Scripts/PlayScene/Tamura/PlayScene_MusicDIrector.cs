using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene_MusicDIrector : MonoBehaviour
{
    AudioSource[] audioSource; //オーディオソース使用

    bool randomStopflag = false; //音の抽選を止めるフラグ
    bool resetStopflag = false; //波紋数のリセットを止めるフラグ

    [SerializeField]
    int maxRippleCount = 10; //現在のステージで出せる波紋の最大数を入力

    [SerializeField]
    RippleGenerator rippleGenerator; //他スクリプトから使用

    [SerializeField] GameDirector gameDirector;

    int rippleCounter = 0; //波紋を出せる回数
    int rippleVariation = -1; //ランダムに決める波紋の組み合わせ

    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && this.rippleCounter >= 1 && gameDirector.ReturnPhase())
        {
            audioSource[Random.Range(0, 3)].Play();
        }
        this.rippleCounter = rippleGenerator.GetRippleCount(); //RippleGeneratorから波紋を出せる回数を取得
    }

    /*
    void Update()
    {  
        if (Input.GetMouseButtonDown(0) && this.rippleCounter > 0 && gameDirector.ReturnPhase()) //左マウスボタンを押した瞬間、波紋カウンターが０より大きいとき
        {
            this.resetStopflag = false; //リセ止めフラグオフ　リセットできるようになる

            if (this.maxRippleCount == 1) //ステージの波紋最大数が１のとき
            {
                audioSource[Random.Range(0, 4)].Play(); //0~3の中の音をランダムで鳴らす
            }
            else if (this.maxRippleCount == 2) //ステージの波紋最大数が２のとき
            {
                if (this.randomStopflag == false) //抽選を止めるフラグがオフの時
                {
                    this.rippleVariation = Random.Range(0, 6); //0~5の数字を抽選し、変数に代入
                    this.randomStopflag = true; //抽選を止めるフラグをオン
                }

                RippleSound_2(this.rippleVariation); //波紋の音を鳴らす処理（出せる回数が２回のとき）
            }
            else if (this.maxRippleCount == 3) //ステージの波紋最大数が３のとき
            {
                if (this.randomStopflag == false) //抽選を止めるフラグがオフの時
                {
                    Debug.Log("３回　抽選");
                    this.rippleVariation = Random.Range(0, 9); //0~8の数字を抽選し、変数に代入
                    Debug.Log(rippleVariation);
                    this.randomStopflag = true; //抽選を止めるフラグをオン
                }

                RippleSound_3(this.rippleVariation); //波紋の音を鳴らす処理（出せる回数が３回のとき）
            }
        }

        //波紋を出せる回数が回復したときに、音の情報をリセットする処理
        if (this.rippleCounter == this.maxRippleCount && this.resetStopflag == false) //波紋カウンタが最大値に回復し、かつリセ止めフラグがオフだったら
        {
            Debug.Log("リセット");
            this.randomStopflag = false; //抽選止めフラグをオフ　抽選できるようにする
            this.resetStopflag = true; //リセ止めフラグをオン　不要なリセットを止める
            this.rippleVariation = -1; //バリエーション決め変数をリセットする
        }
}*/

    void RippleSound_2(int rippleVar) //波紋の音を鳴らす処理（出せる回数が２回のとき）
    {
        /*
        波紋の組み合わせ
        ・２回の時
        0 ０、１　3 １、０
        1 １、２　4 ２、１
        2 ２、３　5 ３、２
        */

        if (this.rippleCounter == 1) //波紋カウンターが1のとき
        {
            if (rippleVar == 0) //バリエーションが０のとき
            {
                audioSource[0].Play(); //０番目の音を鳴らす
            }
            else if (rippleVar == 1) //バリエーションが1のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 2) //バリエーションが2のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 3) //バリエーションが3のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 4) //バリエーションが4のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 5) //バリエーションが5のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
        }
        else if (this.rippleCounter == 0) //波紋カウンターが0のとき
        {
            if (rippleVar == 0) //バリエーションが０のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 1) //バリエーションが1のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 2) //バリエーションが2のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
            else if (rippleVar == 3) //バリエーションが3のとき
            {
                audioSource[0].Play(); //0番目の音を鳴らす
            }
            else if (rippleVar == 4) //バリエーションが4のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 5) //バリエーションが5のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }

            this.randomStopflag = false; //抽選を止めるフラグをオフ
        }
    }

    void RippleSound_3(int rippleVar) //波紋の音を鳴らす処理（出せる回数が３回のとき）
    {
        Debug.Log("３回　開始");

        /*
        波紋の組み合わせ
        ・３回の時
        0 ０、１、２　4 ２、１、０
        1 ０、２、３　5 ３、２、０
        2 ０、１、３　6 ３、１、０
        3 １、２、３　7 ３、２、１
        */

        if (this.rippleCounter == 2) //波紋カウンターが2のとき
        {
            if (rippleVar == 0) //バリエーションが０のとき
            {
                audioSource[0].Play(); //０番目の音を鳴らす
            }
            else if (rippleVar == 1) //バリエーションが1のとき
            {
                audioSource[0].Play(); //0番目の音を鳴らす
            }
            else if (rippleVar == 2) //バリエーションが2のとき
            {
                audioSource[0].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 3) //バリエーションが3のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 4) //バリエーションが4のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 5) //バリエーションが5のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
            else if (rippleVar == 6) //バリエーションが6のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
            else if (rippleVar == 7) //バリエーションが7のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }

            this.rippleCounter = 1; //波紋カウンターを１にする
        }
        else if (this.rippleCounter == 1) //波紋カウンターが１のとき
        {
            if (rippleVar == 0) //バリエーションが０のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 1) //バリエーションが1のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 2) //バリエーションが2のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 3) //バリエーションが3のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 4) //バリエーションが4のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 5) //バリエーションが5のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 6) //バリエーションが6のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }
            else if (rippleVar == 7) //バリエーションが7のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }

            this.rippleCounter = 2; //波紋カウンターを2にする
        }
        else if (this.rippleCounter == 0) //波紋カウンターが0のとき
        {
            if (rippleVar == 0) //バリエーションが０のとき
            {
                audioSource[2].Play(); //2番目の音を鳴らす
            }
            else if (rippleVar == 1) //バリエーションが1のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
            else if (rippleVar == 2) //バリエーションが2のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
            else if (rippleVar == 3) //バリエーションが3のとき
            {
                audioSource[3].Play(); //3番目の音を鳴らす
            }
            else if (rippleVar == 4) //バリエーションが4のとき
            {
                audioSource[0].Play(); //0番目の音を鳴らす
            }
            else if (rippleVar == 5) //バリエーションが5のとき
            {
                audioSource[0].Play(); //0番目の音を鳴らす
            }
            else if (rippleVar == 6) //バリエーションが6のとき
            {
                audioSource[0].Play(); //0番目の音を鳴らす
            }
            else if (rippleVar == 7) //バリエーションが7のとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
            }

            this.randomStopflag = false; //抽選を止めるフラグをオフ
        }
    }

}

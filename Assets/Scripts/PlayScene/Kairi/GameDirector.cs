using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField] FitzoneController[] m_fitzoneArray;
    [SerializeField] UI_RippleCount ui_RippleCount;
    [SerializeField] RippleGenerator rippleGenerator;
    [SerializeField] Image fadeImage;
    [SerializeField] float startFadeInSpeed = 0.5f;
    [SerializeField] float restartFadeSpeed = 0.5f;
    [SerializeField] float clearStopTime = 0.5f;
    [SerializeField] float pauseRainVolume = 0.5f;

    AudioSource[] audioSource; //オーディオソース使用（4つ

    bool whiteCircleFlag = true;
    bool restartRippleDelete = false;

    public enum Phase
    {
        PreStart,
        Play,
        Pause,
        Restart,
        BeforeClear,
        Clear,
    }

    public Phase m_phase;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_phase)
        {
            case Phase.PreStart:
                ui_RippleCount.UI_IN();
                if (FadeIn(fadeImage, startFadeInSpeed))
                {
                    m_phase = Phase.Play;
                }
                break;

            case Phase.Play:
                if (ClearCheck())
                {
                    BeforeClear();
                    break;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    RightMouseClick();
                }
                break;

            case Phase.Pause:                
                break;

            case Phase.Restart:
                Restart();
                break;


            case Phase.BeforeClear:
                clearStopTime -= Time.deltaTime;
                if (clearStopTime <= 0)
                {
                    m_phase = Phase.Clear;
                }
                ui_RippleCount.UI_OUT();
                break;

            case Phase.Clear:
                if (!this.audioSource[1].isPlaying && whiteCircleFlag) //クリアフラグがオンになったら
                {
                    audioSource[1].Play(); //1番目の音を鳴らす
                    whiteCircleFlag = false;
                }
                audioSource[2].volume = audioSource[2].volume - 0.15f * Time.deltaTime; //「雨音」の音量を下げる
                break;
        }
    }

    //　リスタート演出
    void Restart()
    {
        if (restartRippleDelete)
        {
            if (FadeIn(fadeImage, restartFadeSpeed))
            {
                restartRippleDelete = false;
                m_phase = Phase.Play;
            }
        }
        else if (FadeOut(fadeImage, restartFadeSpeed))
        {
            //　プレイヤーの起こした波紋が消える
            GameObject[] ripple = GameObject.FindGameObjectsWithTag("Ripple");
            RippleController[] rippleControllers = new RippleController[ripple.Length];
            for (int i = ripple.Length - 1; i >= 0; i--)
            {
                rippleControllers[i] = ripple[i].GetComponentInChildren<RippleController>();
                rippleControllers[i].Restart();
            }
            //　共鳴の波紋が消える
            GameObject[] resonanceRipple = GameObject.FindGameObjectsWithTag("ResonanceRipple");
            RippleController[] resonanceRippleControllers = new RippleController[resonanceRipple.Length];
            for (int i = resonanceRipple.Length - 1; i >= 0; i--)
            {
                resonanceRippleControllers[i] = resonanceRipple[i].GetComponentInChildren<RippleController>();
                resonanceRippleControllers[i].Restart();
            }
            rippleGenerator.Restart();      //　波紋の数をリセット

            //　フィットゾーンのクリアカウントをリセット
            GameObject[] fitzones = GameObject.FindGameObjectsWithTag("FitZone");
            FitzoneController[] fitzoneControllers = new FitzoneController[fitzones.Length];
            for(int i = 0; i < fitzones.Length; i++)
            {
                fitzoneControllers[i] = fitzones[i].GetComponent<FitzoneController>();
                fitzoneControllers[i].Restart();
            }
            restartRippleDelete = true;     //　波紋を消したフラグを立てる
        }
    }

    //　右クリック時
    void RightMouseClick()
    {
        audioSource[3].Play(); //音鳴らす

        m_phase = Phase.Restart;
    }


    bool ClearCheck()
    {
        for (int i = 0; i < m_fitzoneArray.Length; i++)
        {
            if (m_fitzoneArray[i].GetCount() != 0) { return false; }
        }

        return true;
    }

    //　ポーズ画面に入る
    public void EnterPause()
    {
        m_phase = Phase.Pause;
        audioSource[2].volume = pauseRainVolume;
    }

    //　ポーズ画面から出る
    public void ExitPause()
    {
        m_phase = Phase.Play;
        audioSource[2].volume = 1f;
    }

    void BeforeClear()
    {
        m_phase = Phase.BeforeClear;
        audioSource[0].Play(); //0番目の音を鳴らす
    }

    bool FadeIn(Image image, float fadeSpeed)
    {
        image.color = new Color(255, 255, 255, image.color.a - (fadeSpeed * Time.deltaTime));

        if (image.color.a < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool FadeOut(Image image, float fadeSpeed)
    {
        image.color = new Color(255, 255, 255, image.color.a + (fadeSpeed * Time.deltaTime));

        if (image.color.a > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    //　Phaseを返す
    public bool ReturnPhase()
    {
        if (m_phase == Phase.Play)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PhaseChange(Phase change)
    {
        m_phase = change;
    }

    //　クリアの時の止まっている時間
    public bool ReturnClearStopTIme()
    {
        if (clearStopTime <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField] FitzoneController[] m_fitzoneArray;
    [SerializeField] UI_RippleCount ui_RippleCount;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeInSpeed = 0.5f;
    [SerializeField] float clearStopTime = 0.5f;

    AudioSource[] audioSource; //オーディオソース使用（３つ

    bool whiteCircleFlag = true;

    public enum Phase
    {
        PreStart,
        Play,
        Pause,
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
                if (FadeIn(fadeImage))
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
                    Restart();
                }
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
                break;
        }
    }

    void Restart()
    {

    }

    bool ClearCheck()
    {
        for (int i = 0; i < m_fitzoneArray.Length; i++)
        {
            if (m_fitzoneArray[i].GetCount() != 0) { return false; }
        }

        return true;
    }

    void BeforeClear()
    {
        m_phase = Phase.BeforeClear;
        audioSource[0].Play(); //0番目の音を鳴らす
    }

    bool FadeIn(Image image)
    {
        image.color = new Color(255, 255, 255, image.color.a - (fadeInSpeed * Time.deltaTime));

        if (image.color.a < 0)
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

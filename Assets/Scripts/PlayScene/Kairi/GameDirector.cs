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

    public enum Phase
    {
        PreStart,
        Play,
        Pause,
        Clear,
    }

    public Phase m_phase;

    // Start is called before the first frame update
    void Start()
    {

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
                    Clear();
                    break;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    Restart();
                }
                break;

            case Phase.Clear:
                ui_RippleCount.UI_OUT();
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

    void Clear()
    {
        m_phase = Phase.Clear;
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
}

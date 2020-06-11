using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntervalUI : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] RippleGenerator rippleGenerator;

    RectTransform recttransform;
    Image image;

    Vector2 vec = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        recttransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameDirector.m_phase == GameDirector.Phase.BeforeClear || gameDirector.m_phase == GameDirector.Phase.Pause)
        {
            NotDisplay();
        }
        else if(gameDirector.m_phase==GameDirector.Phase.Play && !ReturnColorA())
        {
            Display();
        }

        TrackingMouse();
        Interval();
    }

    //　追尾
    void TrackingMouse()
    {
        vec.x = Input.mousePosition.x * 1920 / Screen.width;
        vec.y = Input.mousePosition.y * 1080 / Screen.height;
        recttransform.anchoredPosition = vec;
    }

    //　インターバルUI
    void Interval()
    {
        if (this.gameDirector.m_phase == GameDirector.Phase.Restart)
        {
            image.fillAmount = 0f;
        }
        else if(this.rippleGenerator.GetRippleCount() != 0)
        {
            image.fillAmount = rippleGenerator.ReturnInterval() / rippleGenerator.ReturnDefoultInterval();
        }
    }

    //　画面に表示
    void Display()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    //　画面から消す
    void NotDisplay()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

    //　透明度をチェック
    bool ReturnColorA()
    {
        if (image.color.a == 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

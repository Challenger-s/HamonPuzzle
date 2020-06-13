using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseButton : MonoBehaviour
{
    [SerializeField] float largeSize = 1.1f;
    [SerializeField] float changeSizeTime = 1f;
    [SerializeField] float returnSizeTime = 0.5f;

    float defoultSizeX = 0;
    float defoultSizeY = 0;

    float clickSmallSize = 0.9f;
    float clickSmallTime = 0.35f;

    bool onFlag = false;
    bool clickFlag = false;
    bool smalledFlag = false;

    public enum Buttons
    {
        click,
        large,
        smaller,

        normal,
    }

    public Buttons touchedButton = Buttons.normal;

    private void Awake()
    {
        this.gameObject.GetComponent<Button>().interactable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        defoultSizeX = GetComponent<RectTransform>().sizeDelta.x;
        defoultSizeY = GetComponent<RectTransform>().sizeDelta.y;

        this.gameObject.GetComponent<Button>().interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (touchedButton)
        {
            case Buttons.click:
                if (Clicked())
                {
                    touchedButton = Buttons.normal;
                }
                break;

            case Buttons.large:
                
                if (Large())
                {
                    touchedButton = Buttons.normal;
                }
                
                break;

            case Buttons.smaller:
                if (Small())
                {
                    touchedButton = Buttons.normal;
                }
                
                break;
        }
    }

    //　サイズを段々大きくする
    public bool Large()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        if (size.x < defoultSizeX * largeSize)
        {
            size.x = size.x + size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            GetComponent<RectTransform>().sizeDelta = size;
            if (size.x > defoultSizeX * largeSize)
            {
                size.x = defoultSizeX * largeSize;
                size.y = defoultSizeY * largeSize;
                GetComponent<RectTransform>().sizeDelta = size;
                return true;
            }
        }
        return false;

        //Debug.Log("big");
    }

    //　サイズを段々小さくして元に戻す
    public bool Small()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        if (size.x > defoultSizeX)
        {
            size.x = size.x - size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            GetComponent<RectTransform>().sizeDelta = size;
            if (size.x < defoultSizeX)
            {
                size.x = defoultSizeX;
                size.y = defoultSizeY;
                GetComponent<RectTransform>().sizeDelta = size;
                return true;
            }
        }
        return false;
        //Debug.Log("small");
    }

    //　クリックされたとき
    public bool Clicked()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        //　元に戻る
        if (size.x < defoultSizeX && smalledFlag)
        {
            size.x = size.x + size.x * 1 / clickSmallTime * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / clickSmallTime * Time.unscaledDeltaTime;
            GetComponent<RectTransform>().sizeDelta = size;
            if (size.x > defoultSizeX)
            {
                size.x = defoultSizeX;
                size.y = defoultSizeY;
                GetComponent<RectTransform>().sizeDelta = size;
                return true;
            }
            Debug.Log("big");
        }
        //　小さくなる
        else if (size.x > defoultSizeX * clickSmallSize && !smalledFlag)
        {
            size.x = size.x - size.x * 1 / clickSmallTime * clickSmallSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / clickSmallTime * clickSmallSize * Time.unscaledDeltaTime;
            GetComponent<RectTransform>().sizeDelta = size;
            if (size.x <= defoultSizeX * clickSmallSize)
            {
                size.x = defoultSizeX * clickSmallSize;
                size.y = defoultSizeY * clickSmallSize;
                GetComponent<RectTransform>().sizeDelta = size;
                smalledFlag = true;
            }
            Debug.Log("small");
        }
        return false;
        //Debug.Log("click");
    }

    //　サイズを一瞬で元に戻す
    public void SizeReset()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        size.x = defoultSizeX;
        size.y = defoultSizeY;
        GetComponent<RectTransform>().sizeDelta = size;
        onFlag = false;
        smalledFlag = false;
        clickFlag = false;
    }

    //　クリックされたとき
    public void OnClick()
    {
        clickFlag = true;
        if(this.gameObject.name == "MenuButton")
        {
            touchedButton = Buttons.click;
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    //　マウスが乗ったとき
    public void On_mouse()
    {
        onFlag = true;
        touchedButton = Buttons.large;
    }

    //　マウスが離れたとき
    public void Exit_mouse()
    {
        onFlag = false;
        touchedButton = Buttons.smaller;
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PoseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float largeSize = 1.1f;
    [SerializeField] float changeSizeTime = 1f;
    [SerializeField] float clickedSize = 0.8f;
    [SerializeField] float clickSizeTime = 1f;
    [SerializeField] float returnSizeTime = 0.5f;

    float defoultSizeX = 0;
    float defoultSizeY = 0;

    bool clickSmallFinish = false;
    bool clickAsBeforeFinish = false;
    float clickSmallSize = 0.9f;
    float clickSmallTime = 0.35f;

    public enum Button
    {
        click,
        large,
        smaller,

        normal,
    }

    Button touchedButton = Button.normal;

    // Start is called before the first frame update
    void Start()
    {
        defoultSizeX = GetComponent<RectTransform>().sizeDelta.x;
        defoultSizeY = GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (touchedButton)
        {
            case Button.click:
                if (ClickProduction())
                {
                    this.gameObject.SetActive(false);
                }
                break;

            case Button.large:

                if (Large())
                {
                    touchedButton = Button.normal;
                }
                break;

            case Button.smaller:
                if (Small())
                {
                    touchedButton = Button.normal;
                }
                break;
        }
        Debug.Log(touchedButton);
    }

    //　サイズを段々大きくする
    public bool Large()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        if(size.x < defoultSizeX * largeSize)
        {
            size.x = size.x + size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;

            if (size.x > defoultSizeX * largeSize)
            {
                size.x = defoultSizeX * largeSize;
                size.y = defoultSizeY * largeSize;
                GetComponent<RectTransform>().sizeDelta = size;
                return true;
            }
        }
        return false;
    }

    //　サイズを段々小さくして元に戻す
    public bool Small()
    {
        var size = GetComponent<RectTransform>().sizeDelta;        
        if (size.x > defoultSizeX)
        {
            size.x = size.x - size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            GetComponent<RectTransform>().sizeDelta = size;

            if (size.x < defoultSizeX)
            {
                size.x = defoultSizeX;
                size.y = defoultSizeY;
                GetComponent<RectTransform>().sizeDelta = size;
                return true;
            }
        }
        
        return false;
    }

    public bool ClickProduction()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        if (!clickSmallFinish)
        {
            if (size.x > defoultSizeX * clickSmallSize)
            {
                size.x = size.x - size.x * 1 / clickSmallTime * clickSmallSize * Time.unscaledDeltaTime;
                size.y = size.y - size.y * 1 / clickSmallTime * clickSmallSize * Time.unscaledDeltaTime;
                GetComponent<RectTransform>().sizeDelta = size;
            }
            else if (size.x < defoultSizeX * clickSmallSize)
            {
                size.x = defoultSizeX * clickSmallSize;
                size.y = defoultSizeX * clickSmallSize;
                GetComponent<RectTransform>().sizeDelta = size;
                clickSmallFinish = true;
            }
        }
        else if (clickSmallFinish && !clickAsBeforeFinish)
        {
            if (size.x < defoultSizeX)
            {
                size.x = size.x + size.x * 1 / clickSmallTime * defoultSizeX * Time.unscaledDeltaTime;
                size.y = size.y + size.y * 1 / clickSmallTime * defoultSizeY * Time.unscaledDeltaTime;
                GetComponent<RectTransform>().sizeDelta = size;
            }
            else if (size.x > defoultSizeX)
            {
                Debug.Log("a");
                size.x = defoultSizeX;
                size.y = defoultSizeY;
                GetComponent<RectTransform>().sizeDelta = size;
                clickAsBeforeFinish = true;
            }
        }
        else if (clickAsBeforeFinish)
        {
            clickSmallFinish = false;
            clickAsBeforeFinish = false;
            return true;
        }
        return false;
    }

    ////　クリックされたとき
    //public void Clicked()
    //{
    //    var size = GetComponent<RectTransform>().sizeDelta;
    //    if (!clickSmallFinish)
    //    {
    //        //　小さくなる
    //        if (size.x > defoultSizeX * clickedSize)
    //        {
    //            size.x = size.x - size.x * 1 / clickSizeTime * largeSize * Time.unscaledDeltaTime;
    //            size.y = size.y - size.y * 1 / clickSizeTime * largeSize * Time.unscaledDeltaTime;

    //            if (size.x <= defoultSizeX * clickedSize)
    //            {
    //                size.x = defoultSizeX * clickedSize;
    //                size.y = defoultSizeY * clickedSize;
    //                GetComponent<RectTransform>().sizeDelta = size;
    //                clickSmallFinish = true;
    //            }
    //        }
    //    }
    //    else if (clickSmallFinish && !clickAsBeforeFinish)
    //    {
    //        //　元に戻る
    //        if (size.x < defoultSizeX)
    //        {
    //            size.x = size.x + size.x * 1 / changeSizeTime * Time.unscaledDeltaTime;
    //            size.y = size.y + size.y * 1 / changeSizeTime * Time.unscaledDeltaTime;

    //            if (size.x > defoultSizeX)
    //            {
    //                size.x = defoultSizeX;
    //                size.y = defoultSizeY;
    //                clickAsBeforeFinish = true;
    //            }
    //            GetComponent<RectTransform>().sizeDelta = size;
    //        }
    //    }
    //    else if (clickAsBeforeFinish)
    //    {
    //        clickSmallFinish = false;
    //        clickAsBeforeFinish = false;
    //    }
    //}

    //　サイズを一瞬で元に戻す
    public void SizeReset()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        size.x = defoultSizeX;
        size.y = defoultSizeY;
        GetComponent<RectTransform>().sizeDelta = size;
        touchedButton = Button.normal;
    }

    //　クリックされたとき
    public void OnClick()
    {
        touchedButton = Button.click;
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        touchedButton = Button.large;
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    // 
    public void OnPointerExit(PointerEventData eventData)
    {
        touchedButton = Button.smaller;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseButton : MonoBehaviour
{
    [SerializeField] float largeSize = 1.1f;
    [SerializeField] float changeSizeTime = 1f;
    [SerializeField] float clickedSize = 0.8f;
    [SerializeField] float clickSizeTime = 1f;
    [SerializeField] float returnSizeTime = 0.5f;

    float defoultSizeX = 0;
    float defoultSizeY = 0;

    bool onFlag = false;
    bool clickFlag = false;
    bool smalledFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        defoultSizeX = GetComponent<RectTransform>().sizeDelta.x;
        defoultSizeY = GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (onFlag)
        {
            Large();    //　大きくする
        }
        else if (!onFlag)
        {
            Small();    //　小さくする
        }
        else if (clickFlag)
        {
            //Clicked();  //　クリックされたときの挙動
        }
    }

    //　サイズを段々大きくする
    void Large()
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
            }
        }
        GetComponent<RectTransform>().sizeDelta = size;
    }

    //　サイズを段々小さくして元に戻す
    void Small()
    {
        var size = GetComponent<RectTransform>().sizeDelta;        
        if (size.x > defoultSizeX)
        {
            size.x = size.x - size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;

            if (size.x < defoultSizeX)
            {
                size.x = defoultSizeX;
                size.y = defoultSizeY;
            }
        }
        GetComponent<RectTransform>().sizeDelta = size;
    }

    /*
    //　クリックされたとき
    void Clicked()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        //　元に戻る
        if (size.x < defoultSizeX && smalledFlag)
        {
            size.x = size.x + size.x * 1 / changeSizeTime * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * Time.unscaledDeltaTime;

            if (size.x > defoultSizeX)
            {
                size.x = defoultSizeX;
                size.y = defoultSizeY;
            }
            GetComponent<RectTransform>().sizeDelta = size;
        }
        //　小さくなる
        else if (size.x > defoultSizeX * clickedSize)
        {
            size.x = size.x - size.x * 1 / clickSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / clickSizeTime * largeSize * Time.unscaledDeltaTime;

            if (size.x <= defoultSizeX * clickedSize)
            {
                size.x = defoultSizeX * clickedSize;
                size.y = defoultSizeY * clickedSize;
                smalledFlag = true;
            }
        }
        GetComponent<RectTransform>().sizeDelta = size;
    }*/

    //　サイズを一瞬で元に戻す
    public void SizeReset()
    {
        var size = GetComponent<RectTransform>().sizeDelta;
        size.x = defoultSizeX;
        size.y = defoultSizeY;
        GetComponent<RectTransform>().sizeDelta = size;
        onFlag = false;
    }

    //　クリックされたとき
    public void OnClick()
    {
        clickFlag = true;
    }

    //　マウスが乗ったとき
    public void On_mouse()
    {
        onFlag = true;
    }

    //　マウスが離れたとき
    public void Exit_mouse()
    {
        onFlag = false;
    }
}

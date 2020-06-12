using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRutton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    StageSelectDirector selectDirector;

    [SerializeField]
    bool left = false;

    [SerializeField]
    int buttonNumber;

    Button a;


    float largeSize = 1.2f;
    float changeSizeTime = 0.2f;
    float defoultSizeX = 0;
    float defoultSizeY = 0;

    public enum Buttons
    {
        large,
        smaller,

        normal,
    }

    Buttons touchedButton = Buttons.normal;

    private void Start()
    {
        defoultSizeX = selectDirector.sctollButtonImage[0].transform.localScale.x;
        defoultSizeY = selectDirector.sctollButtonImage[0].transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (touchedButton)
        {
            case Buttons.large:

                if (ButtonFalling())
                {
                    touchedButton = Buttons.normal;
                }
                break;

            case Buttons.smaller:
                if (NotButtonFalling())
                {
                    touchedButton = Buttons.normal;
                }
                break;
        }
    }

    public void OnClick()
    {
        selectDirector.Sctoll(left);
    }

    bool ButtonFalling()
    {

        Vector3 size = selectDirector.sctollButtonImage[buttonNumber].transform.localScale;
        if (size.x < defoultSizeX * largeSize)
        {

            size.x = size.x + size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            selectDirector.sctollButtonImage[buttonNumber].transform.localScale = size;
        }
        else if (size.x > defoultSizeX * largeSize)
        {
            Debug.Log("a");
            size.x = defoultSizeX * largeSize;
            size.y = defoultSizeY * largeSize;
            selectDirector.sctollButtonImage[buttonNumber].transform.localScale = size;
            return true;
        }
        return false;
    }

    public bool NotButtonFalling()
    {

        var size = selectDirector.sctollButtonImage[buttonNumber].transform.localScale;
        if (size.x > defoultSizeX)
        {
            size.x = size.x - size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            selectDirector.sctollButtonImage[buttonNumber].transform.localScale = size;
        }
        else if (size.x < defoultSizeX)
        {
            size.x = defoultSizeX;
            size.y = defoultSizeY;
            selectDirector.sctollButtonImage[buttonNumber].transform.localScale = size;
            return true;
        }
        return false;
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        touchedButton = Buttons.large;
        //Debug.Log("a");
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    // 
    public void OnPointerExit(PointerEventData eventData)
    {
        touchedButton = Buttons.smaller;
    }
}

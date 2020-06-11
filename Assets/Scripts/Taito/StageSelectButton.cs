using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    int stageNumber = 2;

    [SerializeField]
    StageSelectDirector selectDirector;

    float largeSize = 1.2f;
    float changeSizeTime = 0.2f;
    float defoultSizeX = 0;
    float defoultSizeY = 0;

    public enum Button
    {
        large,
        smaller,

        normal,
    }

    Button touchedButton = Button.normal;

    private void Start()
    {
        defoultSizeX = selectDirector.stageButtons[stageNumber - 2].transform.GetChild(1).transform.localScale.x;
        defoultSizeY = selectDirector.stageButtons[stageNumber - 2].transform.GetChild(1).transform.localScale.y;
    }

    public void OnClick()
    {      
        selectDirector.number = stageNumber;
        selectDirector.sceneTransition = true;
  
    }

    public void Update()
    {
        switch (touchedButton)
        {
            case Button.large:
               
                ButtonFalling(stageNumber - 2);              
                break;

            case Button.smaller:
                if(NotButtonFalling(stageNumber - 2))
                {
                    touchedButton = Button.smaller;              
                }
                break;
        }
    }



    bool ButtonFalling(int buttonNumber)
    {
        if (buttonNumber == selectDirector.stageClearNumber)
        {
             selectDirector.stageButtons[buttonNumber].transform.GetChild(1).Find("ButtonBack").GetComponent<Renderer>().material = selectDirector.SubUnClearButtonColorMterial;
        }
        Debug.Log("a");
        Vector3 size = selectDirector.stageButtons[buttonNumber].transform.GetChild(1).transform.localScale;

        Debug.Log(size.x);
        Debug.Log(defoultSizeX * largeSize);
        if (size.x < defoultSizeX * largeSize)
        {

            size.x = size.x + size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            selectDirector.stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
        }
        else if (size.x > defoultSizeX * largeSize)
        {
            size.x = defoultSizeX * largeSize;
            size.y = defoultSizeY * largeSize;
            selectDirector.stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
            return true;
        }
        return false;
    }



    public bool NotButtonFalling(int buttonNumber)
    {

        var size = selectDirector.stageButtons[buttonNumber].transform.GetChild(1).transform.localScale;
        if (size.x > defoultSizeX)
        {
            size.x = size.x - size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            selectDirector.stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
        }
        else if (size.x < defoultSizeX)
        {
            size.x = defoultSizeX;
            size.y = defoultSizeY;
            if (buttonNumber == selectDirector.stageClearNumber)
            {
                selectDirector.stageButtons[buttonNumber].transform.GetChild(1).Find("ButtonBack").GetComponent<Renderer>().material = selectDirector.unClearButtonColorMterial;                
            }
            selectDirector.stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;

            return true;
        }
        return false;
    }


    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        touchedButton = Button.large;
        //Debug.Log("a");
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    // 
    public void OnPointerExit(PointerEventData eventData)
    {
        touchedButton = Button.smaller;
    }
}

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
    

    public void OnClick()
    {
        selectDirector.number = stageNumber;
        selectDirector.sceneTransition = true;
  
    }


    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectDirector.buttonNumber = stageNumber - 2;
        selectDirector.ButtonEnumChange(true);
        //Debug.Log("a");
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    // 
    public void OnPointerExit(PointerEventData eventData)
    {
        selectDirector.NotButtonFalling(stageNumber - 2);
    }
}

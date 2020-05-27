﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleGenerator : MonoBehaviour
{
    [SerializeField] int maxRippleCount;        // 同時に存在できる波紋の数
    [SerializeField] GameObject ripplePrefab;   // 波紋のプレハブ
    [SerializeField] RippleList rippleList;

    [SerializeField]
    Camera mainCamera;

    Vector3 topLeft;
    Vector3 bomttomLeft;
    Vector3 topRight;
    Vector3 bomttomRight;

    float delta = 0f;
    float span = 1f;

    int remainRippleCount;  // 波紋を生成できる残りの数


    // Start is called before the first frame update
    void Start()
    {
        remainRippleCount = maxRippleCount;

        topLeft = getScreenTopLeft();
        bomttomLeft = getScreenBottomLeft();
        topRight = getSceenTopRight();
        bomttomRight = getScreenBottomRight();

    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;

        if (delta > span)
        {
            delta = 0;
            GenerateRipple();
        }
       
    }

    // 波紋の生成関数
    void GenerateRipple()
    {
        span = Random.Range(0.2f, 0.5f);
        float x = Random.Range(topRight.x - 1, topLeft.x + 1);
        float y = Random.Range(topRight.y - 1, bomttomLeft.y + 1);

        // マウスのワールド座標取得
        Vector2 GeneratePosition = new Vector2 (x,y);
        // 波紋を作成
        GameObject ripple = Instantiate(ripplePrefab,
                                        GeneratePosition,
                                        Quaternion.identity);
        RippleController rippleController = ripple.transform.GetChild(0).GetComponent<RippleController>();
        rippleController.SetCenterPoint(GeneratePosition);
        rippleController.SetRippleGenerator(this);
        rippleList.AddRipple(rippleController);
        
        // 波紋の残りの数を減らす
        remainRippleCount--;
    }

    public void IncreaseRemainRippleCount()
    {
        remainRippleCount++;
    }

    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomLeft()
    {
        Vector3 bomttemLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);

        return bomttemLeft;
    }

    private Vector3 getSceenTopRight()
    {
        Vector3 leftRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        return leftRight;
    }

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
}

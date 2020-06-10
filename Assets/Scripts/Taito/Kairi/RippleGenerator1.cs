using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleGenerator1 : MonoBehaviour
{
    [SerializeField] int maxRippleCount;        // 同時に存在できる波紋の数
    [SerializeField] GameObject ripplePrefab;   // 波紋のプレハブ
    [SerializeField] RippleList1 rippleList;
    [SerializeField] float shortSpan = 0.1f;
    [SerializeField] float longSpan = 0.5f;

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
        topLeft = getScreenTopLeft();
        bomttomLeft = getScreenBottomLeft();
        topRight = getSceenTopRight();
        bomttomRight = getScreenBottomRight();

        span = Random.Range(shortSpan, longSpan);
        float x = Random.Range(topRight.x - 0.5f, topLeft.x + 0.5f);
        float y = Random.Range(topRight.y - 0.5f, bomttomLeft.y + 0.5f);

        // マウスのワールド座標取得
        Vector2 GeneratePosition = new Vector2 (x,y);
        // 波紋を作成
        GameObject ripple = Instantiate(ripplePrefab,
                                        GeneratePosition,
                                        Quaternion.identity);
        RippleController1 rippleController = ripple.transform.GetChild(0).GetComponent<RippleController1>();
        rippleController.SetCenterPoint(GeneratePosition);
        //rippleController.SetRippleGenerator(this);
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

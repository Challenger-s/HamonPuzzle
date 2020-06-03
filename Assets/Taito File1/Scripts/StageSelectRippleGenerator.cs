using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectRippleGenerator : MonoBehaviour
{
    [SerializeField] int maxRippleCount;        // 同時に存在できる波紋の数
    [SerializeField] GameObject ripplePrefab;   // 波紋のプレハブ
    [SerializeField] RippleList1 rippleList;
    StageSelectDirector m_gameDirector;


    int remainRippleCount;  // 波紋を生成できる残りの数


    // Start is called before the first frame update
    void Start()
    {
        remainRippleCount = maxRippleCount;

    }

    // Update is called once per frame
    void Update()
    {
        {
            if (Input.GetMouseButtonDown(0) && remainRippleCount > 0)
            {
                GenerateRipple();
            }
        }

    }

    // 波紋の生成関数
    void GenerateRipple()
    {
        // マウスのワールド座標取得
        Vector2 GeneratePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 波紋を作成
        GameObject ripple = Instantiate(ripplePrefab,
                                        GeneratePosition,
                                        Quaternion.identity);
        RippleController1 rippleController = ripple.transform.GetChild(0).GetComponent<RippleController1>();
        rippleController.SetCenterPoint(GeneratePosition);

        ripple.transform.parent = Camera.main.transform;
        //rippleController.SetRippleGenerator(this);
        rippleList.AddRipple(rippleController);

        // 波紋の残りの数を減らす
        remainRippleCount--;
    }

    public void IncreaseRemainRippleCount()
    {
        remainRippleCount++;
    }

}

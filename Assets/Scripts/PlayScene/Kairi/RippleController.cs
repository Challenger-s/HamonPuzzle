using System;
using UnityEngine;

public class RippleController : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer = null; // 円を描画するための LineRenderer
    [SerializeField] private float m_radius = 0;    // 円の半径

    GameDirector m_gameDirector;
    
    // staticを付ける/別のスクリプトへ隔離
    [SerializeField] private float m_lineWidth = 0;    // 円の線の太さ
    [SerializeField] private float m_colliderWidth = 0;    // 円の当たり判定のの太さ
    [SerializeField] private float m_speed; // 波紋の速度

    //　ステージの大きさを取得
    [SerializeField] Vector2 stageSize;

    [SerializeField] WallList m_wallList;

    private float m_elapedTime = 0;
    private Vector2 m_centerPoint;
    private float m_scale = 0;

    RippleGenerator rippleGenerator;
    RippleList rippleList;
    RippleList resonanceRippleList;

    Vector2 targetPoint;                    //　波紋がこの地点を通過したら消える


    //　画面遷移時に実行
    private void Start()
    {
        InitLineRenderer();
        m_gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        rippleList = GameObject.Find("RippleGenerator ").GetComponent<RippleList>();
        resonanceRippleList = GameObject.Find("ResonanceRippleList").GetComponent<RippleList>();
    }


    //　常に実行
    private void Update()
    {
        //　波紋が常に広がる処理
        if(m_gameDirector.m_phase == GameDirector.Phase.Play)
        {
            m_elapedTime += Time.deltaTime;

            m_scale = m_elapedTime * m_speed;

            transform.localScale = new Vector3(m_scale, m_scale, 1);

            MeasureTargetPoint();
        }
    }


    //　波紋の初期状態を準備する
    private void InitLineRenderer()
    {
        var segments = 360;

        m_lineRenderer.startWidth = m_lineWidth;
        m_lineRenderer.endWidth = m_lineWidth;
        m_lineRenderer.positionCount = segments;
        m_lineRenderer.loop = true;
        m_lineRenderer.useWorldSpace = false; // transform.localScale を適用するため

        var points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            var x = Mathf.Sin(rad) * m_radius;
            var y = Mathf.Cos(rad) * m_radius;
            points[i] = new Vector3(　x,　y, 0);
        }

        m_lineRenderer.SetPositions(points);
    }

    //　TargetPoint と波紋の距離を計測する
    void MeasureTargetPoint()
    {
        Vector2 rippleCenterPoint = GetRippleCenterPoint();
        float rippleSize = GetRippleSize();

        // 画面の端から波紋の中心点への直線距離の取得
        float distanceFromRipple = Mathf.Abs(Mathf.Sqrt((this.targetPoint.x - rippleCenterPoint.x) *
                                              (this.targetPoint.x - rippleCenterPoint.x) +
                                              (this.targetPoint.y - rippleCenterPoint.y) *
                                              (this.targetPoint.y - rippleCenterPoint.y)));

        float distanceFromTargetPoint_scaleCalculated_outer = (rippleSize - GetRippleColliderWidth()) - distanceFromRipple;

        // 波紋が通過したら波紋を消す処理
        if (distanceFromTargetPoint_scaleCalculated_outer > 0)
        {
            DestroyRipple();
        }
    }

    //　波紋を消す関数
    virtual protected void DestroyRipple()
    {
        //　プレイヤーが起こした波紋なら回数を復活させる
        if (this.gameObject.transform.parent.tag == "Ripple")
        {
            rippleGenerator.IncreaseRemainRippleCount();
        }
        rippleList.RemoveRipple(GetComponent<RippleController>());  //　リップルリストから取り除く
        Destroy(gameObject.transform.parent.gameObject);            // 波紋を消す処理
    }

    //　右クリック時に呼ばれる
    public void Restart()
    {
        //　プレイヤーが起こした波紋なら
        if (this.gameObject.transform.parent.tag == "Ripple")
        {
            rippleList.RemoveRipple(GetComponent<RippleController>());  //　リップルリストから取り除く
        }
        //　共鳴が起こした波紋なら
        else if (this.gameObject.transform.parent.tag == "ResonanceRipple")
        {
            resonanceRippleList.RemoveRipple(GetComponent<RippleController>());  //　共鳴のリップルリストから取り除く
        }
        Destroy(gameObject.transform.parent.gameObject);            // 波紋を消す処理
    }


    public void SetRippleGenerator(RippleGenerator rippleGenerator)
    {
        this.rippleGenerator = rippleGenerator;
    }

    // m_centerPointを設定し、波紋が通過するところを決める関数
    public void SetCenterPoint(Vector2 centerPoint)
    {
        m_centerPoint = centerPoint;
        //　通過したい場所のX座標を決める
        if (this.m_centerPoint.x > 0)
        {
            targetPoint.x = -stageSize.x;
        }
        else
        {
            targetPoint.x = stageSize.x;
        }
        //　通過したい場所のY座標を決める
        if (this.m_centerPoint.y > 0)
        {
            targetPoint.y = -stageSize.y;
        }
        else
        {
            targetPoint.y = stageSize.y;
        }
    }


    // 波紋の大きさを取得する関数
    public float GetRippleSize()
    {
        return m_scale;
    }

    // 波紋の中心点の座標を取得する関数
    public Vector2 GetRippleCenterPoint()
    {
        return m_centerPoint;
    }

    public float GetRippleColliderWidth()
    {
        return m_colliderWidth;
    }

}
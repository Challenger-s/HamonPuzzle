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

    [SerializeField] WallList m_wallList;

    private float m_elapedTime = 0;
    private Vector2 m_centerPoint;
    private float m_scale = 0;

    RippleGenerator rippleGenerator;
    
    private void Start()
    {
        InitLineRenderer();
        m_gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    private void Update()
    {
        if(m_gameDirector.m_phase == GameDirector.Phase.Play)
        {
            m_elapedTime += Time.deltaTime;

            m_scale = m_elapedTime * m_speed;

            transform.localScale = new Vector3(m_scale, m_scale, 1);
        }

    }

    void WallHitCheck()
    {
        int wallcount = m_wallList.GetWallCount();

        for(int i =0;i < wallcount; i++)
        {

        }
    }

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

    virtual protected void DestroyRipple()
    {
        // 波紋を消す処理

        rippleGenerator.IncreaseRemainRippleCount();
    }

    public void SetRippleGenerator(RippleGenerator rippleGenerator)
    {
        this.rippleGenerator = rippleGenerator;
    }

    // m_centerPointを設定する関数
    public void SetCenterPoint(Vector2 centerPoint)
    {
        m_centerPoint = centerPoint;
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
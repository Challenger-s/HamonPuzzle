
using System.Collections;
using UnityEngine;

public sealed class RippleController1 : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer = null; // 円を描画するための LineRenderer
    [SerializeField] private float m_radius = 0;    // 円の半径



    // staticを付ける/別のスクリプトへ隔離
    [SerializeField] private float m_lineWidth = 0;    // 円の線の太さ
    [SerializeField] private float m_colliderWidth = 0;    // 円の当たり判定のの太さ
    [SerializeField] private float m_speed; // 波紋の速度

    [SerializeField]
    GameObject rippleParent;

    [SerializeField]
    private float m_elapedTime = 0;

    private Vector2 m_centerPoint;
    private float m_scale = 0;

    RippleGenerator1 rippleGenerator;

    float maxSize = 0;

    

    private void Reset()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        InitLineRenderer();
        maxSize = Random.Range(2.0f, 4.0f);
    }

    private void Update()
    {

        
        m_elapedTime += Time.deltaTime;

        m_scale = m_elapedTime * m_speed;

        transform.localScale = new Vector3(m_scale, m_scale, 1);

        if(GetRippleSize() > maxSize)
        {
            Destroy(rippleParent);

        }else if(GetRippleSize() > maxSize - 1)
        {
            GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r,
                                                               GetComponent<Renderer>().material.color.g,
                                                               GetComponent<Renderer>().material.color.b, 
                                                               GetComponent<Renderer>().material.color.a - (1f * Time.deltaTime));
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

    void DestroyRipple()
    {
        // 波紋を消す処理

        rippleGenerator.IncreaseRemainRippleCount();
    }

    public void SetRippleGenerator(RippleGenerator1 rippleGenerator)
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
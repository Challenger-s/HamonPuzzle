using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleLineController : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer = null; // 円を描画するための LineRenderer
    [SerializeField] private float m_radius = 0;    // 円の半径
    [SerializeField] private float m_lineWidth = 0;    // 円の線の太さ

    void GenerateRippleLine(int startAngle,int endAngle)
    {
        var segments = 360;

        m_lineRenderer.startWidth = m_lineWidth;
        m_lineRenderer.endWidth = m_lineWidth;
        m_lineRenderer.positionCount = segments;
        m_lineRenderer.loop = true;
        m_lineRenderer.useWorldSpace = false; // transform.localScale を適用するため


        var points = new Vector3[segments];

        for (int i = startAngle; i < endAngle; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            var x = Mathf.Sin(rad) * m_radius;
            var y = Mathf.Cos(rad) * m_radius;
            points[i] = new Vector3(x, y, 0);
        }

        m_lineRenderer.SetPositions(points);

    }
}

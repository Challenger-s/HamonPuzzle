using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonancePointController : MonoBehaviour
{
    [SerializeField] RippleList m_rippleList;
    [SerializeField] RippleGenerator m_rippleGenerator;
    [SerializeField] float ColliderSize;

    List<bool> m_ripplesIsHittedList = new List<bool>();

    private void Update()
    {
        HitCheck();
    }

    void HitCheck()
    {
        int rippleCount = m_rippleList.GetRippleCount();
        for (int i = 0; i < rippleCount; i++)
        {
            if (m_ripplesIsHittedList[i] == true) { continue; }

            RippleController rippleController = m_rippleList.GetRippleController(i);
            Vector2 rippleCenterPoint = rippleController.GetRippleCenterPoint();
            float rippleSize = rippleController.GetRippleSize();

            // 共鳴の中心点から波紋の中心点への直線距離の取得
            float distanceFromRipple = Mathf.Abs(Mathf.Sqrt((this.transform.position.x - rippleCenterPoint.x) *
                                                  (this.transform.position.x - rippleCenterPoint.x) +
                                                  (this.transform.position.y - rippleCenterPoint.y) *
                                                  (this.transform.position.y - rippleCenterPoint.y)));

            float distanceFromRipple_scaleCalculated_inner = distanceFromRipple - rippleSize + rippleController.GetRippleColliderWidth();
            float distanceFromRipple_scaleCalculated_outer = rippleSize - rippleController.GetRippleColliderWidth() - distanceFromRipple;

            //float distanceFromRipple_scaleCalculated_inner = (distanceFromRipple - this.transform.localScale.x / 2) - (rippleSize + rippleController.GetRippleColliderWidth());
            //float distanceFromRipple_scaleCalculated_outer = (rippleSize - rippleController.GetRippleColliderWidth()) - (distanceFromRipple + this.transform.localScale.x / 2);


            Debug.Log(i.ToString());
            // 円の当たり判定の応用
            if (distanceFromRipple_scaleCalculated_inner < ColliderSize && distanceFromRipple_scaleCalculated_outer < ColliderSize)
            {
                m_ripplesIsHittedList[i] = true;
                m_rippleGenerator.GenerateResonanceRipple(this.transform.position);
            }

        }

    }

    public void Add_ripplesIsHittedList()
    {
        bool b = false;
        m_ripplesIsHittedList.Add(b);
    }

    public void Remove_ripplesIsHittedList(int index)
    {
        m_ripplesIsHittedList.Remove(m_ripplesIsHittedList[index]);
    }
}

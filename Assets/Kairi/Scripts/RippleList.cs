using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleList : MonoBehaviour
{
    [SerializeField] ResonancePointList resonancePointList;
    List<RippleController> m_rippleList = new List<RippleController>();
    int m_rippleCount = 0;

    List<RippleController> m_resonanceRippleList = new List<RippleController>();
    

    public void AddRipple(RippleController ripple)
    {
        m_rippleList.Add(ripple);
        for(int i = 0;i < resonancePointList.GetListSize();i++)
        {
            resonancePointList[i].Add_ripplesIsHittedList();
        }

        m_rippleCount++;
    }

    public void RemoveRipple(RippleController ripple)
    {
        int index = m_rippleList.IndexOf(ripple);
        m_rippleList.Remove(ripple);
        for (int i = 0; i < resonancePointList.GetListSize(); i++)
        {
            resonancePointList[i].Remove_ripplesIsHittedList(index);
        }

        m_rippleCount--;
    }

    public int GetRippleCount()
    {
        return m_rippleCount;
    }

    public RippleController GetRippleController(int index)
    {
        return m_rippleList[index];
    }
}

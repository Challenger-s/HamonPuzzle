using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleList : MonoBehaviour
{
    List<RippleController> m_rippleList = new List<RippleController>();
    int m_rippleCount = 0;
    
    public void AddRipple(RippleController ripple)
    {
        m_rippleList.Add(ripple);
        m_rippleCount++;
    }

    public void RemoveRipple(RippleController ripple)
    {
        m_rippleList.Remove(ripple);
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

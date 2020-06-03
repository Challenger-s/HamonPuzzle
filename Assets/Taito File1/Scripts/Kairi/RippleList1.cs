using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleList1 : MonoBehaviour
{
    List<RippleController1> m_rippleList = new List<RippleController1>();
    int m_rippleCount = 0;
    
    public void AddRipple(RippleController1 ripple)
    {
        m_rippleList.Add(ripple);
        m_rippleCount++;
    }

    public void RemoveRipple(RippleController1 ripple)
    {
        m_rippleList.Remove(ripple);
        m_rippleCount--;
    }

    public int GetRippleCount()
    {
        return m_rippleCount;
    }

    public RippleController1 GetRippleController(int index)
    {
        return m_rippleList[index];
    }
}

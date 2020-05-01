using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleList : MonoBehaviour
{
    List<RippleController> m_rippleList = new List<RippleController>();
    RippleListObserver rippleListObserver;
    int m_rippleCount = 0;

    private void Start()
    {
        rippleListObserver = GetComponent<RippleListObserver>();
    }

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

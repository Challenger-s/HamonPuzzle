using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] FitzoneController[] m_fitzoneArray;

    public enum Phase
    {
        PreStart,
        Play,
        Pause,
        Clear,
    }

    public Phase m_phase;

    // Start is called before the first frame update
    void Start()
    {
        m_phase = Phase.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClearCheck())
        {
            Clear();
        }
        
        
    }

    bool ClearCheck()
    {
        bool isCleared = true;

        for (int i = 0; i < m_fitzoneArray.Length; i++)
        {
            if (m_fitzoneArray[i].GetCount() != 0) { isCleared = false; }
        }

        return isCleared;
    }

    void Clear()
    {
        Debug.Log("Clear!");
        m_phase = Phase.Clear;

    }

    
}

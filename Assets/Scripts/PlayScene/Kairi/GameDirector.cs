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

    }

    // Update is called once per frame
    void Update()
    {
        switch (m_phase)
        {
            case Phase.Play:

                if (ClearCheck())
                {
                    Clear();
                    break;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    Restart();
                }
                break;

        }


    }

    void Restart()
    {

    }

    bool ClearCheck()
    {
        for (int i = 0; i < m_fitzoneArray.Length; i++)
        {
            if (m_fitzoneArray[i].GetCount() != 0) { return false; }
        }

        return true;
    }

    void Clear()
    {
        m_phase = Phase.Clear;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitzoneList : MonoBehaviour
{
    [SerializeField] List<FitzoneController> m_fitzoneList = new List<FitzoneController>();

    public int GetFitzoneCount()
    {
        return m_fitzoneList.Count;
    }


}

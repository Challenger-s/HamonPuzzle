using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonancePointList : MonoBehaviour
{
    [SerializeField] List<ResonancePointController> resonancePointList;

    public ResonancePointController this[int i]
    {
        get { return resonancePointList[i]; }
    }

    public int GetListSize()
    {
        return resonancePointList.Count;
    }
}

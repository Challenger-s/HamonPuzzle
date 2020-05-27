using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallList : MonoBehaviour
{
    List<GameObject> m_wallList = new List<GameObject>();

    public GameObject GetWallObject(int index)
    {
        return m_wallList[index];
    }

    public int GetWallCount()
    {
        return m_wallList.Count;
    }
}

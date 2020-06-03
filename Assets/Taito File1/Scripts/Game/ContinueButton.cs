using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField]
    GameD gameD;


    public void OnClck()
    {
        gameD.Continue();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] GameD gameD;
    [SerializeField] PoseButton poseButton;

    public void OnClck()
    {
        poseButton.SizeReset();
        gameD.Continue();
    }
}

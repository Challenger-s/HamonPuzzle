using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] GameD gameD;
    [SerializeField] PoseButton poseButton;

    AudioSource[] audioSource; //オーディオソース使用

    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
    }

    public void OnClck()
    {
        audioSource[0].Play(); //音

        poseButton.SizeReset();
        gameD.Continue();
    }
}

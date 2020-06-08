using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    GameD gameD;

    AudioSource[] audioSource; //オーディオソース使用

    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
    }

    public void OnClck()
    {
        audioSource[0].Play(); //音

        gameD.menu = true;
    }
}

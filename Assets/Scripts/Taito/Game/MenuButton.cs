using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    GameD gameD;
<<<<<<< HEAD

    // Start is called before the first frame update
    void Start()
    {
        
    }
=======
>>>>>>> Prog_Jin

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

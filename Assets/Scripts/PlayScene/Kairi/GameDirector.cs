using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] FitzoneController[] m_fitzoneArray;

    bool stageClearFlag = false; //ステージをクリアしたかどうかの判定フラグ
    float musicCountUpTimer = 0; //クリア後、白円音が鳴るまでの間

    AudioSource[] audioSource; //オーディオソース使用（３つ）

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
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
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

        if (this.stageClearFlag == true) //クリアフラグがオンになったら
        {
            this.musicCountUpTimer += Time.deltaTime; //カウント加算
            if(this.musicCountUpTimer > 1) //カウントタイマーが１より大きくなったとき
            {
                audioSource[1].Play(); //1番目の音を鳴らす
                this.stageClearFlag = false; //クリアフラグオフ
            }
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

        audioSource[0].Play(); //0番目の音を鳴らす

        this.stageClearFlag = true; //フラグオン
    }
}

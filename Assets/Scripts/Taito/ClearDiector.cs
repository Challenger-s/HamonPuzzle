using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearDiector : MonoBehaviour
{
    [SerializeField] GameObject StageClearText;
    [SerializeField] GameObject ClearRippleGenerator;
    [SerializeField] GameObject Forward;
    [SerializeField] Image MostForwardImage;
    [SerializeField] Camera mainCamera;
    [SerializeField] Material RippleTexture;
    

    [SerializeField] float fadeInTime = 1.5f;
    [SerializeField] float displayTime = 2f;
    [SerializeField] float fadeInTime2 = 1.5f;

    GameDirector gameDirector;

    float span = 0;
    float delta = 0;

    GameObject[] fitZones;

    bool clear = true;
    bool a = false;
    bool b = false;
    bool c = false;

    bool forwardFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        StageClearText.SetActive(false);
        Forward.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 0);
        //RippleTexture.color = new Color32(255, 255, 255, 120);
        fitZones = GameObject.FindGameObjectsWithTag("FitZone");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameDirector.m_phase == GameDirector.Phase.Clear)
        {
            if (!forwardFlag)
            {
                forwardFlag = true;
                Forward.SetActive(true);
            }
            clearProduction();
        }
    }

    void clearProduction()
    {
        if (c)
        {
            //Debug.Log("c");
            MostForwardImage.color = new Color(255, 255, 255, MostForwardImage.color.a + (1 / fadeInTime2 * Time.deltaTime));
            if(MostForwardImage.color.a < 0)
            {
                clear = false;
            }
        }

        if (b)
        {
            //ForwardImage.color = new Color(255, 255, 255, ForwardImage.color.a - (0.5f * Time.deltaTime));

            /*if (ForwardImage.color.a < 0)
            {
                b = false;
                c = true;
            }*/

            displayTime -= Time.deltaTime;
            if (displayTime < 0)
            {
                b = false;
                c = true;
            }
        }

        if (a)
        {
            a = false;
            //ForwardImage.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 1f);
            mainCamera.backgroundColor = new Color(1, 1, 1, 1);
            //Destroy(BackImage);
            //RippleTexture.color = new Color32(0, 255,255,120);
            StageClearText.SetActive(true);
            ClearRippleGenerator.SetActive(true);
            b = true;
        }

        if (Forward.GetComponent<Renderer>().material.color.a < 1)
        {
            //Debug.Log(BackImage.color.a + (0.3f * Time.deltaTime));
            Forward.GetComponent<Renderer>().material.color = 
                new Color(1, 1, 1, Forward.GetComponent<Renderer>().material.color.a + (1 / fadeInTime * Time.deltaTime));

            if (Forward.GetComponent<Renderer>().material.color.a >= 0.9f)
            {
                for (int i = 0; i < fitZones.Length; ++i)
                {
                    Destroy(fitZones[i]);
                }
                a = true;
            }
        }
    }
}

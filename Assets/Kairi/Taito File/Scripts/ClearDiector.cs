using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearDiector : MonoBehaviour
{
    [SerializeField]
    GameObject StageClearText;

    [SerializeField]
    Image BackImage;

    [SerializeField]
    Image ForwardImage;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Material RippleTexture;

    float span = 0;
    float delta = 0;

    GameObject[] fitZones;

    bool clear = true;
    bool a = false;
    bool b = false;
    bool c = false;



    // Start is called before the first frame update
    void Start()
    {
        StageClearText.SetActive(false);
        BackImage.color = new Color(255, 255, 255, 0);
        ForwardImage.color = new Color(255, 255, 255, 0);
        RippleTexture.color = new Color32(255, 255, 255, 120);
        fitZones = GameObject.FindGameObjectsWithTag("FitZone");
    }

    // Update is called once per frame
    void Update()
    {
        if (clear)
        {
            clearProduction();
        }
    }

    void clearProduction()
    {
        if (c)
        {

            ForwardImage.color = new Color(255, 255, 255, ForwardImage.color.a + (0.7f * Time.deltaTime));
            if(ForwardImage.color.a < 0)
            {
                clear = false;
            }

        }

        if (b)
        {

            ForwardImage.color = new Color(255, 255, 255, ForwardImage.color.a - (0.5f * Time.deltaTime));
            if (ForwardImage.color.a < 0)
            {
                b = false;
                c = true;
            }
        }

        if (a)
        {
            a = false;
            ForwardImage.color = new Color(255, 255, 255, 1);
            mainCamera.backgroundColor = new Color(1, 1, 1, 1);
            Destroy(BackImage);
            RippleTexture.color = new Color32(0, 255,255,120);
            StageClearText.SetActive(true);
            b = true;

        }

        if (BackImage.color.a < 1)
        {
            BackImage.color = new Color(255, 255, 255, BackImage.color.a + (0.3f * Time.deltaTime));
            if (BackImage.color.a > 0.9f)
            {
                for(int i = 0; i < fitZones.Length; ++i)
                {
                    Destroy(fitZones[i]);
                }
                a = true;
            }

        }
    }
}

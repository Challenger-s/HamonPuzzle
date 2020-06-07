using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameD : MonoBehaviour
{
    [SerializeField] GameObject[] pauseObjects;
    [SerializeField] Image forwardImage;
    [SerializeField] RippleGenerator rippleGenerator;
    [SerializeField] GameDirector gameDirector;
    [SerializeField] float fadeSpeed = 0.5f;

    public bool menu = false;

    bool poseDisplay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menu)
        {
            Menu();
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) && gameDirector.ReturnPhase())
        {            
            if (! poseDisplay)
            {
                poseDisplay = true;
                this.rippleGenerator.ChangePoseFlag(poseDisplay);
                for (int i = 0; i < pauseObjects.Length; i++)
                {
                    Time.timeScale = 0f;
                    pauseObjects[i].SetActive(true);
                }
                Debug.Log("pose");
            }
            else
            {
                poseDisplay = false;
                this.rippleGenerator.ChangePoseFlag(poseDisplay);
                Continue();
            }
        }
    }

    public void Continue()
    {
        for(int i = 0; i < pauseObjects.Length; i++)
        {
            poseDisplay = false;
            this.rippleGenerator.ChangePoseFlag(poseDisplay);
            pauseObjects[i].SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Menu()
    {
        if (FadeOut(forwardImage))
        {
            SceneManager.LoadScene(1);
            menu = false;
        }
    }

    bool FadeIn(Image image)
    {
        image.color = new Color(255, 255, 255, image.color.a - (fadeSpeed * Time.unscaledDeltaTime));

        if (image.color.a < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    bool FadeOut(Image image)
    {
        image.color = new Color(255, 255, 255, image.color.a + (fadeSpeed * Time.unscaledDeltaTime));

        if (image.color.a >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //　poseDisplayを返す
    public bool ReturnPoseDisplay()
    {
        return poseDisplay;
    }
}

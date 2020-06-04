using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameD : MonoBehaviour
{
    [SerializeField]
    GameObject[] pauseObjects;

    [SerializeField]
    Image forwardImage;

    [SerializeField] RippleGenerator rippleGenerator;

    public bool menu = false;

    bool poseDisplay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
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
            }
            else
            {
                poseDisplay = false;
                this.rippleGenerator.ChangePoseFlag(poseDisplay);
                Continue();
            }
        }

        if (menu)
        {
            Menu();
        }
    }

    public void Continue()
    {
        for(int i = 0; i < pauseObjects.Length; i++)
        {
            pauseObjects[i].SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Menu()
    {
        pauseObjects[0].SetActive(false);
        pauseObjects[1].SetActive(false);

        if (FadeOut(forwardImage) == true)
        {
            SceneManager.LoadScene(1);
            menu = false;
        }
    }

    bool FadeIn(Image image)
    {
        image.color = new Color(255, 255, 255, image.color.a - (0.3f * Time.deltaTime));

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
        image.color = new Color(255, 255, 255, image.color.a + (0.3f * Time.deltaTime));

        if (image.color.a > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

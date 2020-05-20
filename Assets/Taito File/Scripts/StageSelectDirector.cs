using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectDirector : MonoBehaviour
{
    [SerializeField]
    float backGroundMoveSpeed = 1;

    [SerializeField]
    int stageClearNumber = 1;

    [SerializeField]
    GameObject a;

    [SerializeField]
    GameObject b;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Image BackGround;

    [SerializeField]
    Image forwardImage;

    [SerializeField]
    Camera camera;

    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject clearButtonColor;

    [SerializeField]
    GameObject parentClearButtonColor;

    [SerializeField]
    GameObject unClearButtonColor;

    [SerializeField]
    GameObject[] stageButtons;


    [SerializeField]
    SpriteRenderer ac;

    float unclearImageWidth = 0;

    float c;

    int currentStage;

    bool fadeOut = false;
    bool playable = false;

    bool sctollL = false;
    bool sctollR = false;

    bool playebleA = false;
    bool playebleB = false;
    int number = 0;

    float screenSizeX = 0;


    float delta = 0;
    float span = 0.5f;

    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;

    Vector3 screenPos;

    Vector3 BackGroundColor;

    // Start is called before the first frame update
    void Start()
    {
        stageClearNumber = PlayerPrefs.GetInt("StageClear", 0);

        BackGround = new Color(BackGround.color.r, BackGround.color.g, BackGround.color.b, BackGround.color.a);

        c = (a.transform.position.x - b.transform.position.x) / 2;

        screenSizeX = ScreenSizeX();
        screenPos = mainCamera.transform.position;

        camera.orthographicSize = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(stageClearNumber > currentStage)
        {
            StageAddition();
        }

        if (sctollL)
        {
            if (mainCamera.transform.position.x > (screenPos.x - screenSizeX))
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 20f * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3(screenPos.x - screenSizeX, mainCamera.transform.position.y, mainCamera.transform.position.z);
                sctollL = false;
            }
        }


        if (sctollR)
        {
            if (mainCamera.transform.position.x < screenPos.x + screenSizeX)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 20f * Time.deltaTime, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3(screenPos.x + screenSizeX, mainCamera.transform.position.y, mainCamera.transform.position.z);
                sctollR = false;
            }
        }


        if (playable)
        {
            if (playebleA)
            {
                if (stageButtons[currentStage - 1].transform.localScale.x < 0.22)
                {
                    stageButtons[currentStage - 1].transform.localScale = new Vector3(stageButtons[currentStage - 1].transform.localScale.x + 0.01f + Time.deltaTime, stageButtons[currentStage - 1].transform.localScale.y + 0.01f + Time.deltaTime, 0);
                }
                else if (stageButtons[currentStage - 1].transform.localScale.x > 0.22)
                {
                    delta += Time.deltaTime;
                    if (delta > span)
                    {
                        delta = 0;
                        playebleA = false;
                        playebleB = true;
                    }                  
                }
            }
            
            if (playebleB)
            {
                if (stageButtons[currentStage - 1].transform.localScale.x > 0.2)
                {
                    stageButtons[currentStage - 1].transform.localScale = new Vector3(stageButtons[currentStage - 1].transform.localScale.x - 0.01f + Time.deltaTime, stageButtons[currentStage - 1].transform.localScale.y - 0.01f + Time.deltaTime, 0);
                }
                else if (stageButtons[currentStage - 1].transform.localScale.x > 0.2)
                {
                    playebleB = false;
                    playable = false;
                }
            }
        
        }

        if (fadeOut)
        {
            forwardImage.color = new Color(255, 255, 255, forwardImage.color.a + (0.3f * Time.deltaTime));

            camera.orthographicSize = camera.orthographicSize - (1f * Time.deltaTime);

            if (forwardImage.color.a > 0.9f)
            {
                SceneManager.LoadScene(number);
            }
        }
    }

    void StageAddition()
    {


        if (clearButtonColor.transform.position.x + (ac.bounds.size.x / 2f) < a.transform.position.x + (3.5f / 2) + ((stageClearNumber - 1) * 3.5f)) 
        {
            parentClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, parentClearButtonColor.transform.localScale.y, 0);
 
        }
        else
        {
            currentStage = stageClearNumber;
            BackGround.color
            playable = true;
            playebleA = true;
        }

        /*
         *   Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, unclearBackground.rectTransform.position);


        //スクリーン座標→ワールド座標に変換
        RectTransformUtility.ScreenPointToWorldPointInRectangle(unclearBackground.rectTransform, screenPos, canvas.worldCamera, out result);

        if (result.x < (result2.x) + ((3.5f / 2f) * stageClearNumber))
        {
           unclearBackground.rectTransform.sizeDelta = new Vector2(unclearBackground.rectTransform.sizeDelta.x - (backGroundMoveSpeed * 0.84f) * Time.deltaTime, unclearBackground.rectTransform.sizeDelta.y);
            unclearBackground.rectTransform.position = new Vector3(unclearBackground.rectTransform.position.x + backGroundMoveSpeed * Time.deltaTime, 0, 0);

            unClearButtonColor.transform.position = new Vector3(unClearButtonColor.transform.position.x + 1f * Time.deltaTime, unClearButtonColor.transform.position.y, 0);

            if(!(stageClearNumber == 0))
            {
                unClearButtonColor.transform.localScale = new Vector3(unClearButtonColor.transform.localScale.x - 0.6f * Time.deltaTime, unClearButtonColor.transform.localScale.y, 0);

                //unClearButtonColor.transform.localScale = new Vector3(unClearButtonColor.transform.localScale.x + 0.6f * Time.deltaTime, unClearButtonColor.transform.localScale.y, 0);
            }
            else
            {
                unClearButtonColor.transform.localScale = new Vector3(unClearButtonColor.transform.localScale.x - 0.5f * Time.deltaTime, unClearButtonColor.transform.localScale.y, 0);
            }

            clearButtonColor.transform.localScale = new Vector3(clearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, clearButtonColor.transform.localScale.y, 0);


        }
        else
        {
            currentStage = stageClearNumber;
            playable = true;
            playebleA = true;
        }
        */
    }

    public void Sctoll(bool left)
    {
        screenSizeX = ScreenSizeX();
        screenPos = mainCamera.transform.position;

        if (left)
        {
            sctollL = true;
        }
        else
        {
            sctollR = true;
        }
    }



    public void ButtonFalling(int buttonNumber)
    {
        stageButtons[buttonNumber].transform.localScale = new Vector3(0.22f, 0.22f, 0);
    }

    public void NotButtonFalling(int buttonNumber)
    {
        stageButtons[buttonNumber].transform.localScale = new Vector3(0.2f, 0.2f, 0);
    }


    public void FadeOut(int stageNumber)
    {
        number = stageNumber;

        if (number > 0)
        {
             fadeOut =  true;
        }
        else
        {
            fadeOut = false;
        }
    }

    private float ScreenSizeX()
    {
        Vector3 topLeft = camera.ScreenToWorldPoint(Vector3.zero);
        topLeft.Scale(new Vector3(1f, -1f, 1f));

        Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        float screenSizeX = Mathf.Sqrt(Mathf.Pow(topLeft.x - topRight.x, 2));
 
        return screenSizeX;
    }


    void NextGame()
    {
        PlayerPrefs.SetInt("StageClear", stageClearNumber);
        PlayerPrefs.Save();
    }


}

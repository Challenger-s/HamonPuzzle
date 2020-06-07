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
    Image[] BackGround;

    [SerializeField]
    Color startColor;

    [SerializeField]
    Color endColor;

    [Range(0f, 1f)]
    public float t;

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
    GameObject parentUnClearButtonColor;

    [SerializeField]
    GameObject[] stageButtons;

    [SerializeField]
    GameObject[] buttons;

    [SerializeField]
    SpriteRenderer ac;

    [SerializeField]
    SpriteRenderer unClearColor;

    [SerializeField]
    SpriteRenderer stageBuuttonSizse;

    float unclearImageWidth = 0;

    float c;

    int currentStage;
    int backGuroundNumber = 0;

    public bool sceneTransition = false;
    bool fadeIN = false;

    bool playable = false;

    bool sctollL = false;
    bool sctollR = false;

    bool newStage = false;

    public int number = 0;

    float screenSizeX = 0;


    float delta = 0;
    float span = 0.2f;

    Vector3 result = Vector3.zero;
    Vector3 result2 = Vector3.zero;

    Vector3 screenPos;

    enum Button
    {
        large,
        smaller,

        normal,
    }

    Button button = Button.normal;


    // Start is called before the first frame update
    void Start()
    {
        stageClearNumber = PlayerPrefs.GetInt("StageClear", 0);

        //c = (a.transform.position.x - b.transform.position.x) / 2;

        screenSizeX = ScreenSizeX();
        screenPos = mainCamera.transform.position;

        camera.orthographicSize = 5f;

        BackGround[0].color = Color.Lerp(startColor, endColor, t);

        forwardImage.color = new Color(1, 1, 1, 1);
        fadeIN = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIN)
        {
            if (FadeIn(forwardImage))
            {
                fadeIN = false;
            }
        }

        if (stageClearNumber > currentStage)
        {
            
            if (newStage)
            {
                NewStage();
            }
            else
            {
                StageAddition();
            }
        }

        if (sctollL)
        {
            if (mainCamera.transform.position.x > (screenPos.x - screenSizeX))
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 20f * Time.deltaTime,
                    mainCamera.transform.position.y,
                    mainCamera.transform.position.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3(screenPos.x - screenSizeX,
                    mainCamera.transform.position.y,
                    mainCamera.transform.position.z);
                sctollL = false;
                backGuroundNumber--;
                ButtonOff(true);
            }
        }


        if (sctollR )
        {
            if (mainCamera.transform.position.x < screenPos.x + screenSizeX)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 20f * Time.deltaTime,
                    mainCamera.transform.position.y,
                    mainCamera.transform.position.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3(screenPos.x + screenSizeX,
                    mainCamera.transform.position.y, 
                    mainCamera.transform.position.z);
                sctollR = false;
                backGuroundNumber++;
                ButtonOff(true);
            }
        }


       switch (button)
       {
            case Button.large:

                //stageButtons[currentStage - 1].transform.localScale = new Vector3(stageButtons[currentStage - 1].transform.localScale.x + 0.01f + Time.deltaTime, stageButtons[currentStage - 1].transform.localScale.y + 0.01f + Time.deltaTime, 0);
                ButtonFalling(stageClearNumber - 1);

                delta += Time.deltaTime;
                if(delta > span)
                {
                    delta = 0;
                    button = Button.smaller;
                }              
                break;

            case Button.smaller:

                //stageButtons[currentStage - 1].transform.localScale = new Vector3(stageButtons[currentStage - 1].transform.localScale.x - 0.01f + Time.deltaTime, stageButtons[currentStage - 1].transform.localScale.y - 0.01f + Time.deltaTime, 0);
                NotButtonFalling(stageClearNumber - 1);
   
                button = Button.normal;              
                break;
       }


        if (sceneTransition)
        {
            SceneTransition();
        }
   
    }

    void StageAddition()
    {
       /* if (clearButtonColor.transform.position.x + (ac.bounds.size.x / 2f) < stageButtons[stageClearNumber - 1].transform.position ) 
        {
            parentClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, 
                parentClearButtonColor.transform.localScale.y, 
                0);

            parentUnClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x +1.15f * Time.deltaTime,
                parentClearButtonColor.transform.localScale.y, 
                0);
        }
        else
        {                   
            BackGroundColor();
            newStage = true;
            button = Button.large;
        }
        */
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

    void NewStage()
    {
        if (unClearButtonColor.transform.position.x + (unClearColor.bounds.size.x / 2f) < stageButtons[stageClearNumber].transform.position.x + (stageBuuttonSizse.bounds.size.x / 2f))
        {
            parentUnClearButtonColor.transform.localScale = new Vector3(parentUnClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, parentUnClearButtonColor.transform.localScale.y, 0);
        }
        else
        {
            currentStage = stageClearNumber;
            newStage = false;
        }
    }

    private void BackGroundColor()
    {       
        BackGround[backGuroundNumber].color = Color.Lerp(startColor, endColor, t += 1f / 5f);
    }

    public void Sctoll(bool left)
    {
       
        if (left)
        {
            if (backGuroundNumber > 0)
            {
                screenSizeX = ScreenSizeX();
                screenPos = mainCamera.transform.position;
                ButtonOff(false);
                sctollL = true;
            }
        }
        else
        {
            if (backGuroundNumber < BackGround.Length)
            {
                screenSizeX = ScreenSizeX();
                screenPos = mainCamera.transform.position;
                ButtonOff(false);
                sctollR = true;
            }
        }
        
    }



    public void ButtonFalling(int buttonNumber)
    {
        stageButtons[buttonNumber].transform.localScale = new Vector3(0.2f , 0.2f, 0);
    }

    public void NotButtonFalling(int buttonNumber)
    {
        stageButtons[buttonNumber].transform.localScale = new Vector3(0.17f, 0.17f, 0);
    }

    public void SceneTransition()
    {

        if (number > 0)
        {
            if (FadeOut(forwardImage))
            {
                SceneManager.LoadScene(number);
                number = 0;
            }
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


    private float ScreenSizeX()
    {
        Vector3 topLeft = camera.ScreenToWorldPoint(Vector3.zero);
        topLeft.Scale(new Vector3(1f, -1f, 1f));

        Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        float screenSizeX = Mathf.Sqrt(Mathf.Pow(topLeft.x - topRight.x, 2));
 
        return screenSizeX;
    }

    void ButtonOff(bool off)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(off);
        } 
    }


    void NextGame()
    {
        PlayerPrefs.SetInt("StageClear", stageClearNumber);
        PlayerPrefs.Save();
    }
}

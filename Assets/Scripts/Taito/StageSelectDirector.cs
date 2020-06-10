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
    SpriteRenderer[] stageButtonsSp;

    [SerializeField]
    GameObject[] buttons1;

    [SerializeField]
    GameObject[] buttons2;

    [SerializeField]
    GameObject[] sctollButton;

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

    AudioSource[] audioSource; //オーディオソース使用
    bool stageAddStartFlag = false; //ステージ追加を開始したか判定
    bool sceneChangeStartFlag = false; //シーン遷移を開始したか判定

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
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得

        stageClearNumber = currentStage = PlayerPrefs.GetInt("CurrentStage",0);

        stageClearNumber = PlayerPrefs.GetInt("StageClear",0) + 4;

        ButtonOff(false);
        ButtonOff(true);

        screenSizeX = ScreenSizeX();
        screenPos = mainCamera.transform.position;

        camera.orthographicSize = 5f;

        BackGround[0].color = Color.Lerp(startColor, endColor, t);

        forwardImage.color = new Color(1, 1, 1, 1);
        fadeIN = true;     

         Restoration();
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
            if (this.stageAddStartFlag == false) //フラグがオフだったら
            {
                audioSource[0].Play(); //音を鳴らす（ステージ追加演出音）
                Debug.Log("ステージ追加演出");
                this.stageAddStartFlag = true; //フラグをオン
            }

            stageButtons[stageClearNumber-1].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
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
        float a = (stageButtons[stageClearNumber].transform.GetChild(1).transform.position.x + (stageButtonsSp[stageClearNumber].bounds.size.x / 2f));
        float b = (stageButtons[stageClearNumber - 1].transform.GetChild(1).transform.position.x - (stageButtonsSp[stageClearNumber - 1].bounds.size.x / 2f));
        float c = ((a - b) / 2f) + b;

        if (clearButtonColor.transform.position.x + (ac.bounds.size.x / 2f) < c) 
        {
            parentClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, 
                parentClearButtonColor.transform.localScale.y, 
                0);
        }
        else
        {
            parentUnClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime,
          parentClearButtonColor.transform.localScale.y,
          0);
            BackGroundColor();
            stageButtons[stageClearNumber].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -2;
            newStage = true;
            button = Button.large;
        }
        
    }

    void NewStage()
    {
        if (unClearButtonColor.transform.position.x + (unClearColor.bounds.size.x / 2f) < stageButtons[stageClearNumber].transform.GetChild(1).transform.position.x + (stageBuuttonSizse.bounds.size.x / 2f))
        {
            parentUnClearButtonColor.transform.localScale = new Vector3(parentUnClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, parentUnClearButtonColor.transform.localScale.y, 0);
        }
        else
        {
            audioSource[1].Play(); //音を鳴らす（ステージ追加演出終了音）
            Debug.Log("追加演出終了");

            currentStage = stageClearNumber;
            SaveCurrent();
            ButtonOff(true);
            newStage = false;
        }
    }


    private void BackGroundColor()
    {       
        BackGround[backGuroundNumber].color = Color.Lerp(startColor, endColor, t += 1f / 6f);
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
        stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = new Vector3(0.3f , 0.3f, 0);
    }

    public void NotButtonFalling(int buttonNumber)
    {
        stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = new Vector3(0.27f, 0.27f, 0);
    }

    public void SceneTransition()
    {
        if (this.sceneChangeStartFlag == false) //フラグがオフだったら
        {
            audioSource[2].Play(); //音を鳴らす（シーン遷移開始音）
            Debug.Log("シーン遷移開始");
            this.sceneChangeStartFlag = true; //フラグオン
        }

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
        GameObject[][] buttonsALL = { buttons1, buttons2 };
        if (off)
        {

            sctollButton[0].SetActive(true);
            sctollButton[1].SetActive(true);
            int c = 0;
            
            if (backGuroundNumber == 0)
            {
                c = stageClearNumber + 1;
            }
            else if (backGuroundNumber == 1)
            {
                Debug.Log(backGuroundNumber);
                c = stageClearNumber - buttons2.Length + 1;
            }
            else
            {
                c = 0;
            }

            if (c > 6)
            {
                c = 6;
            }

            for (int i = 0; i < c; i++)
            {
                buttonsALL[backGuroundNumber][i].SetActive(off);
            }            
        }
        else
        {
            sctollButton[0].SetActive(false);
            sctollButton[1].SetActive(false);
            for (int i = 0; i < buttonsALL.Length; i++)
            {
                for (int j = 0; j < buttonsALL[i].Length; j++)
                {
                    buttonsALL[i][j].SetActive(false);
                }
            }
        }
    }

    void Restoration()
    {
        Debug.Log(currentStage);
        float c = 0;
        if (currentStage > 0)
        {
            float a = (stageButtons[currentStage].transform.GetChild(1).transform.position.x + (stageButtonsSp[currentStage].bounds.size.x / 2f));
            float b = (stageButtons[currentStage - 1].transform.GetChild(1).transform.position.x - (stageButtonsSp[currentStage - 1].bounds.size.x / 2f));
            c = ((a - b) / 2f) + b;
        }       
        

        bool clear = true;
        bool unClear = true;

        while (clear || unClear) {
            if (clearButtonColor.transform.position.x + (ac.bounds.size.x / 2f) < c && currentStage > 0)
            {
                parentClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime,
                    parentClearButtonColor.transform.localScale.y,
                    0);
            }
            else
            {
                stageButtons[currentStage].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -2;
                clear = false;
            }


                if (unClearButtonColor.transform.position.x + (unClearColor.bounds.size.x / 2f) < stageButtons[currentStage].transform.GetChild(1).transform.position.x + (stageBuuttonSizse.bounds.size.x / 2f))
                {
                    parentUnClearButtonColor.transform.localScale = new Vector3(parentUnClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, parentUnClearButtonColor.transform.localScale.y, 0);
                }
                else
                {
                    unClear = false;
                }
            
        }
    }

    void SaveCurrent()
    {       
        PlayerPrefs.SetInt("CurrentStage", currentStage);
        PlayerPrefs.Save();
        if (PlayerPrefs.HasKey("CurrentStage"))
        {
            Debug.Log(currentStage);
        }
        
    }

    void NextGame()
    {
        PlayerPrefs.SetInt("StageClear", stageClearNumber);
        PlayerPrefs.Save();
    }
}

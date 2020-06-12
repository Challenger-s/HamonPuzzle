using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectDirector : MonoBehaviour
{
    [SerializeField]
    float backGroundMoveSpeed = 1;

    public int stageClearNumber = 1;


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
    public Material unClearButtonColorMterial;

    [SerializeField]
    public Material SubUnClearButtonColorMterial;

    [SerializeField]
    public GameObject[] stageButtons;

    [SerializeField]
    SpriteRenderer[] stageButtonsSp;

    [SerializeField]
    GameObject[] buttons1;

    [SerializeField]
    GameObject[] buttons2;

    [SerializeField]
    GameObject[] buttons3;

    [SerializeField]
    GameObject[] sctollButton;

    [SerializeField]
    public GameObject[] sctollButtonImage;

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
    bool clearStage = false;

    public int number = 0;

    float screenSizeX = 0;

    float delta = 0;
    float span = 0.2f;
    public int buttonNumber;
    int endButtonNumber;

    float largeSize = 1.2f;
    float changeSizeTime = 1f;
    float defoultSizeX = 0;
    float defoultSizeY = 0;

    int randomBgm = 0; //Bgmをランダムに選ぶ変数

    Vector3 screenPos;

    AudioSource[] audioSource; //オーディオソース使用
    bool stageAddStartFlag = false; //ステージ追加を開始したか判定
    bool sceneChangeStartFlag = false; //シーン遷移を開始したか判定

    public enum Button
    {
        large,
        smaller,

        normal,
    }
    Button button = Button.normal;

    Button touchedButton = Button.normal;

    enum AddStage
    {
        clearStage,
        newStage,

        normal,
    }
    AddStage addStage = AddStage.normal;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>(); //オーディオソース取得
        //ランダム抽選
        this.randomBgm = Random.Range(0, 2);
        //０だったら
        if(this.randomBgm == 0)
        {
            audioSource[5].Play();
        }
        /*
        //１だったら
        else if(this.randomBgm == 1)
        {
            audioSource[6].Play();
        }
        */
        //1だったら
        else if(this.randomBgm == 1)
        {
            audioSource[7].Play();
        }


        defoultSizeX = stageButtons[0].transform.GetChild(1).transform.localScale.x;
        defoultSizeY = stageButtons[0].transform.GetChild(1).transform.localScale.y;
            
        stageClearNumber = currentStage = PlayerPrefs.GetInt("CurrentStage",0);
        
        if (currentStage > stageButtons.Length - 1)
        {
            stageClearNumber = currentStage = stageButtons.Length - 1;
        }

        ButtonOff(false);
        SctollBUtton();

        screenSizeX = ScreenSizeX();
        screenPos = mainCamera.transform.position;

        camera.orthographicSize = 5f;

        BackGround[0].color = Color.Lerp(startColor, endColor, t);

        forwardImage.color = new Color(1, 1, 1, 1);
        fadeIN = true;     

        Restoration();

        stageClearNumber = PlayerPrefs.GetInt("StageClear", 0);

        stageButtons[stageClearNumber - 1].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;

        if (stageClearNumber > stageButtons.Length - 1)
        {
            stageClearNumber = stageButtons.Length - 1;
            addStage = AddStage.clearStage;
        }

        if (stageClearNumber > currentStage)
        {
            BackGroundColor();
            addStage = AddStage.clearStage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIN)
        {
            if (FadeIn(forwardImage))
            {             
                ButtonOff(true);
                fadeIN = false;
            }
        }


        switch (addStage)
        {
            case AddStage.clearStage:

                if (this.stageAddStartFlag == false) //フラグがオフだったら
                {
                    audioSource[0].Play(); //音を鳴らす（ステージ追加演出音）
                    Debug.Log("ステージ追加演出");
                    this.stageAddStartFlag = true; //フラグをオン
                }
                StageAddition();

                break;


            case AddStage.newStage:
                NewStage();
                break;
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


        if (sctollR)
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
                if (ButtonFalling(stageClearNumber))
                {
                    button = Button.smaller;
                }             
                break;

            case Button.smaller:
                Debug.Log(button);
                //stageButtons[currentStage - 1].transform.localScale = new Vector3(stageButtons[currentStage - 1].transform.localScale.x - 0.01f + Time.deltaTime, stageButtons[currentStage - 1].transform.localScale.y - 0.01f + Time.deltaTime, 0);
                if (NotButtonFalling(stageClearNumber))
                {
                    button = Button.normal;
                }
                break;
        }

        if (sceneTransition)
        {
            SceneTransition();
        }

    }

    void StageAddition()
    {
        if(stageClearNumber >= stageButtons.Length - 1)
        {
            if (clearButtonColor.transform.position.x + (ac.bounds.size.x / 2f) < stageButtons[stageClearNumber].transform.GetChild(1).transform.position.x + (stageBuuttonSizse.bounds.size.x / 2f))
            {
                parentClearButtonColor.transform.localScale = new Vector3(parentClearButtonColor.transform.localScale.x + 1.15f * Time.deltaTime, parentClearButtonColor.transform.localScale.y, 0);
            }
            else
            {
                addStage = AddStage.normal;
            }  
        }
        else
        {
            if (stageClearNumber < stageButtons.Length - 1)
            {
                float a = (stageButtons[stageClearNumber].transform.GetChild(1).transform.position.x + (stageButtonsSp[stageClearNumber].bounds.size.x / 2f));
                float b = (stageButtons[stageClearNumber - 1].transform.GetChild(1).transform.position.x - (stageButtonsSp[stageClearNumber - 1].bounds.size.x / 2f));
                float c = ((a - b) / 2f) + b;


                if (clearButtonColor.transform.position.x + (ac.bounds.size.x / 2f) < c && stageClearNumber < stageButtons.Length - 1)
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
                    stageButtons[stageClearNumber].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -2;
                    addStage = AddStage.newStage;
                }
            }
            
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
            if (!(sctollL || sctollR))
            {
                ButtonOff(true);
            }
              button = Button.large;
            addStage = AddStage.normal;
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
                audioSource[3].Play(); //音を鳴らす（スクロール音）
                Debug.Log("左スクロール");

                screenSizeX = ScreenSizeX();
                screenPos = mainCamera.transform.position;
                ButtonOff(false);
                sctollL = true;
            }
        }
        else
        {
            if (backGuroundNumber < BackGround.Length - 1)
            {
                audioSource[3].Play(); //音を鳴らす（スクロール音）
                Debug.Log("右スクロール");

                screenSizeX = ScreenSizeX();
                screenPos = mainCamera.transform.position;
                ButtonOff(false);
                sctollR = true;
            }
        }
    }



    bool ButtonFalling(int buttonNumber)
    {
        if (buttonNumber == stageClearNumber)
        {
            stageButtons[buttonNumber].transform.GetChild(1).Find("ButtonBack").GetComponent<Renderer>().material = SubUnClearButtonColorMterial;
        }
       
        Vector3 size = stageButtons[buttonNumber].transform.GetChild(1).transform.localScale;
        if (size.x < defoultSizeX * largeSize)
        {
           
            size.x = size.x + size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
        }
        else if(size.x > defoultSizeX * largeSize)
        {
            Debug.Log("a");
            size.x = defoultSizeX * largeSize;
            size.y = defoultSizeY * largeSize;
            stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
            return true;
        }        
        return false;
    }

    public bool NotButtonFalling(int buttonNumber)
    {
        
        var size = stageButtons[buttonNumber].transform.GetChild(1).transform.localScale;
        if (size.x > defoultSizeX)
        {
            size.x = size.x - size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y - size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
        }
        else if (size.x < defoultSizeX)
        {
            size.x = defoultSizeX;
            size.y = defoultSizeY;
            if (buttonNumber == stageClearNumber)
            {
                stageButtons[buttonNumber].transform.GetChild(1).Find("ButtonBack").GetComponent<Renderer>().material = unClearButtonColorMterial;                
            }
            stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
            return true;
        }
        return false;
    }


    public void SceneTransition()
    {
        if (this.sceneChangeStartFlag == false) //フラグがオフだったら
        {
            ButtonOff(false);
            audioSource[2].Play(); //音を鳴らす（シーン遷移開始音）
            Debug.Log("シーン遷移開始");
            this.sceneChangeStartFlag = true; //フラグオン
        }

        //音量ダウン
        audioSource[2].volume = audioSource[2].volume - 0.6f * Time.deltaTime;
        audioSource[4].volume = audioSource[4].volume - 0.6f * Time.deltaTime;
        audioSource[5].volume = audioSource[5].volume - 0.4f * Time.deltaTime;
        audioSource[6].volume = audioSource[6].volume - 0.4f * Time.deltaTime;
        audioSource[7].volume = audioSource[7].volume - 0.4f * Time.deltaTime;

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
        image.color = new Color(255, 255, 255, image.color.a + (0.5f * Time.deltaTime));

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
        image.color = new Color(255, 255, 255, image.color.a - (0.5f * Time.deltaTime));

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
        GameObject[][] buttonsALL = { buttons1, buttons2 ,buttons3};
        if (off)
        {
            int c = 0;
            SctollBUtton();
            if (backGuroundNumber == 0)
            {
                c = currentStage + 1;
            }
            else if (backGuroundNumber == 1)
            {
                Debug.Log(backGuroundNumber);
                c = currentStage - buttons2.Length + 1;
            }
            else if(backGuroundNumber == 2)
            {
                c = currentStage - buttons3.Length + 1;
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

    void SctollBUtton()
    {
        if (backGuroundNumber < 1)
        {
            //sctollButton[0].GetComponent<Button>().interactable = true;
            sctollButtonImage[0].SetActive(false);
            sctollButtonImage[1].SetActive(true);
            sctollButton[1].SetActive(true);
        }
        else if (backGuroundNumber > BackGround.Length - 2)
        {
            sctollButton[1].SetActive(false);
            sctollButtonImage[1].SetActive(false);
            sctollButtonImage[0].SetActive(true);
            sctollButton[0].SetActive(true);
        }
        else
        {
            sctollButton[0].SetActive(true);
            sctollButton[1].SetActive(true);
            sctollButtonImage[1].SetActive(true);
            sctollButtonImage[0].SetActive(true);
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
                for (int i = 0; i < currentStage; i++) {
                BackGroundColor();
                }
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
}

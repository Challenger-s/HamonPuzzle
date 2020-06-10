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
    Material unClearButtonColorMterial;

    [SerializeField]
    Material SubUnClearButtonColorMterial;

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

    [SerializeField] float largeSize = 1.3f;
    [SerializeField] float changeSizeTime = 0.2f;

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

    public int buttonNumber;

    float screenSizeX = 0;

    float defoultSizeX = 0;
    float defoultSizeY = 0;

    Vector3 screenPos;

    public enum Button
    {
        large,
        smaller,

        normal,
    }
    Button button = Button.normal;

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
        defoultSizeX = stageButtons[0].transform.GetChild(1).transform.localScale.x;
        defoultSizeY = stageButtons[0].transform.GetChild(1).transform.localScale.y;
            
        stageClearNumber = currentStage = 7;//PlayerPrefs.GetInt("CurrentStage",0);
        
        if (currentStage > stageButtons.Length - 1)
        {
            stageClearNumber = currentStage = stageButtons.Length - 1;
        }


        ButtonOff(false);
        ButtonOff(true);

        screenSizeX = ScreenSizeX();
        screenPos = mainCamera.transform.position;

        camera.orthographicSize = 5f;

        BackGround[0].color = Color.Lerp(startColor, endColor, t);

        forwardImage.color = new Color(1, 1, 1, 1);
        fadeIN = true;     

         Restoration();

        stageClearNumber = 8; //PlayerPrefs.GetInt("StageClear", 0);
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

                Debug.Log(stageClearNumber);
                Debug.Log(currentStage);
                fadeIN = false;
            }
        }


        switch (addStage)
        {
            case AddStage.clearStage:
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
                if(ButtonFalling(buttonNumber))
                {
                    Debug.Log("c");
                    button = Button.smaller;
                }                
                break;

            case Button.smaller:
                //stageButtons[currentStage - 1].transform.localScale = new Vector3(stageButtons[currentStage - 1].transform.localScale.x - 0.01f + Time.deltaTime, stageButtons[currentStage - 1].transform.localScale.y - 0.01f + Time.deltaTime, 0);
                NotButtonFalling(buttonNumber);
                
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
                    buttonNumber = stageClearNumber - 1;
                    button = Button.large;
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
        
            currentStage = stageClearNumber;
            SaveCurrent();
            if (!(sctollL || sctollR))
            {
                ButtonOff(true);
            }
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



    bool ButtonFalling(int buttonNumber)
    {
        if (buttonNumber == stageClearNumber)
        {
            stageButtons[buttonNumber].transform.GetChild(1).Find("ButtonBack").GetComponent<Renderer>().material = SubUnClearButtonColorMterial;
        }
        Debug.Log("a");
        Vector3 size = stageButtons[buttonNumber].transform.GetChild(1).transform.localScale;

        Debug.Log(size.x);
        Debug.Log(defoultSizeX * largeSize);
        if (size.x < defoultSizeX * largeSize)
        {
           
            size.x = size.x + size.x * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            size.y = size.y + size.y * 1 / changeSizeTime * largeSize * Time.unscaledDeltaTime;
            stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
        }
        else if(size.x > defoultSizeX * largeSize)
        {
            size.x = defoultSizeX * largeSize;
            size.y = defoultSizeY * largeSize;
            stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = size;
            return true;
        }        
        return false;
    }

    public void NotButtonFalling(int buttonNumber)
    {
        stageButtons[buttonNumber].transform.GetChild(1).transform.localScale = new Vector3(defoultSizeX, defoultSizeY, 1);
        
        if (buttonNumber == stageClearNumber)
        {
            stageButtons[buttonNumber].transform.GetChild(1).Find("ButtonBack").GetComponent<Renderer>().material = unClearButtonColorMterial;
        }
    }


    public void ButtonEnumChange(bool enumButton)
    {
        if (enumButton) {
            button = Button.large;
        }
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

    public void NextGame(int stageClearNumber)
    {
        PlayerPrefs.SetInt("StageClear", stageClearNumber);
        PlayerPrefs.Save();
    }
}

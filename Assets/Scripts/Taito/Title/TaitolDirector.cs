using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaitolDirector : MonoBehaviour
{
    [SerializeField]
    GameObject TransparentSpherePrefab;

    Vector3 clickPosition;

    [SerializeField]
    Image forwardImage;

    [SerializeField]
    SpriteRenderer backImage;

    [SerializeField]
    GameObject deleteText;

    [SerializeField]
    GameObject deleteYes;

    [SerializeField]
    SpriteRenderer spDeleteYes;

    [SerializeField]
    GameObject deleteNo;

    [SerializeField]
    GameObject[] buttons;

    [SerializeField] GameObject TitleRippleGenerator;

    public bool delete = false;

    bool click = true;

    public bool sceneReload = false;

    enum DeleteStep
    {
        s1,
        s2,
        s3,
    }

    DeleteStep deleteStep = DeleteStep.s1;

    // Start is called before the first frame update
    void Start()
    {
        deleteText.SetActive(false);
        deleteNo.SetActive(false);
        deleteYes.SetActive(false);
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delete)
        {           
            Delete();
        }

        if (sceneReload)
        {
            SceneReload();
        }

        if (click)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickPosition = Input.mousePosition;

                clickPosition.z = 5f;

                Instantiate(TransparentSpherePrefab, Camera.main.ScreenToWorldPoint(clickPosition), TransparentSpherePrefab.transform.rotation);
                Destroy(TitleRippleGenerator);
                click = false;
            }
        }

    }

    void Delete()
    {
        buttons[0].SetActive(false);
        Destroy(GameObject.FindGameObjectWithTag("TransparentSphere"));
        switch (deleteStep) {

            case DeleteStep.s1:
                if (FadeOut(forwardImage) == true)
                {
                    backImage.color = new Color(1, 1, 1, 1);
                    deleteText.SetActive(true);
                    deleteNo.SetActive(true);
                    buttons[2].SetActive(true);
                    deleteStep = DeleteStep.s2;               
                }
                break;


            case DeleteStep.s2:
                if (FadeIn(forwardImage) == true)
                {
                    spDeleteYes.color = new Color(spDeleteYes.color.r, spDeleteYes.color.g, spDeleteYes.color.b, 0);
                    deleteYes.SetActive(true);
                    Debug.Log("a");
                    deleteStep = DeleteStep.s3;
                }
                break;


            case DeleteStep.s3:

                spDeleteYes.color += new Color(0, 0, 0, forwardImage.color.a + (1f * Time.deltaTime));

                if (spDeleteYes.color.a > 1)
                {
                    buttons[1].SetActive(true);
                    deleteStep = DeleteStep.s1;
                    delete = false;

                }
                break;
        }

    }

    public void SceneReload()
    {
        if (FadeOut(forwardImage))
        {
            // 現在のScene名を取得する
            Scene loadScene = SceneManager.GetActiveScene();
            // Sceneの読み直し
            SceneManager.LoadScene(loadScene.name);
        }
    }

    bool FadeOut(Image image)
    {
        image.color = new Color(255, 255, 255, forwardImage.color.a + (0.3f * Time.deltaTime));
        
        if(image.color.a > 1)
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
        image.color = new Color(255, 255, 255, forwardImage.color.a - (0.3f * Time.deltaTime));

        if (image.color.a < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //void 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentSphere : MonoBehaviour
{
    Vector3 sphereScale;

    Vector3 topLeft;
    Vector3 bomttomLeft;
    Vector3 topRight;
    Vector3 bomttomRight;

    [SerializeField]
    Camera _mainCamera;

    [SerializeField]
    bool Object2D = true;

    [SerializeField]
    float expansionSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        sphereScale  = transform.localScale;

        topLeft      = getScreenTopLeft();
        bomttomLeft  = getScreenBottomLeft();
        topRight     = getSceenTopRight();
        bomttomRight = getScreenBottomRight();
    }

    // Update is called once per frame
    void Update()
    {
        if (expansion(topLeft)     ||
            expansion(bomttomLeft) ||
            expansion(topRight)    ||
            expansion(bomttomRight))
        {
            transform.localScale = new Vector3(sphereScale.x += (expansionSpeed * Time.deltaTime), sphereScale.y += (expansionSpeed * Time.deltaTime), 0);
        }

    }

    private bool expansion(Vector3 cameraPos)
    {
        Vector3 spherePos = transform.position;

        float width = 0;

        if (Object2D)
        {
            width = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        }
        else
        {
            width = transform.localScale.x;
        }

        float distance = Mathf.Sqrt(Mathf.Pow(spherePos.x - cameraPos.x, 2) + Mathf.Pow(spherePos.y - cameraPos.y, 2));


        if ((width / 2) < distance)
        {
            return true;
        }
        else
        {
            return false;          
        }        
    }


    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = _mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomLeft()
    {
        Vector3 bomttemLeft = _mainCamera.ScreenToWorldPoint(Vector3.zero);

        return bomttemLeft;
    }

    private Vector3 getSceenTopRight()
    {
        Vector3 leftRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        return leftRight;
    }

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
}

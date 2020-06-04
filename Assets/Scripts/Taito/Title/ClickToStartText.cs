using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStartText : MonoBehaviour
{
    SpriteRenderer clickToStart;

    bool a = true;
    bool b = false;

    // Start is called before the first frame update
    void Start()
    {
        clickToStart = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            if (clickToStart.color.a > 0)
            {
                clickToStart.color = new Color(clickToStart.color.r, clickToStart.color.g, clickToStart.color.b, clickToStart.color.a - 0.5f * Time.deltaTime);
            }
            else
            {
                a = false;
                b = true;
            }
        }

        if (b)
        {
            if (clickToStart.color.a < 1)
            {
                clickToStart.color = new Color(clickToStart.color.r, clickToStart.color.g, clickToStart.color.b, clickToStart.color.a + 0.5f * Time.deltaTime);
            }
            else
            {
                b = false;
                a = true;
            }
        }
    }
}

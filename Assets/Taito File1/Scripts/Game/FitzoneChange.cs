using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitzoneChange : MonoBehaviour
{
    [SerializeField]
    FitzoneController fitzoneController;

    [SerializeField]
    GameObject notEnough;

    [SerializeField]
    SpriteRenderer[] SpNotEnoughs;

    [SerializeField]
    GameObject complete;

    [SerializeField]
    GameObject burst;

    [SerializeField]
    float GrowingSpeed = 0.3f;

    [SerializeField]
    float SmallerSpeed = 0.3f;

    int lastCont = 1;

    int clearCont = 0;

    float delta = 0;
    float span = 0.3f;

    float size = 0;
    float maxSize = 0;

    bool a = false;
    bool b = false;

    // Start is called before the first frame update
    void Start()
    {
        maxSize = notEnough.transform.localScale.x * 1.2f;
        size = notEnough.transform.localScale.x;

        clearCont = fitzoneController.GetCount();
        lastCont = fitzoneController.GetCount();
        Debug.Log(clearCont);
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;

        if (delta > span)
        {
            //notEnough.transform.localScale = new Vector3(0.5f, 0.5f, 1);

            //complete.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        if (a)
        {
            if (notEnough.transform.localScale.x < maxSize || complete.transform.localScale.x < maxSize)
            {
                notEnough.transform.localScale = new Vector3(notEnough.transform.localScale.x + GrowingSpeed * Time.deltaTime, notEnough.transform.localScale.y + GrowingSpeed * Time.deltaTime, 1);
                complete.transform.localScale = new Vector3(complete.transform.localScale.x + GrowingSpeed * Time.deltaTime, complete.transform.localScale.y + GrowingSpeed * Time.deltaTime, 1);
            }
            else
            {
                a = false;
                b = true;
            }
        }

        if (b)
        {
            if (notEnough.transform.localScale.x > size || complete.transform.localScale.x > size)
            {
                notEnough.transform.localScale = new Vector3(notEnough.transform.localScale.x - SmallerSpeed * Time.deltaTime, notEnough.transform.localScale.y - SmallerSpeed * Time.deltaTime, 1);
                complete.transform.localScale = new Vector3(complete.transform.localScale.x - SmallerSpeed * Time.deltaTime, complete.transform.localScale.y - SmallerSpeed * Time.deltaTime, 1);
            }
            else
            {
                b = false;
            }
        }




        if (!(lastCont == fitzoneController.GetCount()))
        {
            lastCont = fitzoneController.GetCount();
            if (fitzoneController.GetCount() == 0)
            {
                Complete();
            }
            else if (fitzoneController.GetCount() < 0)
            {
                Burst();
            }
            else if (fitzoneController.GetCount() > 0)
            {
                NotEnough();
            }
        }
    }

    void NotEnough()
    {
        complete.SetActive(false);
        burst.SetActive(false);
        notEnough.SetActive(true);

        if (lastCont < clearCont)
        {
            delta = 0;

            /*
            notEnough.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            for (int i = 0; i < SpNotEnoughs.Length; i++)
            {
                SpNotEnoughs[i].color = new Color(SpNotEnoughs[i].color.r, SpNotEnoughs[i].color.g, SpNotEnoughs[i].color.b, SpNotEnoughs[i].color.a - (1 / 3f));
            }
            */
            a = true;

        }
        else
        {
            notEnough.transform.localScale = new Vector3(size, size, 1);
            for (int i = 0; i < SpNotEnoughs.Length; i++)
            {
                SpNotEnoughs[i].color = new Color(SpNotEnoughs[i].color.r, SpNotEnoughs[i].color.g, SpNotEnoughs[i].color.b, 1);
            }
        }
    }

    void Complete()
    {
        delta = 0f;
        burst.SetActive(false);
        notEnough.SetActive(false);
        complete.SetActive(true);
        //complete.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        a = true;
    }

    void Burst()
    {
        notEnough.SetActive(false);
        complete.SetActive(false);
        burst.SetActive(true);
    }
}

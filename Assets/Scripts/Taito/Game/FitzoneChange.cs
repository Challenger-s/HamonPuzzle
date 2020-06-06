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

    [SerializeField] GameDirector gameDirector;

    [SerializeField]
    float GrowingSpeed = 0.3f;

    [SerializeField]
    float SmallerSpeed = 0.3f;

    int lastCont = 1;

    int clearCont = 0;

    float delta = 0;
    float span = 0.3f;

    float minsize = 0;
    float maxSize = 0;

    enum Change
    {
        NotEnougLarge,
        NotEnougSmall,
        CompleteLrage,
        CompleteSmall,
        BurstLarge,
        BurstSmall,
        NULL,
    }
    Change change = Change.NULL;

    // Start is called before the first frame update
    void Start()
    {
        maxSize = notEnough.transform.localScale.x * 1.2f;
        minsize = notEnough.transform.localScale.x;

        clearCont = fitzoneController.GetCount();
        lastCont = fitzoneController.GetCount();
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameDirector.ReturnPhase())
        //{
            delta += Time.deltaTime;

            if (delta > span)
            {
                //notEnough.transform.localScale = new Vector3(0.5f, 0.5f, 1);

                //complete.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }

            switch (change)
            {
                case Change.NotEnougLarge:
                    if (Large(notEnough))
                    {

                        change = Change.NotEnougSmall;
                    }
                    break;
                case Change.NotEnougSmall:
                    if (Small(notEnough))
                    {
                        change = Change.NULL;
                    }
                    break;


                case Change.CompleteLrage:
                    if (Large(complete))
                    {
                        change = Change.CompleteSmall;
                    }
                    break;
                case Change.CompleteSmall:
                    if (Small(complete))
                    {
                        change = Change.NULL;
                    }
                    break;


                case Change.BurstLarge:
                    if (Large(burst))
                    {
                        change = Change.BurstSmall;
                    }
                    break;
                case Change.BurstSmall:
                    if (Small(burst))
                    {
                        change = Change.NULL;
                    }
                    break;
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
        //}
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
            Debug.Log("a");
            change = Change.NotEnougLarge;
        }
        else
        {
            //notEnough.transform.localScale = new Vector3(minsize, minsize, 1);
            for (int i = 0; i < SpNotEnoughs.Length; i++)
            {
                SpNotEnoughs[i].color = new Color(SpNotEnoughs[i].color.r, SpNotEnoughs[i].color.g, SpNotEnoughs[i].color.b, 1);
            }
        }
    }

    void Complete()
    {
        burst.SetActive(false);
        notEnough.SetActive(false);
        complete.SetActive(true);
        //complete.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        change = Change.CompleteLrage;
    }

    void Burst()
    {
        notEnough.SetActive(false);
        complete.SetActive(false);
        burst.SetActive(true);
        change = Change.BurstLarge;
    }

    bool Large(GameObject fitzone)
    {
        fitzone.transform.localScale = new Vector3(fitzone.transform.localScale.x + GrowingSpeed * Time.deltaTime, fitzone.transform.localScale.y + GrowingSpeed * Time.deltaTime, 1);

        if (fitzone.transform.localScale.x > maxSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool Small(GameObject fitzone)
    {
        fitzone.transform.localScale = new Vector3(fitzone.transform.localScale.x - GrowingSpeed * Time.deltaTime, fitzone.transform.localScale.y - GrowingSpeed * Time.deltaTime, 1);

        if (fitzone.transform.localScale.x < minsize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

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

    int lastCont = 1;

    int clearCont = 0;

    float delta = 0;
    float span = 0.3f;
    bool a = true;
    bool b = true;

    // Start is called before the first frame update
    void Start()
    {
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
            notEnough.transform.localScale = new Vector3(0.5f, 0.5f, 1);

            complete.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        if (!(lastCont == fitzoneController.GetCount()))
        {

            lastCont = fitzoneController.GetCount();
            if (fitzoneController.GetCount() == 0)
            {
                Complete();
            }
            else if(fitzoneController.GetCount() < 0)
            {
                Burst();
            }
            else if(fitzoneController.GetCount() > 0)
            {
                NotEnough();
            }
            

        }


    }

    void NotEnough()
    {
        b = true;
        complete.SetActive(false);
        burst.SetActive(false);
        notEnough.SetActive(true);

        if (lastCont < clearCont)
        {
            delta = 0;
            notEnough.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            for (int i = 0; i < SpNotEnoughs.Length; i++)
            {
                SpNotEnoughs[i].color = new Color(SpNotEnoughs[i].color.r, SpNotEnoughs[i].color.g, SpNotEnoughs[i].color.b, SpNotEnoughs[i].color.a - (1 / 3f));
            }
        }
        else
        {
            notEnough.transform.localScale = new Vector3(0.5f, 0.5f, 1);
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
        complete.transform.localScale = new Vector3(0.6f, 0.6f, 1);
    }

    void Burst()
    {       
        notEnough.SetActive(false);
        complete.SetActive(false);
        burst.SetActive(true);
    }
}

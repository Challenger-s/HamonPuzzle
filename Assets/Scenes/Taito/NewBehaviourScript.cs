using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    int number = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("StageClear", 1);
            PlayerPrefs.SetInt("CurrentStage", 1);
            PlayerPrefs.SetInt("backGuroundNumber", 0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("StageClear", 2);
            PlayerPrefs.SetInt("CurrentStage", 2);
            PlayerPrefs.SetInt("backGuroundNumber", 0);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("StageClear", 3);
            PlayerPrefs.SetInt("CurrentStage", 3);
            PlayerPrefs.SetInt("backGuroundNumber", 0);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("StageClear", 4);
            PlayerPrefs.SetInt("CurrentStage", 4);
            PlayerPrefs.SetInt("backGuroundNumber", 0);
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            PlayerPrefs.SetInt("StageClear", 5);
            PlayerPrefs.SetInt("CurrentStage", 5);
            PlayerPrefs.SetInt("backGuroundNumber", 0);
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            PlayerPrefs.SetInt("StageClear", 6);
            PlayerPrefs.SetInt("CurrentStage", 6);
            PlayerPrefs.SetInt("backGuroundNumber", 1);
        }

        if (Input.GetKey(KeyCode.Alpha7))
        {
            PlayerPrefs.SetInt("StageClear", 7);
            PlayerPrefs.SetInt("CurrentStage", 7);
            PlayerPrefs.SetInt("backGuroundNumber", 1);
        }

        if (Input.GetKey(KeyCode.Alpha8))
        {
            PlayerPrefs.SetInt("StageClear", 8);
            PlayerPrefs.SetInt("CurrentStage", 8);
            PlayerPrefs.SetInt("backGuroundNumber", 1);
        }

        if (Input.GetKey(KeyCode.Alpha9))
        {
            PlayerPrefs.SetInt("StageClear", 9);
            PlayerPrefs.SetInt("CurrentStage", 9);
            PlayerPrefs.SetInt("backGuroundNumber", 1);
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("StageClear", 10);
            PlayerPrefs.SetInt("CurrentStage", 10);
            PlayerPrefs.SetInt("backGuroundNumber", 1);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            PlayerPrefs.SetInt("StageClear", 11);
            PlayerPrefs.SetInt("CurrentStage", 11);
            PlayerPrefs.SetInt("backGuroundNumber", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            PlayerPrefs.SetInt("StageClear", 12);
            PlayerPrefs.SetInt("CurrentStage", 12);
            PlayerPrefs.SetInt("backGuroundNumber", 2);
        }

        if (Input.GetKey(KeyCode.E))
        {
            PlayerPrefs.SetInt("StageClear", 13);
            PlayerPrefs.SetInt("CurrentStage", 13);
            PlayerPrefs.SetInt("backGuroundNumber", 2);
        }

        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetInt("StageClear", 14);
            PlayerPrefs.SetInt("CurrentStage", 14);
            PlayerPrefs.SetInt("backGuroundNumber", 2);
        }

        if (Input.GetKey(KeyCode.T))
        {
            PlayerPrefs.SetInt("StageClear", 15);
            PlayerPrefs.SetInt("CurrentStage", 15);
            PlayerPrefs.SetInt("backGuroundNumber", 2);
        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerPrefs.DeleteKey("StageClear");
            PlayerPrefs.DeleteKey("backGuroundNumber");
            PlayerPrefs.DeleteKey("CurrentStage");
        }

        
    }

}

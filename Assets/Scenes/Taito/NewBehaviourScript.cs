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
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4);
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(5);
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(6);
        }

        if (Input.GetKey(KeyCode.Alpha7))
        {
            SceneManager.LoadScene(7);
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CounterPrefab : MonoBehaviour
{
    [SerializeField] float glowingSpeed = 1;
    [SerializeField] float lowerTransparencySpeed = 1;

    Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x + glowingSpeed * Time.deltaTime, 
                                                    this.transform.localScale.x + glowingSpeed * Time.deltaTime, 1);

        color.a = color.a - lowerTransparencySpeed * Time.deltaTime;
        gameObject.GetComponent<Image>().color = color;

        if (color.a <= 0)
        {
            Destroy(this.gameObject);
            //Debug.Log("hakai");
        }
    }
}

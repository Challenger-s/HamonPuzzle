using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionCreatePrefab : MonoBehaviour
{
    [SerializeField]
    GameObject TransparentSpherePrefab;

    Vector3 clickPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;

            clickPosition.z = 5f;

            Instantiate(TransparentSpherePrefab, Camera.main.ScreenToWorldPoint(clickPosition), TransparentSpherePrefab.transform.rotation);
        }
    }
}

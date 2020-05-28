using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRutton : MonoBehaviour
{
    [SerializeField]
    StageSelectDirector selectDirector;

    [SerializeField]
    bool left = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        selectDirector.Sctoll(left);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteYes : MonoBehaviour
{
    [SerializeField]
    TaitolDirector titleDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClck()
    {
        titleDirector.sceneReload = true;
    }
}

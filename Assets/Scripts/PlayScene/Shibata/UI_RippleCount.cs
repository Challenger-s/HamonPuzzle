using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RippleCount : MonoBehaviour
{
    [SerializeField] GameObject ui_CounterPrefab;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //　UIの波紋を作成
    public void GenerateUIRipple()
    {
        // UIの波紋を作成
        GameObject ui_Ripple = (GameObject)Instantiate(ui_CounterPrefab);
        ui_Ripple.transform.SetParent(this.transform, false);
    }

    //　INのアニメーション
    public void UI_IN()
    {
        this.animator.Play("UI_IN");
    }

    //　OUTのアニメーション
    public void UI_OUT()
    {
        this.animator.Play("UI_OUT");
    }
}

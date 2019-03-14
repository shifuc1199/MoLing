using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class DialogView : View
{
    public Text name_text;
    public Text contenct_text;
    public Image head;
    string _contenct;
    // Start is called before the first frame update
    void Start()
    {
        SetContenct("我的机器配件的埃松坡见到平时见到平时就的撒的撒");
    }
    public void SetName(string name)
    {
       
        name_text.text = name;
    }
    public void SetContenct(string contenct)
    {
        contenct_text.DOText(contenct, contenct.Length/10).SetEase(Ease.Linear);
    }
    public void SetHead(Sprite headsprite)
    {
        head.sprite = headsprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

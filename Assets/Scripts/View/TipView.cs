using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TipView : View
{
    public Text nametext;
    public Text destext;
    public Image icon;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
         
    }
    public void OnClick()
    {
      
        game.Scene._instance.player.GetComponentInChildren<Animator>().SetTrigger("up");
        Time.timeScale = 1;
        button.GetComponent<Image>().raycastTarget = false;
        GetComponent<CanvasGroup>().DOFade(0, 1);
        Timer.Register(1, () => {
            button.GetComponent<Image>().DOFade(0,0);
            nametext.DOFade(0, 0);
            icon.DOFade(0, 0);
            destext.DOFade(0, 0);
            gameObject.SetActive(false); game.Scene._instance.player.Inputable = true; GetComponent<CanvasGroup>().alpha = 1; },null,false,true);
    }
    public void SetItem(Item item)
    {
 
        button.GetComponent<Image>().raycastTarget = false;
        nametext.text = item.name;
        destext.text = item.des;
        icon.sprite = item.itemsprite;
        icon.SetNativeSize();
        Timer.Register(2, () => {   button.GetComponent<Image>().raycastTarget = true; },null,false,true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

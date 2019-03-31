using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using game;
using UnityEngine.UI;
public class DialogView : View
{
    public Text name_text;
    public Text contenct_text;
    public Image head;
    Talk[] talk;
    int index = 0;
    Tweener tweener;
  public  bool icomplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    public void OnNextOnClick()
    {
 
        if (tweener.IsPlaying())
        {
            tweener.Complete();
        }
        else
        {

            index++;
            if (index < talk.Length)
            {

                Talk(talk[index]);
            }
            else
            {
                icomplete = true;
                gameObject.SetActive(false);
                UIManager._instance.GetView<GameView>().gameObject.SetActive(true);
                Scene._instance.player.Inputable = true;
            }
        }
        // contenct_text.DOText(_contenct, 0).SetEase(Ease.Linear);
    }
    public void Talk(Talk talk)
    {
        contenct_text.text = "";
        AudioManager._instance.PlayCV(talk.talk_clip);
        tweener = contenct_text.DOText(talk.contenct, talk.contenct.Length / 10).SetEase(Ease.Linear);
        name_text.text = talk.talker_name;
    }
    public void SetContenct(Talk[] talk)
    {
        UIManager._instance.GetView<GameView>().gameObject.SetActive(false);
        Scene._instance.player.Inputable = false;
      this.talk= talk;
     
      Talk(talk[index]);
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

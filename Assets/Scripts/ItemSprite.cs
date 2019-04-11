using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprite : MonoBehaviour,IInteractClick
{
    public string ID;
    public GameObject btn;
    Item item;
    // Start is called before the first frame update
    void Start()
    {
         item = ConfigManager.item_config.items.Find((a) => { return a.ID == ID; });
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            btn.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            btn.SetActive(false);
        }
    } 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractClick()
    {
        btn.SetActive(false);
        game.Scene._instance.player.Inputable = false;
        game.Scene._instance.player.GetComponentInChildren<Animator>().SetTrigger("down");
        Timer.Register(0.5f, () =>
        {


            UIManager._instance.OpenView<TipView>().SetItem(item);
            Destroy(gameObject);
           
            Time.timeScale = 0;
        });
    }
}

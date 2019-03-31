using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprite : MonoBehaviour
{
    public string ID;
    Item item;
    // Start is called before the first frame update
    void Start()
    {
         item = ConfigManager.item_config.items.Find((a) => { return a.ID == ID; });
        GetComponent<SpriteRenderer>().sprite = item.itemsprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            game.Scene._instance.player.Inputable = false;
            game.Scene._instance.player.GetComponentInChildren<Animator>().SetTrigger("down");
            Timer.Register(0.5f, () =>
            {
              
 
                UIManager._instance.OpenView<TipView>().SetItem(item);
                Destroy(gameObject);
                PlayerInfo.info.ItemDic[ID] = true;
                Time.timeScale = 0;
            });
          
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

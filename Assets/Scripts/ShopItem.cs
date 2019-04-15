using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ShopItemData
{
    public int amount;
    public string ID;
    public Sprite icon;
    public string des;
    public int price;
    
}
 

public class ShopItem : MonoBehaviour
{
    public Image Item_Icon;
    public Text Price;
    public Text Des;
    public Text BuyAmount_text;
    public int BuyAmount;
    public string ID;
    public Button BuyButton;
    private ShopItemData data;
    public void Start()
    {
        Debug.Log(ID);
        Debug.Log(ConfigManager.shopitemconfig==null);
        data = ConfigManager.shopitemconfig.Datas.Find((a) => { return a.ID == ID; });
        Init();
    }
    void Init()
    {
        this.Item_Icon.sprite = data.icon;
      
        CheckCanBuy();
    }
    public void Buy()
    {
        UIManager._instance.GetView<PlayerInfoView>().SetAddMoney(-(BuyAmount * data.price));
        PlayerInfoController._instance.pi.ItemDic[ID]+=BuyAmount;
        data.amount -= BuyAmount;
         BuyAmount = 1;
        UIManager._instance.OpenView<PromptView>();
        CheckCanBuy();
        
    }
    void CheckCanBuy()
    {
        if (data.amount == 0)
            Destroy(gameObject);
        Des.text = data.des + "剩余数量: " + data.amount;
        Price.text = (BuyAmount * data.price).ToString();
        if (BuyAmount * data.price <= PlayerInfoController._instance.pi.Money)
        {
            BuyButton.GetComponentInChildren<Text>().text = "购买";
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.GetComponentInChildren<Text>().text = "没钱";
            BuyButton.interactable = false;
        }

    }
    public void Add()
    {
       if(BuyAmount>= data.amount)
        {
            return;
        }
        BuyAmount++;
        CheckCanBuy();
    }
    public void Minus()
    {
         
        if (BuyAmount>1)
        {
            
            BuyAmount--;
            CheckCanBuy();
        }
    }
    private void Update()
    {
        BuyAmount_text.text = BuyAmount.ToString();
    }

}

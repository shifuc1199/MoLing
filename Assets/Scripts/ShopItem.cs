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
    public ItemType type;
}
public enum ItemType
{
    Equipment,
    Item
}

public class ShopItem : MonoBehaviour
{
    public Image Item_Icon;
    public Text Price;
    public Text Des;
    public Text BuyAmount_text;
    public int BuyAmount;
 
    public Button BuyButton;
    private ShopItemData data;
    public void Start()
    {
       
    }
    private void OnEnable()
    {
        CheckCanBuy();
    }
   public void Init(ShopItemData _data)
    {
        this.data = _data;
        this.Item_Icon.sprite = data.icon;
        CheckCanBuy();

    }
    public void Buy()
    {
        UIManager._instance.GetView<PlayerInfoView>().SetAddMoney(-(BuyAmount * data.price));
        if(PlayerInfoController._instance.pi.ItemDic.ContainsKey(data.ID))
        PlayerInfoController._instance.pi.ItemDic[data.ID] +=BuyAmount;
        if (!PlayerInfoController._instance.pi.BagItemDic.ContainsKey(data.ID))
            PlayerInfoController._instance.pi.BagItemDic.Add(data.ID,ConfigManager.item_config.items.Find(a=> { return a.ID == data.ID; }));

        data.amount -= BuyAmount;
         BuyAmount = 1;
        if(data.type==ItemType.Equipment)
        {
            UIManager._instance.GetView<ShopView>().isBuyEquip = true;
        }
        UIManager._instance.OpenView<PromptView>();
        CheckCanBuy();
        
    }
    void CheckCanBuy()
    {
        if (data == null)
            return;
        if (data.amount == 0)
            Destroy(gameObject);
        Des.text = data.des + "\n剩余数量: " + data.amount;
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

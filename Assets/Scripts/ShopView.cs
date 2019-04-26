using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopView : View
{
    public Text Price;
    public Text Des;
 
    public Text BuyAmount_text;
    public int BuyAmount;
    public Button BuyButton;
    public Button AddButton;
    public Button MinusButton;
    public GameObject BackMask;
    public Transform root;
    public GameObject SelectItem;
    public bool isBuyEquip = false;
    // Start is called before the first frame update
    void Start()
    {
        
        InitShopView();
    }
    private void OnEnable()
    {
       
        if (SelectItem==null)
        {
            BuyButton.interactable = false;
            BuyAmount = 1;
            Price.text = "--";
            Des.text = "--";
            BuyAmount_text.text = "--";
        }

        CheckCanBuy();
    }
  
    public override void OnCloseClick()
    {
    
        base.OnCloseClick();
        BackMask.SetActive(false);
        if (!PlayerInfoController._instance.pi.Teach["Equip"]&& isBuyEquip)
        {
            UIManager._instance.OpenView<TeachView>();
        }
        isBuyEquip = false;

    }
    void InitShopView()
    {
        foreach (var item in ConfigManager.shopitemconfig.Datas)
        {
            GameObject shopitem = Instantiate(Resources.Load<GameObject>("Prefab/ShopItem"), root);
            shopitem.GetComponent<ShopItem>().Init(item);
        }
    }

    public void Buy()
    {
        if (SelectItem == null)
            return;


        UIManager._instance.GetView<PlayerInfoView>().SetAddMoney(-(BuyAmount * SelectItem.GetComponent<ShopItem>().data.price));
        if (PlayerInfoController._instance.pi.ItemDic.ContainsKey(SelectItem.GetComponent<ShopItem>().data.ID))
            PlayerInfoController._instance.pi.ItemDic[SelectItem.GetComponent<ShopItem>().data.ID] += BuyAmount;

        if (!PlayerInfoController._instance.pi.BagItemDic.Contains(SelectItem.GetComponent<ShopItem>().data.ID))
            PlayerInfoController._instance.pi.BagItemDic.Add(SelectItem.GetComponent<ShopItem>().data.ID);

        SelectItem.GetComponent<ShopItem>().data.amount -= BuyAmount;
        BuyAmount = 1;
        if (SelectItem.GetComponent<ShopItem>().data.type == ItemType.Equipment)
        {
            UIManager._instance.GetView<ShopView>().isBuyEquip = true;
        }
        UIManager._instance.OpenView<PromptView>();
        CheckCanBuy();
    }

   public void CheckCanBuy()
    {
        if (SelectItem == null)
            return;
        if (SelectItem.GetComponent<ShopItem>().data == null)
            return;
        if (SelectItem.GetComponent<ShopItem>().data.amount == 0)
            Destroy(gameObject);
       
        Des.text = SelectItem.GetComponent<ShopItem>().data.des + "\n剩余数量: " + SelectItem.GetComponent<ShopItem>().data.amount;
        Price.text = (BuyAmount * SelectItem.GetComponent<ShopItem>().data.price).ToString();
        if (BuyAmount * SelectItem.GetComponent<ShopItem>().data.price <= PlayerInfoController._instance.pi.Money)
        {
            AddButton.interactable = true;
            BuyButton.GetComponentInChildren<Text>().text = "购买";
            BuyButton.interactable = true;
        }
        else
        {
            AddButton.interactable = false;
            BuyButton.GetComponentInChildren<Text>().text = "没钱";
            BuyButton.interactable = false;
        }

    }
    public void Add()
    {
        if (SelectItem == null)
            return;
        if (BuyAmount >= SelectItem.GetComponent<ShopItem>().data.amount)
        {
            return;
        }
        BuyAmount++;
        CheckCanBuy();
    }
    public void Minus()
    {
        if (SelectItem == null)
            return;
        if (BuyAmount > 1)
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

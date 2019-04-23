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
    public GameObject SelectImage;
    public ShopItemData data;
    public void Start()
    {
       
    }
    private void OnEnable()
    {
    }
    public void OnClick()
    {
 
        if (UIManager._instance.GetView<ShopView>().SelectItem == gameObject)
            return;
        if (UIManager._instance.GetView<ShopView>().SelectItem != null)
            UIManager._instance.GetView<ShopView>().SelectItem.GetComponent<ShopItem>().UnClick();

     
        UIManager._instance.GetView<ShopView>().SelectItem = gameObject;
        SelectImage.SetActive(true);
        UIManager._instance.GetView<ShopView>().CheckCanBuy();

    }
      public void UnClick()
    {
        SelectImage.SetActive(false);
    }
   public void Init(ShopItemData _data)
    {
        this.data = _data;
        this.Item_Icon.sprite = data.icon;
     

    }
    

}

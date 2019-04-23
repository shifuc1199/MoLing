using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : View
{
    public Transform root;
    public bool isBuyEquip = false;
    // Start is called before the first frame update
    void Start()
    {
        InitShopView();
    }
    public override void OnCloseClick()
    {
    
        base.OnCloseClick();
        if(!PlayerInfoController._instance.pi.Teach["Equip"]&& isBuyEquip)
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

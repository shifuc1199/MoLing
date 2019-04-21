using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : View
{
    public Transform root;
    // Start is called before the first frame update
    void Start()
    {
        InitShopView();
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

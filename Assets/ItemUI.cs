using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour
{
    Item item;

    bool isEquip = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   public void Equip()
    {
        if (isEquip)
        {
            PlayerInfoController._instance.pi.BagItemDic.Add(item.ID, item);
            PlayerInfoController._instance.pi.EquipItemDic.Remove(item.ID);
            transform.parent = UIManager._instance.GetView<BagView>().UnEquipmentRoot;
        }
        else
        {
            PlayerInfoController._instance.pi.EquipItemDic.Add(item.ID, item);
            PlayerInfoController._instance.pi.BagItemDic.Remove(item.ID);
            transform.parent = UIManager._instance.GetView<BagView>().EquipmentRoot;
        }
        UIManager._instance.GetView<BagView>().TipText.text = "";
        isEquip = !isEquip;
        UIManager._instance.GetView<BagView>().SelectGameObject = null;
        transform.GetChild(0).gameObject.SetActive(false);
    }
     public void OnClick()
    {
        if (UIManager._instance.GetView<BagView>().SelectGameObject == gameObject)
            return;

           if (UIManager._instance.GetView<BagView>().SelectGameObject!=null)
        {
            UIManager._instance.GetView<BagView>().SelectGameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
          
        transform.GetChild(0).gameObject.SetActive(true);
        UIManager._instance.GetView<BagView>().SelectGameObject = gameObject;
        UIManager._instance.GetView<BagView>().SetItemTip(item);
    }
    public  void Init(Item _item)
    {
       
        item = _item;
       
        GetComponent<Image>().sprite = _item.itemsprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

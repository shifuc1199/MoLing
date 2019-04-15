using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemConfig")]
public class ShopItemConfig : ScriptableObject
{
    public List<ShopItemData> Datas = new List<ShopItemData>();
}

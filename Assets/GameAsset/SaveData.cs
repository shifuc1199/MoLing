using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
 
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
public class SerilizedVector3
{
    public float x;
    public float y;
    public float z;
 
    public void Copy(Vector3 temp)
    {
        this.x = temp.x;
        this.y = temp.y;
        this.z = temp.z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
[System.Serializable]
public class GameData
{
    public Dictionary<int, bool> Doors = new Dictionary<int, bool>();
    public Dictionary<string,int> ShopItemDatasAmount = new Dictionary<string, int>();
    public SerilizedVector3 _playerpos=new SerilizedVector3();
    public PlayerInfo info;

    public void UpdateData(bool IncludePos = true)
    {

        foreach (var item in ConfigManager.shopitemconfig.Datas)
        {
            if(ShopItemDatasAmount.ContainsKey(item.ID))
            {
                ShopItemDatasAmount[item.ID] = item.amount;
            }
            else
            ShopItemDatasAmount.Add(item.ID, item.amount);
        }
        
         Doors = game.Scene._instance.DoorDic; 
        info = PlayerInfoController._instance.pi;
        if (IncludePos)
       _playerpos .Copy( game.Scene._instance.player.transform.position);
    }
}
public class SaveData 
{
    public static GameData data=new GameData();
    public  static  void Save(bool Savepos=true)
    {
         
        using (FileStream f = new FileStream(Application.persistentDataPath+"/GameData.data", FileMode.Create))
        {
            data.UpdateData(Savepos);
            BinaryFormatter serilaizer = new BinaryFormatter();
            serilaizer.Serialize(f, data);
            Debug.Log("序列化成功!");
        }
    }
    public static bool isHaveData()
    {
         
         return File.Exists(Application.persistentDataPath + "/GameData.data");
        
    }
    public static void NewGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/GameData.data"))
        {
            return;
        }

        File.Delete(Application.persistentDataPath + "/GameData.data");
    }
    public static void Load()
    {
        if(!File.Exists(Application.persistentDataPath + "/GameData.data"))
        {
            return;
        }

        using (FileStream f = new FileStream(Application.persistentDataPath + "/GameData.data", FileMode.Open))
        {
            BinaryFormatter serilaizer = new BinaryFormatter();
          data=(GameData)  serilaizer.Deserialize(f);
            foreach (var item in ConfigManager.shopitemconfig.Datas)
            {
                item.amount = data.ShopItemDatasAmount[item.ID];
            } 
            Debug.Log("反序列化成功!");
        }
    }
}

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

    public SerilizedVector3 _playerpos=new SerilizedVector3();
    public PlayerInfo info;
    public void UpdateData()
    {
        info = PlayerInfoController._instance.pi;
        _playerpos .Copy( game.Scene._instance.player.transform.position);
    }
}
public class SaveData 
{
    public static GameData data=new GameData();
    public  static  void Save()
    {
         
        using (FileStream f = new FileStream(Application.persistentDataPath+"/GameData.data", FileMode.Create))
        {
            data.UpdateData();
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
           
            Debug.Log("反序列化成功!");
        }
    }
}

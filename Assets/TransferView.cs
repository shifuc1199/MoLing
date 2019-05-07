using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransferView : View
{
  public  int index = 0;
    public GameObject[] Points;
   
    List<GameObject> SavedDoors=new List<GameObject>();
    public bool isSavePos(int i)
    {
        return game.Scene._instance.DoorDic[i];
    }
    private void OnEnable()
    {

        InitDoorUI();
    }
 
    
    void InitDoorUI()
    {
        for (int i = 1; i < Points.Length+1; i++)
        {

            if (!game.Scene._instance.DoorDic[i])
            {
             
                Points[i-1].GetComponent<Image>().color = Color.grey;
            }
            else
            {
                if(!SavedDoors.Contains(Points[i - 1]))
                SavedDoors.Add(Points[i-1]);

                Points[i-1].GetComponent<Image>().color = Color.white;
            }
        }
        SavedDoors[index].GetComponent<Image>().color = Color.red;
    }
    public void Next()
    {
        SavedDoors[index].GetComponent<Image>().color = Color.white;
        index++;
        index %= SavedDoors.Count;

        SavedDoors[index].GetComponent<Image>().color = Color.red;
    }
    public void Last()
    {
        SavedDoors[index].GetComponent<Image>().color = Color.white;
        index--;
        if(index<0)
        {
            index = SavedDoors.Count-1;
        }

        SavedDoors[index].GetComponent<Image>().color = Color.red;
    }
    public GameObject GetDoorById(int index)
    {
        for (int i = 0; i < game.Scene._instance.SaveDoors.Length; i++)
        {
            if(game.Scene._instance.SaveDoors[i].GetComponent<SaveDoor>().id==index)
            {
                return game.Scene._instance.SaveDoors[i];
            }
        }
        return null;
    }
    public void Sure()
    {
        SaveData.Save();
        UIManager._instance.OpenView<MaskView>();
        Timer.Register(1, () => { game.Scene._instance.player.transform.position =new Vector3(GetDoorById(index + 1).transform.position.x, GetDoorById(index + 1).transform.position.y, game.Scene._instance.player.transform.position.z) ; });
        base.OnCloseClick();
    }
    private void Update()
    {
      
    }
}

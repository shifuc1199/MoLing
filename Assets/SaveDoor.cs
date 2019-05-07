using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDoor : MonoBehaviour, IInteractClick
{
    public int id;
 
    public GameObject button;
    public void InteractClick()
    {


        UIManager._instance.OpenView<TransferView>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            button.SetActive(true);
            if (game.Scene._instance.DoorDic[id])
                return;

            game.Scene._instance.DoorDic[id] = true;
            SavePos();
           
           
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            button.SetActive(false);
            if (game.Scene._instance.DoorDic[id])
                return;

            game.Scene._instance.DoorDic[id] = true;
            SavePos();
        }
    }
    public void SavePos()
    {
        UIManager._instance.GetView<TransferView>().index = id-1;
        SaveData.Save();
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (game.Scene._instance.DoorDic[id])
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

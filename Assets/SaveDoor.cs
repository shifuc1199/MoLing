using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDoor : MonoBehaviour, IInteractClick
{
    public int id;
 
    public GameObject button;
    public void InteractClick()
    {
        

        game.Scene._instance.DoorDic[id] = true;
        SavePos();
        button.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if (game.Scene._instance.DoorDic[id])
                return;
          
            button.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (game.Scene._instance.DoorDic[id])
                return;

            button.SetActive(false);
        }
    }
    public void SavePos()
    {
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

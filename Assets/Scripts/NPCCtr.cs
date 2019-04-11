using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractClick
{
    void InteractClick();
}
public class NPCCtr : MonoBehaviour, IInteractClick
{
    public bool isCanRepeatTalk;
    public int ID;
    bool isTalkOneTime = false;
    public GameObject btn;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(isCanRepeatTalk|| !isTalkOneTime)
            btn.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            btn.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractClick()
    {
        NPC npc = ConfigManager.npc_config.npcs.Find((a) => { return a.ID == ID; });
        btn.SetActive(isCanRepeatTalk);
        DialogView view = UIManager._instance.OpenView<DialogView>();
        view.SetContenct(npc._callback_name,npc.talks.ToArray());
        isTalkOneTime = true;
    }
}

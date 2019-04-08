using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RiddleTrigger : MonoBehaviour, IInteractClick
{
    public int ID ;
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
     RiddleGameCtr.gamectr. RiddleDictionry.Add(ID, new RiddleGame(ID, () => { Timer.Register(0.5f, () => { GetComponent<Animator>().SetTrigger("close"); }); }));
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player" )
        {
            transform.GetChild(0).gameObject.SetActive(!RiddleGameCtr.gamectr.RiddleDictionry[GetComponent<RiddleTrigger>().ID].isComplete);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
 
    }

    public void InteractClick()
    {
        if (!UIManager._instance.IsOpening<NumberGameView>())
        {

            UIManager._instance.OpenView<NumberGameView>();
            RiddleGameCtr.gamectr.CreateMap(GetComponentInParent<RiddleTrigger>().ID);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class Number : MonoBehaviour
{
    public int type;
    public int x;
    public int y;

    public Vector2 startpos;
    // Start is called before the first frame update
    private void Awake()
    {
        startpos = GetComponent<RectTransform>().anchoredPosition;
    }
    public void ResetNumber()
    {
        this.y = (int)((startpos.x - GameCtr.gamectr.start) / GameCtr.gamectr.offset);
        this.x = (int)((-startpos.y - GameCtr.gamectr.start) / GameCtr.gamectr.offset);
        GetComponent<RectTransform>().anchoredPosition = startpos;
    }
    void Start()
    {

         
        this.y = (int)((GetComponent<RectTransform>().anchoredPosition.x - GameCtr.gamectr.start) / GameCtr.gamectr.offset);
        this.x = (int)((-GetComponent<RectTransform>().anchoredPosition.y - GameCtr.gamectr.start) / GameCtr.gamectr.offset);
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
     
        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>((t) => {GameCtr.gamectr. FindEmpty(x, y); });
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

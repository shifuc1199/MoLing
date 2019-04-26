using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum BossType
{
    鱼,
    默灵
}
public class Trigger : MonoBehaviour
{
    public BossType type;
    public GameObject  Boss;
    public GameObject[] Walls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetTrigger()
    {
        foreach (var item in Walls)
        {
            item.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if (Boss.activeSelf)
                return;

            switch (type)
            {
                case BossType.鱼:
                    UIManager._instance.GetView<StartView>().SetTitle("邪恶之鱼");
                    AudioManager._instance.PlayBgm("鱼Boss");
                    break;
                case BossType.默灵:
                    UIManager._instance.GetView<StartView>().SetTitle("上代默灵");
                    AudioManager._instance.PlayBgm("Boss");
                    break;
                default:
                    break;
            }
         
           
            Boss.SetActive(true);
            foreach (var item in Walls)
            {
                item.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

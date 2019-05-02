using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public   class NPCManager : MonoBehaviour
{
    
    public GameObject RunAwayWall;
    public static NPCManager _intance;
    public Dictionary<string, TalkCallBack> Npc_CompleteCallBack = new Dictionary<string, TalkCallBack>();
    private void Awake()
    {
        _intance = this;
    }
    private void Start()
    {
        Npc_CompleteCallBack.Add("run_away", () => {
            RunAwayWall.SetActive(false);
            UIManager._instance.GetView<GameView>().Start_Run_Away();
            DOTween.Shake(() => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.1f, 0.2f).SetLoops(-1,LoopType.Yoyo);
        });

        Npc_CompleteCallBack.Add("sit_down_teach", () => {
            UIManager._instance.OpenView<TipView>().SetItem( ConfigManager.item_config.items.Find((a) => { return a.ID == "sitdown"; })); }
           ); 

              Npc_CompleteCallBack.Add("boss_start_talk", () => {




                  game.Scene._instance.Boss.GetComponent<Boss_Controller>().StartPk();
                  game.Scene._instance.Boss.GetComponent<Boss_Controller>().isTalk = true;

              }

       );
        Npc_CompleteCallBack.Add("boss_die_talk", () => {




            game.Scene._instance.Boss.GetComponent<Boss_Controller>().Died();

          
            
        }

       );
       
        Npc_CompleteCallBack.Add("shop_talk", () => {

            UIManager._instance.OpenView<ShopView>();
        }
            
         );
         
    }
}

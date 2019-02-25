using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateTemplate<FishBossAI>
{
    public IdleState(string id, FishBossAI p) : base(id, p)
    {

    }
    public override void OnEter()
    {
       
        Owner._anim.Play("FlashAppear");
        Timer.Register(Owner._anim.GetCurrentAnimatorClipInfo(0).Length-0.4f, OnUpdate);
    }
    public override void OnUpdate()
    {
        
        Owner._anim.Play("FishIdle");
    }
    public override void OnExit()
    {
      
        Owner._anim.Play("FlashDisappear");
    }

}

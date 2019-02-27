using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBossShootWaterState : StateTemplate<FishBossAI>
{

    public FishBossShootWaterState(string id, FishBossAI p) : base(id, p)
    {

    }
    public override void OnEter()
    {
        Owner._anim.Play("FlashAppear");
    }
    public override void OnUpdate()
    {
       if( Owner._anim.CurrentAnimComplete("FlashAppear"))
            Owner._anim.Play("FishWater");

        if (Owner._anim.CurrentAnimComplete("FishWater"))
            OnExit();

        //吐水球动作
        //if (!Owner._anim.GetCurrentAnimatorStateInfo(0).IsName("FishWater"))
        //{
        //    OnExit();
        //}
    }
    public override void OnExit()
    {
        Owner._machine.ChangeState("idle");
    }

}

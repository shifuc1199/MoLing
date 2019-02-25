using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWaterState : StateTemplate<FishBossAI>
{

    public ShootWaterState(string id, FishBossAI p) : base(id, p)
    {

    }
    public override void OnEter()
    {
        Owner._anim.Play("FlashAppear");
        Timer.Register(Owner._anim.GetCurrentAnimatorClipInfo(0).Length - 0.4f, OnUpdate);
    }
    public override void OnUpdate()
    {
        //吐水球动作
        Owner._anim.Play("FishWater");
    }
    public override void OnExit()
    {
        Owner._anim.Play("FlashDisappear");
    }

}

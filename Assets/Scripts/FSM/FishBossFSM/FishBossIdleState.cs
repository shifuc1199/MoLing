using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBossIdleState : StateTemplate<FishBossAI>
{
    public FishBossIdleState(string id, FishBossAI p) : base(id, p)
    {

    }
    public override void OnEter()
    {
        Owner._anim.Play("FlashAppear");
     



    }
    public override void OnUpdate()
    {
        if (Owner._anim.CurrentAnimComplete("FlashAppear"))
            Owner._anim.Play("FishIdle");
        
    }
    public override void OnExit()
    {
      
        
    }

}

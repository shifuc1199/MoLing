using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBoss_DashDownState : StateTemplate<FishBoss_Controller>
{
    Timer dashtimer;
    bool isdash = true;
    public bool isProduce = false;
    public FishBoss_DashDownState(string id, FishBoss_Controller p) :base(id,p)
        {

        }
    public override void OnEter()//进入的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        Timer.Register(1, () =>
        {
        
            Owner.transform.position = new Vector3(Owner.player.transform.position.x, Owner.dash_y+Owner.transform.parent.position.y, Owner.transform.position.z);
            Owner._anim.SetTrigger("dash");

            dashtimer = Timer.Register(1f, () => { isdash = false; });
        });
       
    }
    public override void OnUpdate()//每帧的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (!isdash)
            Owner.transform.Translate(-Owner.transform.up *2);
       
    }
    public override void OnExit()//退出的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (dashtimer!=null)
        dashtimer.Cancel();
           isdash = true;
        Owner._anim.SetTrigger("disappear");
        isProduce = false;
    }
}

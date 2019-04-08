using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AirAttackState : StateTemplate<Boss_Controller>
{
    Timer airattacktimer;
    bool isairattack = false;
    public Boss_AirAttackState(string id, Boss_Controller p) : base(id, p)
    {

    }
    public override void OnEter()//进入的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;

        Timer.Register(1, () =>
        {

           // Owner.transform.position = new Vector3(Owner.player.transform.position.x, Owner.dash_y + Owner.transform.parent.position.y, Owner.transform.position.z);
            Owner._anim.SetTrigger("appear");
            airattacktimer = Timer.Register(1f, () => { isairattack = true; Owner._anim.SetTrigger("airattack"); });
        });

    }
    public override void OnUpdate()//每帧的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (!isairattack)
            return;


    }

    public override void OnExit()//退出的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        isairattack = false;

          Owner._anim.SetTrigger("disappear");
    }
}

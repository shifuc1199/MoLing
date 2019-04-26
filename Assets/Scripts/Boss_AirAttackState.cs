using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AirAttackState : StateTemplate<Boss_Controller>
{
    int index;
    Timer airattacktimer;
    bool isairattack = false;
    public Boss_AirAttackState(string id, Boss_Controller p) : base(id, p)
    {

    }
    public override void OnEter()//进入的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (Owner.isToSecond)
            return;
        Timer.Register(1, () =>
        {
            index = Owner.GetDashPointIndex();
            Owner.transform.eulerAngles = new Vector3(0, 180 * (index - 1), 0);
            Owner.transform.position = Owner.dashpos[index].position+new Vector3(0,2);
            airattacktimer = Timer.Register(1f, () => { Timer.Register(0.75f, () => { isairattack = true; });  Owner._anim.SetTrigger("airattack"); });
        });

    }
    public override void OnUpdate()//每帧的时候执行
    { 
        if (Owner._hurtcontroller.isdie)
            return;

        if (Owner.isToSecond)
            return;
        if (!isairattack)
            return;

        Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, Owner.ToSecondPos.position+new Vector3(1.5f,-4f), 10f*Time.deltaTime);

    }

    public override void OnExit()//退出的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        isairattack = false;

       //   Owner._anim.SetTrigger("disappear");
    }
}

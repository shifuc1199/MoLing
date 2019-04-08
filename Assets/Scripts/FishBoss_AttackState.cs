using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBoss_AttackState : StateTemplate<FishBoss_Controller>
{
    bool isattack = false;
    Timer attacktimer;
    int index;
    public FishBoss_AttackState(string id, FishBoss_Controller p) :base(id,p)
        {

        }
    public override void OnEter()//进入的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        Timer.Register(1, () =>
        {
            index = Owner.GetDashPointIndex();
            Owner.transform.eulerAngles = new Vector3(0, 180*index, 0);
            Owner.transform.position = Owner.dashpos[index].position;
            Owner._anim.SetTrigger("appear");
            attacktimer = Timer.Register(1f, () => { Owner._anim.SetTrigger("attack"); isattack = true; });
        });
       
    }
    public override void OnUpdate()//每帧的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (isattack )
        {
 
            if(Vector3.Distance(Owner.transform.position, Owner.dashpos[(index+1)%Owner.dashpos.Length].position) >= 1)
            {
                Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner.dashpos[(index+1) % Owner.dashpos.Length].position.x, Owner.transform.position.y), Time.deltaTime * Owner.dash_spped);

            }
            else
            {
                isattack = false;

            }
        }

         

    }
  
    public override void OnExit()//退出的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (attacktimer!=null)
            attacktimer.Cancel();

        isattack = false;

        Owner._anim.SetTrigger("disappear");
    }
}

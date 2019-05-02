using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BulletState : StateTemplate<Boss_Controller>
{
     
    public Boss_BulletState(string id, Boss_Controller p) : base(id, p)
    {

    }
    public override void OnEter()//进入的时候执行
    {
        if (Owner.isReset)
            return;
        
        if (Owner._hurtcontroller.isdie)
            return;

        Owner.transform.position = Owner.ToSecondPos.position;
        Owner.transform.rotation = Quaternion.identity;
        Owner._anim.SetTrigger("appear");
        Timer.Register(1.5f, () => 
        {

            if (Owner.isReset)
                return;
            if (Owner._hurtcontroller.isdie)
                return;
          

            Owner.InvokeRepeating("ShootButterFly", 0.5f, 2.5f);
            Owner._anim.SetTrigger("bullet"); });
      

    }
    public override void OnUpdate()//每帧的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (Owner.isReset)
            return;



    }

    public override void OnExit()//退出的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        Owner.CancelInvoke("ShootButterFly");
        Owner._anim.SetTrigger("bulletover");
    }
        
}

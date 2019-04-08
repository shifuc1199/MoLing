using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBoss_ShootState : StateTemplate<FishBoss_Controller>
{
    Timer timer;
    bool isEnter = false;
    public FishBoss_ShootState(string id, FishBoss_Controller p) :base(id,p)
        {

        }
    public override void OnEter()//进入的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        timer= Timer.Register(1, () => {
            isEnter = true;
            Owner.transform.position = Owner.shootmovepos[UnityEngine.Random.Range(0, Owner.shootmovepos.Length)].position;
            Owner._anim.SetTrigger("appear");Owner.InvokeRepeating("Shoot", 0, 2); });
    }
   
    public override void OnUpdate()//每帧的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (isEnter)
        {
            if (Owner.transform.position.x > Owner.player.transform.position.x)
            {
                Owner.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    public override void OnExit()//退出的时候执行
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (timer != null)
            timer.Cancel();
        Owner.CancelInvoke("Shoot");
        Owner._anim.SetTrigger("disappear");
    }
}

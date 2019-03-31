using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_2_ShootState : StateTemplate<Enemy_2_Controller>
{
    int index = 0;
    public Enemy_2_ShootState(string id, Enemy_2_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {
      
        Owner.InvokeRepeating("ShootAnim", 0, 3f);

    }
    public override void OnUpdate()
    {
        if (Owner._hurtcontroller.isdie)
            return;


         


    }
   
    public override void OnExit()
    {

        Owner.CancelInvoke();
    }
}
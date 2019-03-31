using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_4_ProduceState : StateTemplate<Enemy_4_Controller>
{
    public Enemy_4_ProduceState(string id, Enemy_4_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {
        if (Owner._hurtcontroller.isdie)
            return;

       
    }
     
    public override void OnUpdate()
    {
        if (Owner._hurtcontroller.isdie)
            return;

          

    }
    public override void OnExit()
    {


    }
}

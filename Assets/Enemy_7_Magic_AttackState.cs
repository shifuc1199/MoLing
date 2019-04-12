using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7_Magic_AttackState : StateTemplate<Enemy_7_Magic_Controller>
{
     

    public Enemy_7_Magic_AttackState(string id, Enemy_7_Magic_Controller p) : base(id, p)
    {


    }
    public override void OnEter()
    {
        Owner.InvokeRepeating("ShootMagic",2,2);
    }
    
    public override void OnUpdate()
    {
         

    }
    public override void OnExit()
    {
      


    }
}

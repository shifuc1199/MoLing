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
       
        Debug.Log("123");
        Owner.InvokeRepeating("ShootMagic",0,2);
    }
    
    public override void OnUpdate()
    {

        if (Owner._player.position.x < Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Owner.transform.rotation = Quaternion.identity;
        }
    }
    public override void OnExit()
    {
        Owner.CancelInvoke();

    }
}

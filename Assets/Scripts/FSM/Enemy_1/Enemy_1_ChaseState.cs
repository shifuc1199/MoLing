using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_ChaseState : StateTemplate<Enemy_1_Controller>
{

    public Enemy_1_ChaseState(string id, Enemy_1_Controller p) : base(id, p)
    {

    }
    public override void OnEter( )
    {
          


    }
    public override void OnUpdate()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (Mathf.Abs(Mathf.Abs(Owner.transform.position.x) -Mathf.Abs( Owner._player.transform.position.x)) > 15)
        {
            Owner._anim.SetBool("run", false);
            return;
        }
             
        Owner._anim.SetBool("run", true);
        Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner._player.transform.position.x, Owner.transform.position.y, Owner.transform.position.z), 0.1f);
        if(Owner._player.transform.position.x>Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.identity;
        }
        else
        {
            Owner.transform.rotation = Quaternion.Euler(0,180,0);
        }

    }
    public override void OnExit()
    {

        
    }
    
}

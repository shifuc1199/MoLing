using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy_4_ChaseState : StateTemplate<SmallEnemy_4_Controller>
{
    public SmallEnemy_4_ChaseState(string id, SmallEnemy_4_Controller p) : base(id, p)
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
 
        if (Vector3.Distance(Owner.transform.position,Owner._player.transform.position) <= 10)
        {
            if (Owner._player.transform.position.x > Owner.transform.position.x)
            {
                Owner.transform.rotation = Quaternion.identity;
            }
            else
            {
                Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, Owner._player.transform.position, Owner._speed*Time.deltaTime);
        }
       
    }
    public override void OnExit()
    {
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_6_AttackState : StateTemplate<Enemy_6_Controller>
{

    public bool isattack;
    public Enemy_6_AttackState(string id, Enemy_6_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {


        Owner._anim.SetBool("walk", false);
        Owner._anim.SetBool("attack", false);
        Owner._anim.SetBool("run", false);
        Owner._anim.SetTrigger("crazy");
    }

    public override void OnUpdate()
    {
      
        if (isattack)
            return;
     
        if (Owner._hurtcontroller.isdie)
            return;

        if (Owner._anim.IsAnim("鱼人狂暴"))
            return;

        if (Owner._player.transform.position.x > Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Owner.transform.rotation = Quaternion.identity;
        }

        if (Mathf.Abs(Mathf.Abs(Owner.transform.position.y) - Mathf.Abs(Owner._player.transform.position.y)) > 0.5f)
        {
            Owner._anim.SetBool("run", false);
            Owner._anim.SetBool("attack", false);
            Owner._anim.SetBool("walk", false);
            return;
        }

        if (Mathf.Abs(Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner._player.transform.position.x)) <=4.5f)
        {
            isattack = true;
           
            Owner._anim.SetBool("run", false);
            Owner._anim.SetBool("walk", false);
            Owner._anim.SetBool("attack", true);
        }
        else if (!isattack)   
        {
            Owner._anim.SetBool("attack", false);
            Owner._anim.SetBool("run", true);
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner._player.transform.position.x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime * 2);

        }



    }

    public override void OnExit()
    {
        if (Owner._player.position.x > Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Owner.transform.rotation = Quaternion.identity;
        }
        Owner._anim.SetTrigger("taunt");
       ( Owner._machine.GetState("partol") as Enemy_6_PartolState).isTaunt = true;
        Owner._anim.SetBool("run", false);
    }
}

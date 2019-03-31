using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5_AttackState : StateTemplate<Enemy_5_Controller>
{
    float aim_x;
    Timer timer;
    bool isarrive = false;
    public Enemy_5_AttackState(string id, Enemy_5_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {

        Debug.Log("进攻！");
        Enemy_Dash();
    }
   public void Enemy_Dash()
    {
        isarrive = false;
       
        Owner._anim.SetBool("run", true);
        if (Owner._player.transform.position.x < Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.identity;
        }
        else
        {
            Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        aim_x = -Owner.transform.right.x * Owner.dash_x + Owner.transform.position.x;

    }
    public override void OnUpdate()
    {
         
        if (Owner._hurtcontroller.isdie)
            return;
        if (isarrive)
            return;

       if (Mathf.Abs( Owner.transform.position.x-aim_x)<=0.5f)
        { 
            isarrive = true;
            Owner._anim.SetBool("run", false);
            timer= Timer.Register(2, () => { Enemy_Dash(); });
           
        }
        else
        {
           
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(aim_x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime * 2);
        }
    }

    public override void OnExit()
    {
        Owner._anim.SetBool("run", false);
        if (timer!=null)
            timer.Cancel();
    }
}

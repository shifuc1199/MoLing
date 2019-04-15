using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7_AttackState : StateTemplate<Enemy_7_Controller>
{
    

    public Enemy_7_AttackState(string id, Enemy_7_Controller p) : base(id, p)
    {


    }
    float aim_x;
    Timer timer;
    bool isarrive = true;
   
    public override void OnEter()
    {

        isarrive = true;
        Enemy_Dash();
    }
    public void Enemy_Dash()
    {
        
        if (Owner == null)
            return;
        if (Owner._hurtcontroller.isdie)
            return;
        Owner._anim.SetBool("attack", true);
        Timer.Register(1, () => { isarrive = false; });
        if (Owner._player.transform.position.x > Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.identity;
        }
        else
        {
            Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        aim_x = Owner.transform.right.x * Owner.dash_x + Owner.transform.position.x;

    }

    public override void OnUpdate()
    {

        if (Owner._hurtcontroller.isdie)
            return;
        if (isarrive)
            return;

        if (Mathf.Abs(Owner.transform.position.x - aim_x) <= 0.5f)
        {
            isarrive = true;
            Owner._anim.SetBool("attack", false);
            timer = Timer.Register(1, () => { Enemy_Dash(); });

        }
        else
        {

            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(aim_x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime * 2);
        }
    }

    public override void OnExit()
    {
        Owner._anim.SetBool("attack", false);
        if (timer != null)
            timer.Cancel();
    }
}

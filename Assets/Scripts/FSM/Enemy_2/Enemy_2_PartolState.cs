using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_PartolState : StateTemplate<Enemy_2_Controller>
{
    int index;
    public Enemy_2_PartolState(string id, Enemy_2_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {



    }
    bool isarrive = false;
    public override void OnUpdate()
    {


        if (Owner._hurtcontroller.isdie)
            return;

        if (isarrive)
            return;

        Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner.points[index].position.x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime);
        Owner._anim.SetBool("walk", true);

        if (Mathf.Abs(Owner.transform.position.x - Owner.points[index].position.x) <= 0.5f)
        {
            isarrive = true;
            Owner._anim.SetBool("walk", false);
            index++;
            index %= Owner.points.Length;
            Timer.Register(2, () =>
            {
                if (Owner == null)
                    return;
                isarrive = false;
                if (Owner.points[index].position.x > Owner.transform.position.x)
                {
                    Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    Owner.transform.rotation = Quaternion.identity;
                }
            });
        }
        
    }

    public override void OnExit()
    {


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_ChaseState : StateTemplate<Enemy_1_Controller>
{

    int index;
    public Enemy_1_ChaseState(string id, Enemy_1_Controller p) : base(id, p)
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
        Owner._anim.SetBool("run", true);
 
        if (Mathf.Abs(Owner.transform.position.x - Owner.points[index].position.x) <= 0.5f)
        {
            isarrive = true;
            Owner._anim.SetBool("run", false);
            index++;
            index %= Owner.points.Length;
            Timer.Register(2, () =>
            {
                isarrive = false;
                if (Owner == null)
                    return;
                if (Owner.points[index].position.x < Owner.transform.position.x)
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

}

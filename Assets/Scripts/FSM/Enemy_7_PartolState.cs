using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_7_PartolState : StateTemplate<Enemy_7_Controller>
{
   
    int index = 0;
 
    public Enemy_7_PartolState(string id, Enemy_7_Controller p) : base(id, p)
    {


    }
    public override void OnEter()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        isarrive = false;
        if (Owner.points[index].position.x < Owner.transform.position.x)
        {
            Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Owner.transform.rotation = Quaternion.identity;
        }
      
    }
    bool isarrive = false;
    public override void OnUpdate()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (isarrive)
            return;
         

        if (Mathf.Abs(Owner.transform.position.x - Owner.points[index].position.x) <= 0.5f)
        {
          

            Owner._anim.SetBool("walk", false);
            isarrive = true;
            index++;
            index %= Owner.points.Length;
            Timer.Register(1.5f, () =>
            {
                if (Owner == null)
                    return;
                isarrive = false;
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
        else
        {
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner.points[index].position.x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime);
            Owner._anim.SetBool("walk", true);

        }

    }
    public override void OnExit()
    {
        Owner._anim.SetBool("walk", false);
        
        
    }
}

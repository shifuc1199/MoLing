using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_5_PartolState : StateTemplate<Enemy_5_Controller>
{
    Timer awaketimer;
    int index=0;
    public bool isSleep;
    public Enemy_5_PartolState(string id, Enemy_5_Controller p) : base(id, p)
    {
      

    }
    public override void OnEter()
    {
        if (Owner._hurtcontroller.isdie)
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
        Debug.Log("巡逻！");
    }
    bool isarrive = false;
    public override void OnUpdate()
    {
      
        if (Owner._hurtcontroller.isdie)
            return;
        if (isarrive)
            return;
        if (index < 0||index>=Owner.points.Length)
            return;
        
        if (Mathf.Abs(Owner.transform.position.x - Owner.points[index].position.x) <= 0.5f)
        {
             

            Owner._anim.SetBool("walk", false);
            isarrive = true;
            index++; 
            index %= Owner.points.Length;
            int range = Random.Range(0, 2);
            float timer = 2;
            if (range == 0)
            {
                isSleep = true;
                Owner._anim.SetTrigger("sleep");
               timer = Random.Range(5.0f, 10.0f);
                awaketimer = Timer.Register(timer - 1, () => { if (Owner._anim != null) { Owner._anim.SetTrigger("wake"); }
                   isSleep = false;
                });
            }
      

            Timer.Register(timer, () =>
            {
               
                isarrive = false;
                if (Owner == null)
                    return;
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
        else
        {
            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner.points[index].position.x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime);
            Owner._anim.SetBool("walk", true);

        }

    }
    public override void OnExit()
    {
        Owner._anim.SetBool("walk", false);
        isSleep = false;
        if(awaketimer!=null)
        awaketimer.Cancel();
    }
}

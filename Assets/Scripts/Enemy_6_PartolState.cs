using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_6_PartolState : StateTemplate<Enemy_6_Controller>
{
    int index = 0;
    public bool isTaunt = false;
    public Enemy_6_PartolState(string id, Enemy_6_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {

        if (Owner._hurtcontroller.isdie)
            return;

        isarrive = false;

        

        Owner._anim.SetBool("walk", false);
        Owner._anim.SetBool("attack", false);
        Owner._anim.SetBool("run", false);

        isTaunt = true;
    }
    bool isarrive = false;
    Timer timer;
    public override void OnUpdate()
    {

        if (isTaunt)
            return;
        
          

        if (Owner._hurtcontroller.isdie)
            return;
        if (isarrive)
            return;
        if (index < 0 || index >= Owner.points.Length)
            return;
        if (Mathf.Abs(Owner.transform.position.x - Owner.points[index].position.x) <= 0.5f)
        {
            //int range = Random.Range(0, 2);
            //if (range == 0)
            //{
            //    Owner._anim.SetTrigger("crazy");

            //}

            Owner._anim.SetBool("walk", false);
            isarrive = true;
            index++;
            index %= Owner.points.Length;

            timer= Timer.Register(2, () =>
            {
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
                isarrive = false;
               
            });
        }
        else
        {

            Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, new Vector3(Owner.points[index].position.x, Owner.transform.position.y, Owner.transform.position.z), Owner._speed * Time.deltaTime);
            Owner._anim.SetBool("walk", true);
            if (Owner.points[index].position.x > Owner.transform.position.x)
            {
                Owner.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                Owner.transform.rotation = Quaternion.identity;
            }
        }

    }
    public override void OnExit()
    {
        if(timer!=null)
        timer.Cancel();

    }
}

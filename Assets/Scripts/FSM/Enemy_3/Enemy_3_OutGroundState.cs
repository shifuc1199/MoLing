using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3_OutGroundState : StateTemplate<Enemy_3_Controller>
{
    public Enemy_3_OutGroundState(string id, Enemy_3_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        Owner._anim.SetTrigger("out");

    }
    
    public override void OnUpdate()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (Mathf.Abs(Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner._player.transform.position.x)) <=10)
        {
            if(!Owner.IsInvoking())
            Owner.InvokeRepeating("Attack", .5f, 2);
        }
        else if (Mathf.Abs(Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner._player.transform.position.x))> 20)
        {
            Owner.CancelInvoke();
            Owner._machine.ChangeState("in");

        } 
         

    }
    public override void OnExit()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        Owner._anim.SetTrigger("back");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3_InGroundState : StateTemplate<Enemy_3_Controller>
{
    public Enemy_3_InGroundState(string id, Enemy_3_Controller p) : base(id, p)
    {

    }
    public override void OnEter()
    {

         

    }
    
    public override void OnUpdate()
    {
        if (Owner._hurtcontroller.isdie)
            return;
        if (Mathf.Abs(Mathf.Abs(Owner.transform.position.x) - Mathf.Abs(Owner._player.transform.position.x)) <=20)
        {
            Owner._machine.ChangeState("out");
        }
        else
        {

        }
         

    }
    public override void OnExit()
    {
         

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

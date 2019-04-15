using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7_SlashState : StateTemplate<Enemy_7_Slash_Controller>
{
    public Enemy_7_SlashState(string id, Enemy_7_Slash_Controller p) : base(id, p)
    {


    }
    public override void OnEter()
    {
        Owner.InvokeRepeating("SlashAttack", 0, 2);
    }

    public override void OnUpdate()
    { 
    }
    public override void OnExit()
    {
        Owner.CancelInvoke();
    }
}

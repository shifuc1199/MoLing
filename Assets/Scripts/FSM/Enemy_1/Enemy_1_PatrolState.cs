using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_PatrolState : StateTemplate<Enemy_1_Controller>
{

    public Enemy_1_PatrolState(string id, Enemy_1_Controller p) : base(id, p)
    {

    }

    public override void OnEter()
    {
        OnUpdate();


    }
    public override void OnUpdate()
    {

       

    }
    public override void OnExit()
    {


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class FSMState
{
    
    public string id;
    public FSMState(string id)
    {
        this.id = id;
    }
    public virtual void OnEter()
    {
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }
}

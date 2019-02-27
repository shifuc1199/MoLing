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
    public virtual void OnEter()//进入的时候执行
    {

    }
    public virtual void OnUpdate()//每帧的时候执行
    {

    }
    public virtual void OnExit()//退出的时候执行
    {

    }
}

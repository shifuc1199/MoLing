using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Controller : EnemyBase
{ 
    public Transform _player;
    public FSMMachines _machine=new FSMMachines();
    
    // Use this for initialization
   new void Start ()
    {
        base.Start();
        _machine.RegisterState(new Enemy_1_ChaseState("chase", this));
        _machine.ChangeState("chase");
        _hurtcontroller._DieCallBack = new DieCallBack(() => { Destroy(gameObject); });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => { GetComponent<Rigidbody2D>().AddForce(-transform.right * 30, ForceMode2D.Impulse); });
    }
	
	// Update is called once per frame
	void Update () {
        _machine.m_curretstate.OnUpdate();
	}
}

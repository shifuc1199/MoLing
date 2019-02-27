using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FishBossAI : EnemyBase
{
    public FSMMachines _machine = new FSMMachines();
    [NonSerialized]
    public Animator _anim;
	// Use this for initialization
	new void Start ()
    {
        base.Start();
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => { GetComponent<Rigidbody2D>().AddForce(transform.right * 30, ForceMode2D.Impulse); });
        _anim =GetComponentInChildren<Animator>();
        _machine.RegisterState(new FishBossIdleState("idle", this));
        _machine.RegisterState(new FishBossShootWaterState("shootwater", this));
        _machine.ChangeState("idle");
    }
    private void LateUpdate()
    {
        _machine.Update();
    }
    // Update is called once per frame
    void Update () {
       
		if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _machine.ChangeState("shootwater");
        }
    }
}

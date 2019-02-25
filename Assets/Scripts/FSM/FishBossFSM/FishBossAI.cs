using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FishBossAI : MonoBehaviour
{
    public FSMMachines _machine = new FSMMachines();
    [NonSerialized]
    public Animator _anim;
	// Use this for initialization
	void Start () {
        _anim=GetComponentInChildren<Animator>();
        _machine.RegisterState(new IdleState("idle", this));
        _machine.RegisterState(new ShootWaterState("shootwater", this));
        _machine.ChangeState("idle");
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _machine.ChangeState("shootwater");
        }
	}
}

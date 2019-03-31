using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Controller : EnemyBase
{ 
  
    public float _speed;
    public Animator _anim;
    public Transform[] points;
    // Use this for initialization
   new void Start ()
    {
        base.Start();
        _anim = GetComponentInChildren<Animator>();
        _machine.RegisterState(new Enemy_1_ChaseState("chase", this));
        _machine.ChangeState("chase");
        _hurtcontroller._DieCallBack = new DieCallBack(() => { GetComponent<BoxCollider2D>().enabled = false; GetComponent<Rigidbody2D>().gravityScale = 0; _anim.SetTrigger("die"); Destroy(gameObject,1.5f); });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);


            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
        });
    }
	
	// Update is called once per frame
	 
}

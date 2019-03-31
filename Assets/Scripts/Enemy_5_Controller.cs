using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5_Controller : EnemyBase
{
    public float _speed;
    public Transform _player;
    public Transform[] points;
    public Animator _anim;
    public BoxCollider2D trigger;
    public float dash_x;
   
    // Start is called before the first frame update
    new void Start()
    {
        _anim = GetComponent<Animator>();
        base.Start();
        _machine.RegisterState(new Enemy_5_PartolState("partol", this));
        _machine.RegisterState(new Enemy_5_AttackState("attack", this));
        _machine.ChangeState("partol");
        _hurtcontroller._DieCallBack = () => {
            GetComponent<BoxCollider2D>().enabled = false; GetComponent<Rigidbody2D>().gravityScale = 0;
            _anim.SetBool("walk",false);
            _anim.SetBool("run",false);
            _anim.SetTrigger("die");
            trigger.enabled = false;
            Destroy(gameObject, 1.5f); };
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {
            if(_machine.m_curretstate!=_machine.GetState("attack"))
            _machine.ChangeState("attack");
         
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position,Quaternion.identity);
 
            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
    }

    
}

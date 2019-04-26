using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_6_Controller : EnemyBase
{
    public float _speed;
    public Transform _player;
    public Transform[] points;
    public Animator _anim;
    public BoxCollider2D trigger;
    // Start is called before the first frame update
    new void Start()
    {
        _anim = GetComponent<Animator>();
        base.Start();
        _machine.RegisterState(new Enemy_6_PartolState ("partol", this));
        _machine.RegisterState(new Enemy_6_AttackState("attack", this));
        _machine.ChangeState("partol");
        _hurtcontroller._DieCallBack = () => { GetComponent<Rigidbody2D>().gravityScale = 0; GetComponent<BoxCollider2D>().enabled = false; _anim.SetTrigger("die"); Destroy(gameObject, 1.5f); };
    
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {

            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);
        //    _anim.SetTrigger("hurt");

            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
    }
    public void AttackSetEnable()
    {
        
        trigger.enabled = true;
    }
    public void OnTauntExit()
    {
        (_machine.GetState("partol") as Enemy_6_PartolState).isTaunt = false;
    }
    public void AttackSetDisable()
    {
        
        (_machine.GetState("attack") as Enemy_6_AttackState).isattack = false;
        trigger.enabled = false;
    }
}

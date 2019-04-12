using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7_Magic_Controller : EnemyBase
{
    public float _speed;
    public Animator _anim;
    public Transform[] points;
    public Transform _player;
     
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _machine.RegisterState(new Enemy_7_Magic_AttackState("attack", this));
        _machine.RegisterState(new Enemy_7_Magic_PartolState("partol", this));
        _machine.ChangeState("attack");

        _hurtcontroller._HurtCallBack += () =>
        {
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
        };
        _hurtcontroller._DieCallBack += () =>
        {
            GetComponent<BoxCollider2D>().enabled = false; GetComponent<Rigidbody2D>().gravityScale = 0;
            _anim.SetTrigger("die");
            Destroy(gameObject, 1.5f);
        };

    }
    public void CreateFireBall()
    {
 
        GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("FireBall", transform.position+transform.right*2+transform.up, transform.localRotation);
        
        GameObjectPool.GetInstance().ReleaseGameObject("FireBall", temp2, 1f);
    }
    void ShootMagic()
    {
        _anim.SetTrigger("attack");
    }

}

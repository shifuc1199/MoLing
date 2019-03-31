using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3_Controller : EnemyBase
{
    public Animator _anim;
    public Transform _player;
    // Start is called before the first frame update
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    new   void Start()
    {
        base.Start();
        _machine.RegisterState(new Enemy_3_OutGroundState("out",this));
        _machine.RegisterState(new Enemy_3_InGroundState("in", this));
        _machine.ChangeState("in");
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {

          
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position+new Vector3(0,1.5f,0), Quaternion.identity);


            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
        _hurtcontroller._DieCallBack = new DieCallBack(() => {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            _anim.SetTrigger("die");
            Destroy(gameObject, 2);

        });
    }
    void Attack()
    {
          _anim.SetTrigger("attack");
    }
    // Update is called once per frame
     
}

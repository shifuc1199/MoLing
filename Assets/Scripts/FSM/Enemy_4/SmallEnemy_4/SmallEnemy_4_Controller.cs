using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SmallEnemy_4_Controller : EnemyBase
{
    public Transform _player;
    public float _speed;
    public Enemy_4_Controller father;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale =0;
        if (_hurtcontroller!=null)
        {
            _hurtcontroller.isdie = false;
            _hurtcontroller.Health = _hurtcontroller.MaxHealth;
        }
    }
    new void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        _machine.RegisterState(new SmallEnemy_4_ChaseState("chase", this));
        _machine.ChangeState("chase");
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {

 

            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position,Quaternion.identity);
          
         //   temp2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
        _hurtcontroller._DieCallBack = new DieCallBack(
            () =>
        {

      


            GameObjectPool.GetInstance().ReleaseGameObject("小蜜蜂", gameObject,1);
            father.SmallBeeAmount--;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 40;
          

         


        });
    }

    // Update is called once per frame
    
}

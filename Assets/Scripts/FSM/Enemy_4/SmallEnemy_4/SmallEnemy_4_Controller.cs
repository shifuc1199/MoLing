using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy_4_Controller : EnemyBase
{
    public Transform _player;
    public float _speed;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _machine.RegisterState(new SmallEnemy_4_ChaseState("chase", this));
        _machine.ChangeState("chase");
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {


            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform);
            temp2.transform.localPosition = new Vector3(0f, 0f, 0);
            temp2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
    }

    // Update is called once per frame
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_Controller : EnemyBase
{
    public Animator _anim;
    // Start is called before the first frame update
  new  void Start()
    {
        _anim = GetComponent<Animator>();
        base.Start();
        _hurtcontroller._DieCallBack = new DieCallBack(() => { Destroy(gameObject,1.2f); _anim.SetTrigger("die"); });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {

            _anim.SetTrigger("hurt");
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform);
            temp2.transform.localPosition = new Vector3(0f, 0f, 0);
            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
    }

    // Update is called once per frame
    
}

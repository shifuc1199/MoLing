using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy_4_Controller : EnemyBase
{
  
  
    public int MaxSmallBeeAmount;
    public int SmallBeeAmount = 0;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
       
        _machine.RegisterState(new Enemy_4_ProduceState("produce", this));
        _machine.ChangeState("produce");

        _hurtcontroller._DieCallBack = new DieCallBack(() => { GetComponent<BoxCollider2D>().enabled = false; GetComponent<DOTweenPath>().DOPause(); GetComponent<Rigidbody2D>().gravityScale = 5; Destroy(gameObject.transform.parent.gameObject, 1.5f); });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() =>
        {
         
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);


            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
        });
    }
    public void Produce()
    {
        if (SmallBeeAmount >= MaxSmallBeeAmount)
            return;

        SmallBeeAmount++;
        GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("小蜜蜂", transform.position, Quaternion.identity);
        
        temp2.GetComponent<SmallEnemy_4_Controller>().father = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

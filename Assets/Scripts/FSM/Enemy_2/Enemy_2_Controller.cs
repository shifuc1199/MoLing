using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class Enemy_2_Controller : EnemyBase
{
    public float _speed;
    public AnimationCurve curve;
    public Animator _anim;
    public Transform[] points;
    public Transform player;
    // Start is called before the first frame update
  new  void Start()
    {
        _anim = GetComponent<Animator>();
        base.Start();
        _machine.RegisterState(new Enemy_2_PartolState("partol", this));
        _machine.RegisterState(new Enemy_2_ShootState("shoot", this));
        _machine.ChangeState("partol");
        _hurtcontroller._DieCallBack = new DieCallBack(() => { GetComponent<Rigidbody2D>().gravityScale = 0; GetComponent<PolygonCollider2D>().enabled = false; Destroy(gameObject,1.2f); _anim.SetTrigger("die"); });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {

            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);


            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
    }
    public void ShootAnim()
    {
        _anim.SetTrigger("shoot");
    }
    public void Shoot()
    {

        if (_hurtcontroller.isdie)
            return;
        Vector3[] pos = new Vector3[] {
            new Vector2(Mathf.Abs( transform.position.x-player.transform.position.x)/2+(transform.position.x>player.transform.position.x? player.transform.position.x:transform.position.x),
            (transform.position+transform.up*8).y),new Vector2(player.transform.position.x,transform.position.y-2)
        };

        float timer = Mathf.Sqrt(Mathf.Pow((transform.position + transform.right * 3 + transform.up * 8).magnitude, 2) + Mathf.Pow((player.transform.position-transform.position).x, 2))/150;

        GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("尸蛹攻击", transform.position+new Vector3(0,3.5f), Quaternion.Euler(0, 0, 90));
        GameObjectPool.GetInstance().ReleaseGameObject("尸蛹攻击", temp2, timer);

         
       TweenerCore<Vector3,DG.Tweening.Plugins.Core.PathCore.Path,DG.Tweening.Plugins.Options.PathOptions> core = temp2.transform.DOPath(pos, timer, PathType.CatmullRom, PathMode.TopDown2D);
        core.plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
        core.plugOptions.lookAhead = 0.1f;
        core.SetEase(curve);
    }
   
    // Update is called once per frame

}

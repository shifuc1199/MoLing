using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class FishBoss_Controller : EnemyBase
{
    public List<Timer> timers=new List<Timer>();
    public Transform player;
    [NonSerialized]
    public Animator _anim;
    public float dash_y;
    public float dash_spped;
    public Transform shootpos;
    public Transform[] dashpos;
    public Transform[] shootmovepos;
   
    public Transform diePos;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
      
        _anim = GetComponent<Animator>();
        _hurtcontroller._DieCallBack = new DieCallBack(() => {
            _anim.SetTrigger("disappear");
            CancelInvoke();
            Timer.Register(1, () => { transform.position = new Vector3(diePos.position.x, diePos.position.y+15); _anim.SetTrigger("die"); GetComponent<BoxCollider2D>().enabled = false;Timer.Register(0.5f, () => { transform.DOMoveY(diePos.position.y, 0.5f).SetEase(Ease.Linear);Timer.Register(0.5f, () =>
            {
               
                DOTween.Shake(() => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);

            });
            });  });
            Timer.Register(4.5f, () => { _anim.SetTrigger("diedisappear"); });
            Destroy(gameObject,5);
        });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {
        
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

        
            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);


        });
        _machine.RegisterState(new FishBoss_ShootState("shoot", this));
        _machine.RegisterState(new FishBoss_DashDownState("dashdown", this));
        _machine.RegisterState(new FishBoss_AttackState("attack", this));
        ReleaseSkill();
        
    }
    public void HideCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    
    }
    public void ShowCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;

    }
    int lastindex;
    public void ReleaseSkill()
    {
        int index = UnityEngine.Random.Range(0, 3);
        while(index== lastindex)
        {
            index = UnityEngine.Random.Range(0, 3);
        }
        string[] skillname = new string[] {"shoot","dashdown","attack" };
        switch (skillname[index])
        {
            case "shoot":
                Timer.Register(4
                    , () => { ReleaseSkill(); });
                break;
            case "dashdown":
                Timer.Register(2.5f, () => { ReleaseSkill(); });
                break;
            case "attack":
                Timer.Register(3.5f, () => { ReleaseSkill(); });
                break;
            default:
                break;
        }
        lastindex = index;
        Debug.Log("转状态:" + skillname[index]);
        _machine.ChangeState(skillname[index]);
    }
   
    public   void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer ==LayerMask.NameToLayer( "ground")&&_machine.NowStateIs("dashdown")&&!(_machine.GetState("dashdown") as FishBoss_DashDownState).isProduce)
        {
              DOTween.Shake(() =>game. Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);
            Debug.Log("sb!");
            (_machine.GetState("dashdown") as FishBoss_DashDownState).isProduce = true;
            GameObject temp = GameObjectPool.GetInstance().GetGameObject("DashEffect",new Vector3(transform.position.x,dashpos[0].position.y) + new Vector3(0, 3.5f), Quaternion.identity);
          
            GameObjectPool.GetInstance().ReleaseGameObject("DashEffect", temp, 1);

            GameObject temp1 = GameObjectPool.GetInstance().GetGameObject("DashAttack", new Vector3(transform.position.x, dashpos[0].position.y)+new Vector3(0,0.25f), Quaternion.identity);
            temp1.transform.rotation = Quaternion.identity;
            GameObjectPool.GetInstance().ReleaseGameObject("DashAttack", temp1, 1);

            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("DashAttack", new Vector3(transform.position.x, dashpos[0].position.y) + new Vector3(0,0.25f), Quaternion.identity);
            temp2.transform.rotation = Quaternion.Euler(0,180,0);
            GameObjectPool.GetInstance().ReleaseGameObject("DashAttack", temp2, 1);
        }
    }


    public int GetDashPointIndex()
    {
        float maxDistance = float.MinValue;
        int Index = -1;
        for (int i = 0; i < dashpos.Length; i++)
        {
            if(Vector3.Distance(player.transform.position, dashpos[i].position)>= maxDistance)
            {
                maxDistance = Vector3.Distance(player.transform.position, dashpos[i].position);
                Index = i;
            }
        }
        return Index;
    }
    public void Shoot()
    {
        _anim.SetTrigger("shoot");
         
    }
    public void ShootWaterBall()
    {
        DOTween.Shake(() => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);
        GameObject temp = GameObjectPool.GetInstance().GetGameObject("水球", shootpos.position, shootpos.rotation);
        temp.transform.right = ( player.transform.position- transform.position ).normalized;
        GameObjectPool.GetInstance().ReleaseGameObject("水球",temp, 1);
    }
    
}
